/*
using System;
using System.Collections;
using System.Threading;
using UnityEngine;

public class ZeemoteInput : MonoBehaviour
{
	private enum AXISTYPE
	{
		AXIS_ANALOG = 0,
		AXIS_DIGITAL = 1
	}

	private class AxisData
	{
		public string AxisName;

		public AXISTYPE AxisType;

		public int controllerID;

		public int ID_1;

		public int ID_2;

		public AxisData(string name, AXISTYPE type, int cntID, int ID1, int ID2)
		{
			AxisName = name;
			AxisType = type;
			controllerID = cntID;
			ID_1 = ID1;
			ID_2 = ID2;
		}
	}

	private class ButtonData
	{
		public string ButtonName;

		public int controllerID;

		public int buttonID;

		public ButtonData(string name, int cntID, int btnID)
		{
			ButtonName = name;
			controllerID = cntID;
			buttonID = btnID;
		}
	}

	public const int MAX_CONTROLLER_ID = 7;

	public const int ERR_NONE = 0;

	public const int ERR_WHILE_CONNECTING = 1;

	public const int ERR_WHILE_DISCONNECTING = 2;

	public const int ERR_ALREADY_CONNECTED = 3;

	public const int ERR_SECURITY_EXCEPTION = 4;

	public const int BUTTON_UNDEFINED = 0;

	public const int BUTTON_UP = 1;

	public const int BUTTON_DOWN = 2;

	public const int BUTTON_LEFT = 3;

	public const int BUTTON_RIGHT = 4;

	public const int BUTTON_A = 5;

	public const int BUTTON_B = 6;

	public const int BUTTON_C = 7;

	public const int BUTTON_D = 8;

	public const int BUTTON_E = 9;

	public const int BUTTON_F = 10;

	public const int BUTTON_G = 11;

	public const int BUTTON_H = 12;

	public const int BUTTON_CENTER = 31;

	public const int BUTTON_L1 = 102;

	public const int BUTTON_R1 = 103;

	public const int BUTTON_L2 = 104;

	public const int BUTTON_R2 = 105;

	public const int BUTTON_THUMBL = 106;

	public const int BUTTON_THUMBR = 107;

	public const int BUTTON_START = 108;

	public const int BUTTON_SELECT = 109;

	public const int BUTTON_MODE = 110;

	public const int BUTTON_POWER = 111;

	public const int BUTTON_1 = 188;

	public const int BUTTON_2 = 189;

	public const int BUTTON_3 = 190;

	public const int BUTTON_4 = 191;

	public const int BUTTON_5 = 192;

	public const int BUTTON_6 = 193;

	public const int BUTTON_7 = 194;

	public const int BUTTON_8 = 195;

	public const int BUTTON_9 = 196;

	public const int BUTTON_10 = 197;

	public const int BUTTON_11 = 198;

	public const int BUTTON_12 = 199;

	public const int BUTTON_13 = 200;

	public const int BUTTON_14 = 201;

	public const int BUTTON_15 = 202;

	public const int BUTTON_16 = 203;

	private const int JOYSTICK_AXIS_X = 0;

	private const int JOYSTICK_AXIS_Y = 1;

	private static AndroidJavaObject ZeemotePlugin = null;

	private static int AvailableControllerNum = 0;

	private static bool[] ControllerEnable = new bool[8];

	private static object _Lock = new object();

	private static readonly AxisData[] _axisDataArray = new AxisData[11]
	{
		new AxisData("Horizontal", AXISTYPE.AXIS_ANALOG, 1, 0, 0),
		new AxisData("Vertical", AXISTYPE.AXIS_ANALOG, 1, 0, 1),
		new AxisData("Mouse X", AXISTYPE.AXIS_ANALOG, 2, 0, 0),
		new AxisData("Mouse Y", AXISTYPE.AXIS_ANALOG, 2, 0, 1),
		new AxisData("LookHorizontal", AXISTYPE.AXIS_ANALOG, 2, 0, 0),
		new AxisData("LookVertical", AXISTYPE.AXIS_ANALOG, 2, 0, 1),
		new AxisData("Fire1", AXISTYPE.AXIS_DIGITAL, 1, 0, -1),
		new AxisData("Fire2", AXISTYPE.AXIS_DIGITAL, 1, 1, -1),
		new AxisData("Fire3", AXISTYPE.AXIS_DIGITAL, 1, 2, -1),
		new AxisData("Jump", AXISTYPE.AXIS_DIGITAL, 1, 3, -1),
		new AxisData("TriggerFire", AXISTYPE.AXIS_DIGITAL, 1, 0, -1)
	};

	private static readonly ButtonData[] _buttonDataArray = new ButtonData[12]
	{
		new ButtonData("joystick button 0", 1, 0),
		new ButtonData("joystick button 1", 1, 1),
		new ButtonData("joystick button 2", 1, 2),
		new ButtonData("joystick button 3", 1, 3),
		new ButtonData("joystick 1 button 0", 1, 0),
		new ButtonData("joystick 1 button 1", 1, 1),
		new ButtonData("joystick 1 button 2", 1, 2),
		new ButtonData("joystick 1 button 3", 1, 3),
		new ButtonData("joystick 2 button 0", 2, 0),
		new ButtonData("joystick 2 button 1", 2, 1),
		new ButtonData("joystick 2 button 2", 2, 2),
		new ButtonData("joystick 2 button 3", 2, 3)
	};

	private static ZeemoteInput Instance
	{
		get
		{
			GameObject gameObject = GameObject.Find("/_ZeemoteInput");
			if (gameObject == null)
			{
				gameObject = new GameObject("_ZeemoteInput");
			}
			gameObject.hideFlags = HideFlags.HideAndDontSave;
			ZeemoteInput zeemoteInput = gameObject.GetComponent<ZeemoteInput>();
			if (zeemoteInput == null)
			{
				zeemoteInput = gameObject.AddComponent<ZeemoteInput>();
			}
			return zeemoteInput;
		}
	}

	public static bool SetupPlugin()
	{
		Debug.Log("[ZeemoteInput] SetupPlugin Called.");
		lock (_Lock)
		{
			if (ZeemotePlugin == null)
			{
				Debug.Log("[ZeemoteInput] Initialize ZeemotePlugin.");
				if (AndroidJNI.FindClass("com.zeemote.zc.Controller").ToInt32() == 0)
				{
					Debug.Log("[ZeemoteInput] FATAL ERROR!! Zeemote SDK is not linked. Zeemote SDK is required in order to use Zeemote Unity Plugin.");
					AndroidJNI.ExceptionClear();
					return false;
				}
				if (AndroidJNI.FindClass("com.zeemote.zc.unity.UnityPlugin").ToInt32() == 0)
				{
					Debug.Log("[ZeemoteInput] FATAL ERROR!! UnityPlugin is not linked. ");
					AndroidJNI.ExceptionClear();
					return false;
				}
				AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.zeemote.zc.unity.UnityPlugin");
				ZeemotePlugin = androidJavaClass.CallStatic<AndroidJavaObject>("getUnityPlugin", new object[0]);
				if (ZeemotePlugin.GetRawObject().Equals(IntPtr.Zero))
				{
					Debug.Log("[ZeemoteInput] FATAL ERROR!! Plugin initialize failed.");
					ZeemotePlugin = null;
					return false;
				}
				for (int i = 0; i < ControllerEnable.Length; i++)
				{
					ControllerEnable[i] = true;
				}
				if (Instance == null)
				{
					return false;
				}
				Instance.enabled = true;
			}
		}
		return true;
	}

	public static int FindAvailableControllers()
	{
		if (ZeemotePlugin == null)
		{
			return 0;
		}
		lock (_Lock)
		{
			AvailableControllerNum = ZeemotePlugin.Call<int>("findAvailableControllers", new object[0]);
		}
		return AvailableControllerNum;
	}

	public static string[] GetControllerNameList()
	{
		if (ZeemotePlugin == null)
		{
			return null;
		}
		lock (_Lock)
		{
			if (AvailableControllerNum <= 0)
			{
				return null;
			}
			string[] array = new string[AvailableControllerNum];
			for (int i = 0; i < AvailableControllerNum; i++)
			{
				array[i] = ZeemotePlugin.Call<string>("getControllerName", new object[1] { i });
			}
			return array;
		}
	}

	public static string[] GetControllerAddrList()
	{
		if (ZeemotePlugin == null)
		{
			return null;
		}
		lock (_Lock)
		{
			if (AvailableControllerNum <= 0)
			{
				return null;
			}
			string[] array = new string[AvailableControllerNum];
			for (int i = 0; i < AvailableControllerNum; i++)
			{
				array[i] = ZeemotePlugin.Call<string>("getControllerAddr", new object[1] { i });
			}
			return array;
		}
	}

	public static bool ConnectController(int controllerID, int controllerListIdx)
	{
		Debug.Log("[ZeemoteInput] ConnectController(" + controllerID + ")");
		if (ZeemotePlugin == null)
		{
			return false;
		}
		if (IsConnected(controllerID))
		{
			return false;
		}
		bool flag = ZeemotePlugin.Call<bool>("connect", new object[2] { controllerID, controllerListIdx });
		if (flag)
		{
			while (!IsConnected(controllerID) && GetError(controllerID) == 0)
			{
				Thread.Sleep(10);
			}
			if (GetError(controllerID) != 0)
			{
				return false;
			}
		}
		return flag;
	}

	private static IEnumerator _ConnectCoroutine(int controllerID)
	{
		do
		{
			yield return null;
		}
		while (!IsConnected(controllerID) && GetError(controllerID) == 0);
	}

	public static YieldInstruction ConnectControllerAsync(int controllerID, int controllerListIdx)
	{
		Debug.Log("[ZeemoteInput] ConnectControllerAsync(" + controllerID + ")");
		if (ZeemotePlugin == null)
		{
			return null;
		}
		if (IsConnected(controllerID))
		{
			return null;
		}
		if (!ZeemotePlugin.Call<bool>("connectAsync", new object[2] { controllerID, controllerListIdx }))
		{
			return null;
		}
		return Instance.StartCoroutine(_ConnectCoroutine(controllerID));
	}

	public static bool IsConnected(int controllerID)
	{
		if (ZeemotePlugin == null)
		{
			return false;
		}
		return ZeemotePlugin.Call<bool>("isConnected", new object[1] { controllerID });
	}

	public static bool IsUnexpectedDisconnect(int controllerID)
	{
		if (ZeemotePlugin == null)
		{
			return false;
		}
		return ZeemotePlugin.Call<bool>("isUnexpected", new object[1] { controllerID });
	}

	public static void DisconnectController(int controllerID)
	{
		Debug.Log("[ZeemoteInput] DisconnectController(" + controllerID + ")");
		if (ZeemotePlugin != null)
		{
			ZeemotePlugin.Call("disconnect", controllerID);
			while (IsConnected(controllerID) && GetError(controllerID) == 0)
			{
				Thread.Sleep(10);
			}
		}
	}

	private static IEnumerator _DisconnectCoroutine(int controllerID)
	{
		do
		{
			yield return null;
		}
		while (IsConnected(controllerID) && GetError(controllerID) == 0);
	}

	public static YieldInstruction DisconnectControllerAsync(int controllerID)
	{
		Debug.Log("[ZeemoteInput] DisconnectControllerAsync(" + controllerID + ")");
		if (ZeemotePlugin == null)
		{
			return null;
		}
		ZeemotePlugin.Call("disconnectAsync", controllerID);
		return Instance.StartCoroutine(_DisconnectCoroutine(controllerID));
	}

	public static int[] GetBatteryStatus(int controllerID)
	{
		if (ZeemotePlugin == null || controllerID < 1 || controllerID > 7 || !ControllerEnable[controllerID] || !IsConnected(controllerID))
		{
			return null;
		}
		return ZeemotePlugin.Call<int[]>("getBatteryStatus", new object[1] { controllerID });
	}

	public static float GetX(int controllerID, int joystickID)
	{
		if (ZeemotePlugin == null || controllerID < 1 || controllerID > 7 || !ControllerEnable[controllerID])
		{
			return 0f;
		}
		return ZeemotePlugin.Call<float>("getX", new object[2] { controllerID, joystickID });
	}

	public static float GetY(int controllerID, int joystickID)
	{
		if (ZeemotePlugin == null || controllerID < 1 || controllerID > 7 || !ControllerEnable[controllerID])
		{
			return 0f;
		}
		return ZeemotePlugin.Call<float>("getY", new object[2] { controllerID, joystickID });
	}

	public static bool GetButton(int controllerID, int buttonID)
	{
		if (ZeemotePlugin == null || controllerID < 1 || controllerID > 7 || !ControllerEnable[controllerID])
		{
			return false;
		}
		return ZeemotePlugin.Call<bool>("getButton", new object[2] { controllerID, buttonID });
	}

	public static bool GetButtonDown(int controllerID, int buttonID)
	{
		if (ZeemotePlugin == null || controllerID < 1 || controllerID > 7 || !ControllerEnable[controllerID])
		{
			return false;
		}
		return ZeemotePlugin.Call<bool>("getButtonDown", new object[2] { controllerID, buttonID });
	}

	public static bool GetButtonUp(int controllerID, int buttonID)
	{
		if (ZeemotePlugin == null || controllerID < 1 || controllerID > 7 || !ControllerEnable[controllerID])
		{
			return false;
		}
		return ZeemotePlugin.Call<bool>("getButtonUp", new object[2] { controllerID, buttonID });
	}

	public static float GetAxis(string name)
	{
		if (ZeemotePlugin == null)
		{
			return Input.GetAxis(name);
		}
		AxisData[] axisDataArray = _axisDataArray;
		foreach (AxisData axisData in axisDataArray)
		{
			if (axisData.AxisName.CompareTo(name) != 0)
			{
				continue;
			}
			if (!ControllerEnable[axisData.controllerID] || !IsConnected(axisData.controllerID))
			{
				break;
			}
			switch (axisData.AxisType)
			{
			case AXISTYPE.AXIS_ANALOG:
				switch (axisData.ID_2)
				{
				case 0:
					return ZeemotePlugin.Call<float>("getX", new object[2] { axisData.controllerID, axisData.ID_1 });
				case 1:
					return ZeemotePlugin.Call<float>("getY", new object[2] { axisData.controllerID, axisData.ID_1 });
				default:
					return 0f;
				}
			case AXISTYPE.AXIS_DIGITAL:
			{
				float num = 0f;
				if (axisData.ID_1 != -1 && ZeemotePlugin.Call<bool>("getButton", new object[2] { axisData.controllerID, axisData.ID_1 }))
				{
					num += 1f;
				}
				if (axisData.ID_2 != -1 && ZeemotePlugin.Call<bool>("getButton", new object[2] { axisData.controllerID, axisData.ID_2 }))
				{
					num -= 1f;
				}
				return num;
			}
			default:
				return 0f;
			}
		}
		return Input.GetAxis(name);
	}

	public static bool GetButton(string name)
	{
		if (ZeemotePlugin == null)
		{
			return Input.GetButton(name);
		}
		ButtonData[] buttonDataArray = _buttonDataArray;
		foreach (ButtonData buttonData in buttonDataArray)
		{
			if (buttonData.ButtonName.CompareTo(name) == 0)
			{
				if (!ControllerEnable[buttonData.controllerID] || !IsConnected(buttonData.controllerID))
				{
					break;
				}
				return ZeemotePlugin.Call<bool>("getButton", new object[2] { buttonData.controllerID, buttonData.buttonID });
			}
		}
		return Input.GetButton(name);
	}

	public static bool GetButtonDown(string name)
	{
		if (ZeemotePlugin == null)
		{
			return Input.GetButtonDown(name);
		}
		ButtonData[] buttonDataArray = _buttonDataArray;
		foreach (ButtonData buttonData in buttonDataArray)
		{
			if (buttonData.ButtonName.CompareTo(name) == 0)
			{
				if (!ControllerEnable[buttonData.controllerID] || !IsConnected(buttonData.controllerID))
				{
					break;
				}
				return ZeemotePlugin.Call<bool>("getButtonDown", new object[2] { buttonData.controllerID, buttonData.buttonID });
			}
		}
		return Input.GetButtonDown(name);
	}

	public static bool GetButtonUp(string name)
	{
		if (ZeemotePlugin == null)
		{
			return Input.GetButtonUp(name);
		}
		ButtonData[] buttonDataArray = _buttonDataArray;
		foreach (ButtonData buttonData in buttonDataArray)
		{
			if (buttonData.ButtonName.CompareTo(name) == 0)
			{
				if (!ControllerEnable[buttonData.controllerID] || !IsConnected(buttonData.controllerID))
				{
					break;
				}
				return ZeemotePlugin.Call<bool>("getButtonUp", new object[2] { buttonData.controllerID, buttonData.buttonID });
			}
		}
		return Input.GetButtonUp(name);
	}

	public static void CleanupPlugin()
	{
		if (ZeemotePlugin != null)
		{
			ZeemotePlugin.Call("cleanup");
			ZeemotePlugin = null;
			AvailableControllerNum = 0;
			for (int i = 0; i < ControllerEnable.Length; i++)
			{
				ControllerEnable[i] = true;
			}
			Debug.Log("[ZeemoteInput] Cleanup Plugin.");
		}
	}

	public static void SetEnable(int controllerID, bool enable)
	{
		if (controllerID >= 1 && controllerID <= 7)
		{
			ControllerEnable[controllerID] = enable;
		}
	}

	public static bool IsEnabled(int controllerID)
	{
		if (controllerID < 1 || controllerID > 7)
		{
			return false;
		}
		return ControllerEnable[controllerID];
	}

	public static int GetButtonCount(int controllerID)
	{
		if (ZeemotePlugin == null || controllerID < 1 || controllerID > 7 || !ControllerEnable[controllerID])
		{
			return 0;
		}
		return ZeemotePlugin.Call<int>("getButtonCount", new object[1] { controllerID });
	}

	public static int GetJoystickCount(int controllerID)
	{
		if (ZeemotePlugin == null || controllerID < 1 || controllerID > 7 || !ControllerEnable[controllerID])
		{
			return 0;
		}
		return ZeemotePlugin.Call<int>("getJoystickCount", new object[1] { controllerID });
	}

	public static int GetError(int controllerID)
	{
		if (ZeemotePlugin == null)
		{
			return 0;
		}
		return ZeemotePlugin.Call<int>("getError", new object[1] { controllerID });
	}

	private void LateUpdate()
	{
		if (ZeemotePlugin != null)
		{
			ZeemotePlugin.Call("clearButtonDownAndUpFlag");
		}
	}

	public static int GetButtonCode(int controllerID, int buttonID)
	{
		if (ZeemotePlugin == null || controllerID < 1 || controllerID > 7 || !ControllerEnable[controllerID])
		{
			return 0;
		}
		return ZeemotePlugin.Call<int>("getButtonCode", new object[2] { controllerID, buttonID });
	}

	public static string GetButtonLabel(int controllerID, int buttonID)
	{
		if (ZeemotePlugin == null || controllerID < 1 || controllerID > 7 || !ControllerEnable[controllerID])
		{
			return null;
		}
		return ZeemotePlugin.Call<string>("getButtonLabel", new object[2] { controllerID, buttonID });
	}

	public static bool GetButtonByButtonCode(int controllerID, int buttonCode)
	{
		if (ZeemotePlugin == null || controllerID < 1 || controllerID > 7 || !ControllerEnable[controllerID])
		{
			return false;
		}
		return ZeemotePlugin.Call<bool>("getButtonByButtonCode", new object[2] { controllerID, buttonCode });
	}

	public static bool GetButtonDownByButtonCode(int controllerID, int buttonCode)
	{
		if (ZeemotePlugin == null || controllerID < 1 || controllerID > 7 || !ControllerEnable[controllerID])
		{
			return false;
		}
		return ZeemotePlugin.Call<bool>("getButtonDownByButtonCode", new object[2] { controllerID, buttonCode });
	}

	public static bool GetButtonUpByButtonCode(int controllerID, int buttonCode)
	{
		if (ZeemotePlugin == null || controllerID < 1 || controllerID > 7 || !ControllerEnable[controllerID])
		{
			return false;
		}
		return ZeemotePlugin.Call<bool>("getButtonUpByButtonCode", new object[2] { controllerID, buttonCode });
	}

	public static bool SetHidEventConfiguration(string settingData)
	{
		if (ZeemotePlugin == null || settingData == null)
		{
			return false;
		}
		return ZeemotePlugin.Call<bool>("setHidEventConfiguration", new object[1] { settingData });
	}

	public static IntPtr CreateHidEventConfigureEntry(string[] deviceNameArray)
	{
		if (ZeemotePlugin == null || deviceNameArray == null || deviceNameArray.Length == 0)
		{
			return IntPtr.Zero;
		}
		jvalue[] args = AndroidJNIHelper.CreateJNIArgArray(new object[1] { deviceNameArray });
		IntPtr methodID = AndroidJNIHelper.GetMethodID(ZeemotePlugin.GetRawClass(), "createHidEventConfigureEntry", "([Ljava/lang/String;)Ljava/lang/Object;", false);
		return AndroidJNI.CallObjectMethod(ZeemotePlugin.GetRawObject(), methodID, args);
	}

	public static int AddButtonMapping(IntPtr configureEntryHandle, string buttonCodeName, string androidKeyCodeName, string label)
	{
		if (ZeemotePlugin == null || IntPtr.Zero.Equals(configureEntryHandle) || buttonCodeName == null || androidKeyCodeName == null)
		{
			return -1;
		}
		jvalue[] array = AndroidJNIHelper.CreateJNIArgArray(new object[4] { null, buttonCodeName, androidKeyCodeName, label });
		array[0].l = configureEntryHandle;
		IntPtr methodID = AndroidJNIHelper.GetMethodID(ZeemotePlugin.GetRawClass(), "addButtonMapping", "(Ljava/lang/Object;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I", false);
		return AndroidJNI.CallIntMethod(ZeemotePlugin.GetRawObject(), methodID, array);
	}

	public static int AddJoystickSetting(IntPtr configureEntryHandle, string xAxisIdName, float maxX, float minX, float flatX, string yAxisIdName, float maxY, float minY, float flatY)
	{
		if (ZeemotePlugin == null || IntPtr.Zero.Equals(configureEntryHandle) || (xAxisIdName == null && yAxisIdName == null))
		{
			return -1;
		}
		jvalue[] array = AndroidJNIHelper.CreateJNIArgArray(new object[9] { null, xAxisIdName, maxX, minX, flatX, yAxisIdName, maxY, minY, flatY });
		array[0].l = configureEntryHandle;
		IntPtr methodID = AndroidJNIHelper.GetMethodID(ZeemotePlugin.GetRawClass(), "addJoystickSetting", "(Ljava/lang/Object;Ljava/lang/String;FFFLjava/lang/String;FFF)I", false);
		return AndroidJNI.CallIntMethod(ZeemotePlugin.GetRawObject(), methodID, array);
	}

	public static bool AddHidEventConfigureEntry(IntPtr configureEntryHandle)
	{
		if (ZeemotePlugin == null || IntPtr.Zero.Equals(configureEntryHandle))
		{
			return false;
		}
		jvalue[] array = new jvalue[1];
		array[0].l = configureEntryHandle;
		IntPtr methodID = AndroidJNIHelper.GetMethodID(ZeemotePlugin.GetRawClass(), "addHidEventConfigureEntry", "(Ljava/lang/Object;)Z", false);
		return AndroidJNI.CallBooleanMethod(ZeemotePlugin.GetRawObject(), methodID, array);
	}

	public static IntPtr FindHidEventConfigure(string deviceName)
	{
		if (ZeemotePlugin == null || deviceName == null)
		{
			return IntPtr.Zero;
		}
		jvalue[] args = AndroidJNIHelper.CreateJNIArgArray(new object[1] { deviceName });
		IntPtr methodID = AndroidJNIHelper.GetMethodID(ZeemotePlugin.GetRawClass(), "findHidEventConfigure", "(Ljava/lang/String;)Ljava/lang/Object;", false);
		return AndroidJNI.CallObjectMethod(ZeemotePlugin.GetRawObject(), methodID, args);
	}

	public static bool RemoveHidEventConfigure(IntPtr configure)
	{
		if (ZeemotePlugin == null || IntPtr.Zero.Equals(configure))
		{
			return false;
		}
		jvalue[] array = new jvalue[1];
		array[0].l = configure;
		IntPtr methodID = AndroidJNIHelper.GetMethodID(ZeemotePlugin.GetRawClass(), "removeHidEventConfigure", "(Ljava/lang/Object;)Z", false);
		return AndroidJNI.CallBooleanMethod(ZeemotePlugin.GetRawObject(), methodID, array);
	}
}
*/