using UnityEngine;

public class MyJoystick : MonoBehaviour
{
	private class Boundary
	{
		public Vector2 min = Vector2.zero;

		public Vector2 max = Vector2.zero;
	}

	public AttackComponent attackComponent;

	private static MyJoystick[] joysticks;

	private static bool enumeratedJoysticks;

	private static float tapTimeDelta = 0.3f;

	public bool touchPad;

	public Rect touchZone;

	public Vector2 deadZone = Vector2.zero;

	public bool normalize;

	public Vector2 position;

	public int tapCount;

	private int lastFingerId = -1;

	private float tapTimeWindow;

	private Vector2 fingerDownPos;

	private float fingerDownTime;

	private float firstDeltaTime = 0.5f;

	private GUITexture gui;

	private Rect defaultRect;

	private Boundary guiBoundary = new Boundary();

	private Vector2 guiTouchOffset;

	private Vector2 guiCenter;

	private void Start()
	{
		gui = GetComponent<GUITexture>();
		float num = 0.1f;
		float num2 = (float)Screen.width * num;
		float num3 = gui.pixelInset.width / num2;
		float num4 = gui.pixelInset.height / num3;
		gui.pixelInset = new Rect(0f, 0f, num2, num4);
		defaultRect = gui.pixelInset;
		defaultRect.x = num2 * 1.25f;
		defaultRect.y = num4;
		base.transform.position = new Vector3(0f, 0f, base.transform.position.z);
		if (touchPad)
		{
			if ((bool)gui.texture)
			{
				touchZone = defaultRect;
			}
			return;
		}
		guiTouchOffset.x = defaultRect.width * 0.5f;
		guiTouchOffset.y = defaultRect.height * 0.5f;
		guiCenter.x = defaultRect.x + guiTouchOffset.x;
		guiCenter.y = defaultRect.y + guiTouchOffset.y;
		guiBoundary.min.x = defaultRect.x - guiTouchOffset.x;
		guiBoundary.max.x = defaultRect.x + guiTouchOffset.x;
		guiBoundary.min.y = defaultRect.y - guiTouchOffset.y;
		guiBoundary.max.y = defaultRect.y + guiTouchOffset.y;
	}

	public void Disable()
	{
		base.gameObject.active = false;
		enumeratedJoysticks = false;
	}

	private void ResetJoystick()
	{
		gui.pixelInset = defaultRect;
		lastFingerId = -1;
		position = Vector2.zero;
		fingerDownPos = Vector2.zero;
		if (touchPad)
		{
			gui.color = new Color(gui.color.r, gui.color.g, gui.color.b, 0.075f);
		}
	}

	public bool IsFingerDown()
	{
		return lastFingerId != -1;
	}

	private void LatchedFinger(int fingerId)
	{
		if (lastFingerId == fingerId)
		{
			ResetJoystick();
		}
	}

	private void Update()
	{
		attackComponent.touchedCountroller = false;
		if (!enumeratedJoysticks)
		{
			joysticks = Object.FindObjectsOfType(typeof(MyJoystick)) as MyJoystick[];
			enumeratedJoysticks = true;
		}
		int touchCount = Input.touchCount;
		if (tapTimeWindow > 0f)
		{
			tapTimeWindow -= Time.deltaTime;
		}
		else
		{
			tapCount = 0;
		}
		if (touchCount == 0)
		{
			ResetJoystick();
		}
		else
		{
			for (int i = 0; i < touchCount; i++)
			{
				Touch touch = Input.GetTouch(i);
				Vector2 vector = touch.position - guiTouchOffset;
				bool flag = false;
				if (touchPad)
				{
					if (touchZone.Contains(touch.position))
					{
						flag = true;
						attackComponent.touchedCountroller = true;
					}
				}
				else if (gui.HitTest(touch.position))
				{
					flag = true;
				}
				if (flag && (lastFingerId == -1 || lastFingerId != touch.fingerId))
				{
					if (touchPad)
					{
						gui.color = new Color(gui.color.r, gui.color.g, gui.color.b, 0.15f);
						lastFingerId = touch.fingerId;
						fingerDownPos = touch.position;
						fingerDownTime = Time.time;
					}
					lastFingerId = touch.fingerId;
					if (tapTimeWindow > 0f)
					{
						tapCount++;
					}
					else
					{
						tapCount = 1;
						tapTimeWindow = tapTimeDelta;
					}
					MyJoystick[] array = joysticks;
					foreach (MyJoystick myJoystick in array)
					{
						if (myJoystick != this)
						{
							myJoystick.LatchedFinger(touch.fingerId);
						}
					}
				}
				if (lastFingerId == touch.fingerId)
				{
					if (touch.tapCount > tapCount)
					{
						tapCount = touch.tapCount;
					}
					if (touchPad)
					{
						position.x = Mathf.Clamp((touch.position.x - fingerDownPos.x) / (touchZone.width / 2f), -1f, 1f);
						position.y = Mathf.Clamp((touch.position.y - fingerDownPos.y) / (touchZone.height / 2f), -1f, 1f);
					}
					else
					{
						gui.pixelInset = new Rect(Mathf.Clamp(vector.x, guiBoundary.min.x, guiBoundary.max.x), Mathf.Clamp(vector.y, guiBoundary.min.y, guiBoundary.max.y), gui.pixelInset.width, gui.pixelInset.height);
					}
					if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
					{
						ResetJoystick();
					}
				}
			}
		}
		if (!touchPad)
		{
			position.x = (gui.pixelInset.x + guiTouchOffset.x - guiCenter.x) / guiTouchOffset.x;
			position.y = (gui.pixelInset.y + guiTouchOffset.y - guiCenter.y) / guiTouchOffset.y;
		}
		float num = Mathf.Abs(position.x);
		float num2 = Mathf.Abs(position.y);
		if (num < deadZone.x)
		{
			position.x = 0f;
		}
		else if (normalize)
		{
			position.x = Mathf.Sign(position.x) * (num - deadZone.x) / (1f - deadZone.x);
		}
		if (num2 < deadZone.y)
		{
			position.y = 0f;
		}
		else if (normalize)
		{
			position.y = Mathf.Sign(position.y) * (num2 - deadZone.y) / (1f - deadZone.y);
		}
	}
}
