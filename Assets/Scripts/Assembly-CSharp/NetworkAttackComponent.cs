using System.Collections.Generic;
using UnityEngine;

public class NetworkAttackComponent : EnemyAttackComponent
{
	private bool isLocationInSyncWithReceivedSignal;

	private float lastWalkTime;

	private Vector3 walkVelocity = new Vector3(9f, 9f, 9f);

	private Vector3 runVelocity;

	private float agentWalkSpeed = 15f;

	private float agentRunSpeed;

	public bool remotePlayerIsDead;

	private PlayerAttackComponent player;

	private static int tapjoyOnlineKillPPACount;

	protected override void Awake()
	{
		player = VNLUtil.getInstance<PlayerAttackComponent>("Player");
		runVelocity = walkVelocity * runSpeedMultiple;
		agentRunSpeed = agentWalkSpeed * runSpeedMultiple;
		base.Awake();
		recoveryRate = 0.05f;
	}

	public void setLocation(Vector3 pos, bool isRun)
	{
		if (Vector3.Distance(myTransform.position, pos) > 15f)
		{
			isLocationInSyncWithReceivedSignal = false;
		}
		if (isLocationInSyncWithReceivedSignal)
		{
			agent.enabled = true;
			float speed = ((!isRun) ? agentWalkSpeed : agentRunSpeed);
			agent.speed = speed;
			agent.SetDestination(pos);
			base.isRun = isRun;
			Vector3 speed2 = ((!isRun) ? walkVelocity : runVelocity);
			onWalkPressed(speed2);
			lastWalkTime = Time.fixedTime;
		}
		else
		{
			agent.enabled = false;
			Debug.Log(base.name + " jump to pos: " + pos);
			base.transform.position = pos;
			isLocationInSyncWithReceivedSignal = true;
		}
	}

	public override void doDie()
	{
		setLife(3f);
		if (remotePlayerIsDead)
		{
			tapjoyOnlineKillPPACount++;
			if (tapjoyOnlineKillPPACount < 1 || !(base.CurrentEnemy != null) || base.CurrentEnemy == player)
			{
			}
			base.doDie();
		}
	}

	protected override void reportLeaderBoard()
	{
		APIService.leaderBoard("baddestBullyBeaterOnline");
	}

	protected override void Update()
	{
		if (lastWalkTime < Time.fixedTime - 0.5f)
		{
			onStopWalking();
		}
		doSnapping();
	}

	protected override AttackComponent getEnemyToLookAtBeforeAttacking()
	{
		float num = 9999f;
		GameObject gameObject = null;
		GameObject[] collection = GameObject.FindGameObjectsWithTag("enemy");
		List<GameObject> list = new List<GameObject>(collection);
		list.Add(GameObject.Find("Player"));
		foreach (GameObject item in list)
		{
			if (!item.GetComponent<AttackComponent>().isDead && !item.Equals(base.gameObject))
			{
				float num2 = Vector3.Distance(myTransform.position, item.transform.position);
				if (num2 < num)
				{
					num = num2;
					gameObject = item;
				}
			}
		}
		if (gameObject == null)
		{
			return null;
		}
		return gameObject.GetComponent<AttackComponent>();
	}

	public override void onStopWalking()
	{
		base.onStopWalking();
		agent.enabled = false;
	}

	public override void onAttackPressed(int tapCount)
	{
		forceOnAttackPressed(tapCount);
		agent.enabled = false;
	}

	protected override void onHurt(AttackComponent enemy, Weapon weapon)
	{
		base.onHurt(enemy, weapon);
		agent.enabled = false;
	}

	public override void chooseTarget()
	{
	}

	public override void stopSeeking()
	{
	}

	public void loadEquipedItemsFromString(string items, string itemsPropertiesJson)
	{
		VNLUtil.soundFXOff = true;
		string[] array = items.Split(',');
		ItemSerializer.clearMap();
		ItemSerializer.IS_GAME_STATE_MODE = false;
		while (equippedItems.Count > 0)
		{
			BadNerdItem badNerdItem = equippedItems[0];
			badNerdItem.detachItem(false);
			Object.Destroy(badNerdItem.gameObject);
		}
		Debug.Log(base.name + " deserializing equipments properties...");
		ItemSerializer.setStringToMap(itemsPropertiesJson);
		for (int i = 0; i < array.Length; i++)
		{
			string text = array[i];
			if (!text.Equals(string.Empty))
			{
				Debug.Log("equipment: " + text);
				EquipmentItem equipmentItem = VNLUtil.instantiate<EquipmentItem>(text);
				equipmentItem.loadToItemList(i);
				equipmentItem.isFixable = false;
				equipmentItem.bePickedUp(this);
			}
		}
		VNLUtil.soundFXOff = false;
	}
}
