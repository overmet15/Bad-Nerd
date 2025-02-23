using UnityEngine;

public class ZeemoteJoystick : MonoBehaviour
{
	public bool touchPad;

	public Rect touchZone;

	public float deadZone = 0f;

	public bool normalize = false;

	public Vector2 position;

	public int tapCount;

	public int controllerID = 1;

	public int joystickID = 0;

	public bool[] buttonStatus = null;

	private static float tapTimeDelta = 0.3f;

	private float tapTimeWindow;

	public void Update()
	{
		if (ZeemoteInput.IsConnected(controllerID))
		{
			if (tapTimeWindow > 0f)
			{
				tapTimeWindow -= Time.deltaTime;
			}
			else
			{
				tapCount = 0;
			}
			position.x = ZeemoteInput.GetX(controllerID, joystickID);
			position.y = ZeemoteInput.GetY(controllerID, joystickID);
			if (ZeemoteInput.GetButtonDown(controllerID, 0))
			{
				if (tapTimeWindow > 0f)
				{
					tapCount++;
				}
				else
				{
					tapCount = 1;
					tapTimeWindow = tapTimeDelta;
				}
			}
			float magnitude = position.magnitude;
			if (magnitude < deadZone)
			{
				position = Vector2.zero;
			}
			else if (magnitude > 1f)
			{
				position /= magnitude;
			}
			else if (normalize)
			{
				position *= Mathf.InverseLerp(deadZone, 1f, magnitude);
			}
			int buttonCount = ZeemoteInput.GetButtonCount(controllerID);
			if (buttonStatus == null || buttonStatus.Length != buttonCount)
			{
				buttonStatus = new bool[buttonCount];
			}
			for (int i = 0; i < buttonCount; i++)
			{
				buttonStatus[i] = ZeemoteInput.GetButton(controllerID, i);
			}
		}
		else
		{
			ResetJoystick();
			tapCount = 0;
			buttonStatus = null;
		}
	}

	public void Disable()
	{
		base.gameObject.active = false;
	}

	public void ResetJoystick()
	{
		position = Vector2.zero;
	}
}
