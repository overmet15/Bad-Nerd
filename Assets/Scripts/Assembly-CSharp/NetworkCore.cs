using System;
using System.Collections;
using System.Collections.Generic;
using Electrotank.Electroserver5.Api;
using Electrotank.Electroserver5.Core;
using UnityEngine;

public class NetworkCore : MonoBehaviour
{
	public static bool isNetworkMode;

	public static short ACTION_POSITION = 1;

	public static short ACTION_ANNOUCE_JOINING_POSITION = 11;

	public static short ACTION_GET_ONLINE_USERS = 2;

	public static short ACTION_QUIT = 3;

	public static short ACTION_ATTACK = 4;

	public static short ACTION_EQUIPMENTS = 5;

	public static short ACTION_DIE = 6;

	public static short ACTION_STATUS = 7;

	public static short ACTION_THROW = 8;

	public static short ACTION_HURT_ANIM = 9;

	public static short ACTION_BLOCK_START = 10;

	public static short ACTION_BLOCK_END = 11;

	public static short ACTION_COUNTER_ATTACK = 12;

	public static short ACTION_REQUEST_POSITION = 13;

	public static short ACTION_STORE_POSITION = 14;

	public static short ACTION_ZOMBIE_MODE = 15;

	public static short MSG_TYPE_QUEUED;

	public static short MSG_TYPE_INSTANT = 1;

	public static short MSG_TYPE_REQUEST = 2;

	public static short MSG_TYPE_DIRECT_QUEUED = 3;

	public static short MSG_TYPE_DIRECT_INSTANT = 4;

	public static bool isLoggedIn;

	public string playerName;

	private PlayerAttackComponent player;

	private Transform myTransform;

	private ElectroServer es;

	private Room room;

	private Connection tcpConn;

	private Connection udpConn;

	private PluginRequest pr;

	private string serverVersion = "168";

	private string clientVersion = "169";

	private int roomNumber = 1;

	private float lastPosSentTime;

	private bool doneInitializingAllExistingUsers;

	private void OnEnable()
	{
		Debug.Log("Start Network Core");
		roomNumber = 1;
		playerName = "Player" + SystemInfo.deviceUniqueIdentifier;
		es = new ElectroServer();
		es.Engine.Queueing = EsEngine.QueueDispatchType.External;
		addHandlers();
		isNetworkMode = true;
		connect(null);
		player = GetComponent<PlayerAttackComponent>();
		myTransform = base.transform;
	}

	private void addHandlers()
	{
		es.Engine.ConnectionAttemptResponse += onConnectionAttemptResponse;
		es.Engine.ConnectionResponse += onConnectionResponse;
		es.Engine.LoginResponse += onLoginResponse;
		es.Engine.JoinRoomEvent += onJoinRoom;
		es.Engine.GenericErrorResponse += onGenericErrorResponse;
		es.Engine.PluginMessageEvent += onPluginMessage;
		es.Engine.ConnectionClosedEvent += connect;
		es.Engine.UserVariableUpdateEvent += onUserVariableMessage;
	}

	private void removeHandlers()
	{
		if (es != null)
		{
			es.Engine.ConnectionAttemptResponse -= onConnectionAttemptResponse;
			es.Engine.ConnectionResponse -= onConnectionResponse;
			es.Engine.LoginResponse -= onLoginResponse;
			es.Engine.JoinRoomEvent -= onJoinRoom;
			es.Engine.GenericErrorResponse -= onGenericErrorResponse;
			es.Engine.PluginMessageEvent -= onPluginMessage;
			es.Engine.ConnectionClosedEvent -= connect;
			es.Engine.UserVariableUpdateEvent -= onUserVariableMessage;
		}
	}

	public void connect(ConnectionClosedEvent e)
	{
		isLoggedIn = false;
		if (!isNetworkMode || !GameStart.enableNetworking)
		{
			return;
		}
		if (e != null)
		{
			Debug.Log("Connection closed..." + e.MessageType);
			retryConnection();
			return;
		}
		Debug.Log("Connecting...");
		VNLUtil.getInstance().doStartCoRoutine(delegate
		{
			if (!isLoggedIn)
			{
				retryConnection();
			}
		}, 5f);
		base.enabled = true;
		Server server = new Server("server1");
		string host = "54.65.116.221";
		AvailableConnection con = new AvailableConnection(host, 9899, AvailableConnection.TransportType.BinaryTCP);
		server.AddAvailableConnection(con);
		AvailableConnection con2 = new AvailableConnection(host, 10000, AvailableConnection.TransportType.BinaryUDP, "0.0.0.0", 12000);
		server.AddAvailableConnection(con2);
		es.Engine.AddServer(server);
		es.Engine.Connect();
	}

	private void cleanupES5Objects()
	{
		es = null;
		room = null;
		tcpConn = null;
		udpConn = null;
		pr = null;
	}

	public void retryConnection()
	{
		Debug.Log("retryConnection...");
		if (udpConn != null)
		{
			udpConn.Close();
		}
		if (es != null)
		{
			try
			{
				es.Engine.Close();
			}
			catch (Exception)
			{
			}
		}
		isNetworkMode = false;
		base.enabled = false;
		cleanupES5Objects();
		VNLUtil.getInstance().doStartCoRoutine(delegate
		{
			base.enabled = true;
		}, 1f);
		isLoggedIn = false;
	}

	private void onConnectionResponse(ConnectionResponse res)
	{
		if (res.Successful)
		{
			Debug.Log("conn success: " + res.Successful);
			LoginRequest loginRequest = new LoginRequest();
			loginRequest.UserName = playerName;
			tcpConn = es.Engine.ActiveConnections[0];
			doSend(loginRequest);
		}
		else
		{
			Debug.Log("conn fail: " + res.Error.ToString());
		}
	}

	private void onConnectionAttemptResponse(ConnectionAttemptResponse res)
	{
		Debug.Log("onConnectionAttemptResponse");
		if (res.Successful)
		{
			int count = es.Engine.Servers[0].ActiveConnections.Count;
			Debug.Log("active connections: " + count);
			if (count == 2)
			{
				udpConn = es.Engine.ActiveConnections[1];
				Debug.Log("udp connection successful");
				isLoggedIn = true;
				pr = new PluginRequest();
				pr.PluginName = "MainPlugin" + serverVersion;
				pr.RoomId = room.Id;
				pr.ZoneId = room.ZoneId;
				announceJoiningPosition(myTransform.position.x, myTransform.position.y, myTransform.position.z);
				announceStatus(player);
				player.announceEquipments();
				requestPositionOfAllUsers();
				StartCoroutine(positionStoringLoop());
			}
		}
	}

	private IEnumerator positionStoringLoop()
	{
		while (isLoggedIn)
		{
			yield return new WaitForSeconds(10f);
			float x = myTransform.position.x;
			float y = myTransform.position.y;
			float z = myTransform.position.z;
			storePositionOnServer(x, y, z);
		}
	}

	private void connectUdp()
	{
		Server server = es.Engine.Servers[0];
		AvailableConnection availableConnection = server.AvailableConnectionsForType(AvailableConnection.TransportType.BinaryUDP)[0];
		Debug.Log("connecting udp..." + availableConnection.ToString());
		es.Engine.Connect(server, availableConnection);
	}

	private void onLoginResponse(LoginResponse res)
	{
		if (res.Successful)
		{
			Debug.Log("logged in: " + res.UserName);
			joinRoom();
		}
		else
		{
			Debug.Log("not logged in: " + res.UserName + " " + res.Error.ToString());
		}
	}

	private void joinRoom()
	{
		CreateRoomRequest createRoomRequest = new CreateRoomRequest();
		createRoomRequest.RoomName = "TestRoom_" + clientVersion + "_" + roomNumber;
		createRoomRequest.ZoneName = "TestZone";
		createRoomRequest.CreateOrJoinRoom = true;
		createRoomRequest.Capacity = 10;
		createRoomRequest.ReceivingRoomListUpdates = false;
		createRoomRequest.ReceivingVideoEvents = false;
		createRoomRequest.ReceivingRoomAttributeUpdates = false;
		createRoomRequest.ReceivingRoomVariableUpdates = false;
		PluginListEntry pluginListEntry = new PluginListEntry();
		pluginListEntry.ExtensionName = "MainExtension";
		pluginListEntry.PluginHandle = "MainPlugin" + serverVersion;
		pluginListEntry.PluginName = "MainPlugin" + serverVersion;
		List<PluginListEntry> list = new List<PluginListEntry>();
		list.Add(pluginListEntry);
		createRoomRequest.Plugins = list;
		doSend(createRoomRequest);
	}

	private void onGenericErrorResponse(GenericErrorResponse e)
	{
		Debug.Log("onGenericErrorResponse: " + e.ErrorType);
		if (e.ErrorType.Equals(ErrorType.RoomAtCapacity))
		{
			roomNumber++;
			if (roomNumber > 50)
			{
				roomNumber = 1;
			}
			joinRoom();
		}
	}

	private void onJoinRoom(JoinRoomEvent e)
	{
		room = es.ManagerHelper.ZoneManager.ZoneById(e.ZoneId).RoomById(e.RoomId);
		Debug.Log("room joined");
		connectUdp();
	}

	public void announceDeath()
	{
		if (isLoggedIn)
		{
			setGenericParams(pr, ACTION_DIE, MSG_TYPE_INSTANT);
			doSend(pr, tcpConn);
			isLoggedIn = false;
			base.enabled = false;
		}
	}

	public void disconnect()
	{
		Debug.Log("Disconnecting...");
		bool flag = isLoggedIn;
		removeHandlers();
		isLoggedIn = false;
		isNetworkMode = false;
		if (flag)
		{
			LeaveRoomRequest msg = new LeaveRoomRequest();
			doSend(msg, tcpConn);
			LogOutRequest msg2 = new LogOutRequest();
			doSend(msg2, tcpConn);
		}
		base.enabled = false;
		if (es != null)
		{
			try
			{
				es.Engine.Close();
			}
			catch (Exception)
			{
			}
		}
		cleanupES5Objects();
	}

	public void announceStatus(PlayerAttackComponent player)
	{
		if (isLoggedIn)
		{
			UpdateUserVariableRequest updateUserVariableRequest = new UpdateUserVariableRequest();
			updateUserVariableRequest.Name = "st";
			updateUserVariableRequest.Value = new EsObject();
			updateUserVariableRequest.Value.setFloatArray("healthArray", new float[4] { player.life, player.maxLife, player.energy, player.maxEnergy });
			updateUserVariableRequest.Value.setIntegerArray("attackArray", new int[7] { player.attack1Level, player.attack2Level, player.attack3Level, player.weaponAttack1Level, player.weaponAttack2Level, player.weaponAttack3Level, player.counterAttackLevel });
			updateUserVariableRequest.Value.setBoolean("isZombiefied", player.isZombiefied);
			doSend(updateUserVariableRequest, tcpConn);
		}
	}

	public void announceJoiningPosition(float x, float y, float z)
	{
		if (isLoggedIn)
		{
			pr.Parameters = new EsObject();
			pr.Parameters.setShort("a", ACTION_ANNOUCE_JOINING_POSITION);
			pr.Parameters.setString("playerName", playerName);
			pr.Parameters.setFloatArray("p", new float[3] { x, y, z });
			pr.Parameters.setBoolean("isRun", false);
			doSend(pr, tcpConn);
		}
	}

	public void announcePositionQueued(float x, float y, float z, bool isRun)
	{
		if (isLoggedIn && lastPosSentTime < Time.time - 0.15f)
		{
			pr.Parameters = new EsObject();
			pr.Parameters.setShort("a", ACTION_POSITION);
			pr.Parameters.setString("playerName", playerName);
			pr.Parameters.setFloatArray("p", new float[3] { x, y, z });
			pr.Parameters.setBoolean("isRun", isRun);
			doSend(pr, udpConn);
			lastPosSentTime = Time.time;
		}
	}

	public void storePositionOnServer(float x, float y, float z)
	{
		if (isLoggedIn)
		{
			pr.Parameters = new EsObject();
			pr.Parameters.setShort("a", ACTION_STORE_POSITION);
			pr.Parameters.setFloatArray("p", new float[3] { x, y, z });
			doSend(pr, udpConn);
		}
	}

	public void announceAttack(int tapCount)
	{
		if (isLoggedIn)
		{
			setGenericParams(pr, ACTION_ATTACK, MSG_TYPE_INSTANT);
			pr.Parameters.setInteger("tapCount", tapCount);
			doSend(pr, tcpConn);
		}
	}

	public void announceZombieMode(float seconds)
	{
		if (isLoggedIn)
		{
			setGenericParams(pr, ACTION_ZOMBIE_MODE, MSG_TYPE_INSTANT);
			pr.Parameters.setFloat("seconds", seconds);
			doSend(pr, tcpConn);
		}
	}

	public void requestPositionOfAllUsers()
	{
		if (isLoggedIn)
		{
			pr.Parameters = new EsObject();
			pr.Parameters.setShort("a", ACTION_REQUEST_POSITION);
			doSend(pr, tcpConn);
		}
	}

	public void announceCounterAttack()
	{
		if (isLoggedIn)
		{
			setGenericParams(pr, ACTION_COUNTER_ATTACK, MSG_TYPE_INSTANT);
			doSend(pr, tcpConn);
		}
	}

	public void announceBlockStart()
	{
		if (isLoggedIn)
		{
			setGenericParams(pr, ACTION_BLOCK_START, MSG_TYPE_INSTANT);
			doSend(pr, tcpConn);
		}
	}

	public void announceBlockEnd()
	{
		if (isLoggedIn)
		{
			setGenericParams(pr, ACTION_BLOCK_END, MSG_TYPE_INSTANT);
			doSend(pr, tcpConn);
		}
	}

	public void announceHurtAnum(string hurtString)
	{
		if (isLoggedIn)
		{
			setGenericParams(pr, ACTION_HURT_ANIM, MSG_TYPE_QUEUED);
			pr.Parameters.setString("hs", hurtString);
			doSend(pr, udpConn);
		}
	}

	public void announceThrow(Vector3 pos)
	{
		if (isLoggedIn)
		{
			setGenericParams(pr, ACTION_THROW, MSG_TYPE_INSTANT);
			pr.Parameters.setFloatArray("target", new float[3] { pos.x, pos.y, pos.z });
			doSend(pr, tcpConn);
		}
	}

	public void announceEquipments(string equipmentListString, string equipmentListProperties)
	{
		if (isLoggedIn)
		{
			UpdateUserVariableRequest updateUserVariableRequest = new UpdateUserVariableRequest();
			updateUserVariableRequest.Name = "eq";
			updateUserVariableRequest.Value = new EsObject();
			updateUserVariableRequest.Value.setStringArray("eq", new string[2] { equipmentListString, equipmentListProperties });
			doSend(updateUserVariableRequest, tcpConn);
		}
	}

	private void setGenericParams(PluginRequest pr, short action, short msgType)
	{
		pr.Parameters.setShort("a", action);
		pr.Parameters.setBoolean("g", true);
		pr.Parameters.setShort("t", msgType);
		pr.Parameters.setString("playerName", playerName);
	}

	private void requestUserList()
	{
		foreach (User user in es.ManagerHelper.UserManager.Users)
		{
			foreach (UserVariable userVariable in user.UserVariables)
			{
				handleUserVariable(user.UserName, userVariable);
			}
		}
	}

	private void onUserVariableMessage(UserVariableUpdateEvent e)
	{
		if (!doneInitializingAllExistingUsers)
		{
			Debug.Log("not doneInitializingAllExistingUsers, waiting before processing any UserVariableUpdates");
		}
		else
		{
			handleUserVariable(e.UserName, e.Variable);
		}
	}

	private void handleUserVariable(string userNAme, UserVariable variable)
	{
		NetworkAttackComponent networkAttackComponent = getNetworkAttackComponent(userNAme);
		if (!(networkAttackComponent == null))
		{
			if (variable.Name.Equals("eq"))
			{
				string[] stringArray = variable.Value.getStringArray("eq");
				networkAttackComponent.loadEquipedItemsFromString(stringArray[0], stringArray[1]);
			}
			else if (variable.Name.Equals("st"))
			{
				float[] floatArray = variable.Value.getFloatArray("healthArray");
				int[] integerArray = variable.Value.getIntegerArray("attackArray");
				bool boolean = variable.Value.getBoolean("isZombiefied");
				populateStatus(networkAttackComponent, floatArray, integerArray, boolean);
			}
		}
	}

	private void onPluginMessage(PluginMessageEvent e)
	{
		short @short = e.Parameters.getShort("a");
		if (@short == ACTION_REQUEST_POSITION)
		{
			string[] stringArray = e.Parameters.getStringArray("names");
			string[] array = stringArray;
			foreach (string text in array)
			{
				NetworkAttackComponent networkAttackComponent = getNetworkAttackComponent(text);
				if (networkAttackComponent != null)
				{
					float[] floatArray = e.Parameters.getFloatArray(text);
					Vector3 pos = new Vector3(floatArray[0], floatArray[1], floatArray[2]);
					networkAttackComponent.setLocation(pos, false);
				}
			}
			requestUserList();
			doneInitializingAllExistingUsers = true;
		}
		if (!doneInitializingAllExistingUsers)
		{
			Debug.Log("not doneInitializingAllExistingUsers, waiting before processing other PLUGIN requests");
		}
		else if (@short == ACTION_POSITION || @short == ACTION_ANNOUCE_JOINING_POSITION)
		{
			NetworkAttackComponent networkAttackComponent2 = getNetworkAttackComponent(e.Parameters.getString("playerName"));
			if (networkAttackComponent2 != null)
			{
				bool boolean = e.Parameters.getBoolean("isRun");
				float[] floatArray2 = e.Parameters.getFloatArray("p");
				Vector3 pos2 = new Vector3(floatArray2[0], floatArray2[1], floatArray2[2]);
				networkAttackComponent2.setLocation(pos2, boolean);
			}
		}
		else if (@short == ACTION_ATTACK)
		{
			NetworkAttackComponent networkAttackComponent3 = getNetworkAttackComponent(e.Parameters.getString("playerName"));
			if (networkAttackComponent3 != null)
			{
				int integer = e.Parameters.getInteger("tapCount");
				networkAttackComponent3.onAttackPressed(integer);
			}
		}
		else if (@short == ACTION_COUNTER_ATTACK)
		{
			NetworkAttackComponent networkAttackComponent4 = getNetworkAttackComponent(e.Parameters.getString("playerName"));
			if (networkAttackComponent4 != null)
			{
				networkAttackComponent4.doCounterAttack();
			}
		}
		else if (@short == ACTION_ZOMBIE_MODE)
		{
			NetworkAttackComponent networkAttackComponent5 = getNetworkAttackComponent(e.Parameters.getString("playerName"));
			if (networkAttackComponent5 != null)
			{
				float @float = e.Parameters.getFloat("seconds");
				networkAttackComponent5.becomeZombie(@float);
			}
		}
		else if (@short == ACTION_BLOCK_START)
		{
			NetworkAttackComponent networkAttackComponent6 = getNetworkAttackComponent(e.Parameters.getString("playerName"));
			if (networkAttackComponent6 != null)
			{
				networkAttackComponent6.onBlockPressed();
			}
		}
		else if (@short == ACTION_BLOCK_END)
		{
			NetworkAttackComponent networkAttackComponent7 = getNetworkAttackComponent(e.Parameters.getString("playerName"));
			if (networkAttackComponent7 != null)
			{
				networkAttackComponent7.onBlockReleased();
			}
		}
		else if (@short == ACTION_THROW)
		{
			NetworkAttackComponent networkAttackComponent8 = getNetworkAttackComponent(e.Parameters.getString("playerName"));
			if (networkAttackComponent8 != null)
			{
				float[] floatArray3 = e.Parameters.getFloatArray("target");
				networkAttackComponent8.onThrow(new Vector3(floatArray3[0], floatArray3[1], floatArray3[2]));
			}
		}
		else if (@short == ACTION_HURT_ANIM)
		{
			NetworkAttackComponent networkAttackComponent9 = getNetworkAttackComponent(e.Parameters.getString("playerName"));
			if (networkAttackComponent9 != null)
			{
				string @string = e.Parameters.getString("hs");
				networkAttackComponent9.animateHurt(@string);
			}
		}
		else if (@short == ACTION_DIE)
		{
			NetworkAttackComponent networkAttackComponent10 = getNetworkAttackComponent(e.Parameters.getString("playerName"));
			if (networkAttackComponent10 != null)
			{
				networkAttackComponent10.remotePlayerIsDead = true;
				networkAttackComponent10.doDie();
			}
		}
		else if (@short == ACTION_QUIT)
		{
			string string2 = e.Parameters.getString("playerName");
			GameObject obj = GameObject.Find(string2);
			UnityEngine.Object.Destroy(obj);
		}
	}

	private void populateStatus(NetworkAttackComponent networkPlayer, float[] heathArray, int[] attackArray, bool isZombiefied)
	{
		float life = heathArray[0];
		float maxLife = heathArray[1];
		float energy = heathArray[2];
		float maxEnergy = heathArray[3];
		int attack1Level = attackArray[0];
		int attack2Level = attackArray[1];
		int attack3Level = attackArray[2];
		int weaponAttack1Level = attackArray[3];
		int weaponAttack2Level = attackArray[4];
		int weaponAttack3Level = attackArray[5];
		int counterAttackLevel = attackArray[6];
		networkPlayer.maxLife = maxLife;
		networkPlayer.setLife(life);
		networkPlayer.energy = energy;
		networkPlayer.maxEnergy = maxEnergy;
		networkPlayer.attack1Level = attack1Level;
		networkPlayer.attack2Level = attack2Level;
		networkPlayer.attack3Level = attack3Level;
		networkPlayer.weaponAttack1Level = weaponAttack1Level;
		networkPlayer.weaponAttack2Level = weaponAttack2Level;
		networkPlayer.weaponAttack3Level = weaponAttack3Level;
		networkPlayer.counterAttackLevel = counterAttackLevel;
		if (!networkPlayer.isZombiefied && isZombiefied)
		{
			networkPlayer.becomeZombie(10f);
		}
	}

	private NetworkAttackComponent getNetworkAttackComponent(string playerName)
	{
		if (!playerName.Equals(this.playerName))
		{
			GameObject gameObject = GameObject.Find(playerName);
			if (!gameObject)
			{
				gameObject = createOtherPlayer(playerName).gameObject;
			}
			return gameObject.GetComponent<NetworkAttackComponent>();
		}
		return null;
	}

	private NetworkAttackComponent createOtherPlayer(string playerName)
	{
		if (!this.playerName.Equals(playerName))
		{
			NetworkAttackComponent networkAttackComponent = VNLUtil.instantiate<NetworkAttackComponent>("networkBully" + UnityEngine.Random.Range(1, 5));
			networkAttackComponent.name = playerName;
			Debug.Log("network player created: " + playerName + " " + networkAttackComponent.transform.position);
			return networkAttackComponent;
		}
		return null;
	}

	private void doSend(EsMessage msg)
	{
		doSend(msg, null);
	}

	private void doSend(EsMessage msg, Connection conn)
	{
		if (GameStart.enableNetworking && es != null)
		{
			if (es.Engine.ActiveConnections.Count == 0)
			{
				retryConnection();
			}
			else if (conn != null)
			{
				es.Engine.Send(msg, conn);
			}
			else
			{
				es.Engine.Send(msg);
			}
		}
	}

	private void OnApplicationQuit()
	{
		if (es != null)
		{
			try
			{
				es.Engine.Close();
			}
			catch (Exception)
			{
			}
		}
	}

	private void Update()
	{
		es.Engine.Dispatch();
	}
}
