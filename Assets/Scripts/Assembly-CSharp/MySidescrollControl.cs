using UnityEngine;

public class MySidescrollControl : MonoBehaviour
{
	public MyJoystick moveTouchPad;

	public MyJoystick jumpTouchPad;

	public float forwardSpeed = 4f;

	public float backwardSpeed = 4f;

	public float jumpSpeed = 16f;

	public float inAirMultiplier = 0.25f;

	private Transform thisTransform;

	private CharacterController character;

	private Vector3 velocity;

	private bool canJump = true;

	public AttackComponent attackComponent;

	private void Start()
	{
		thisTransform = GetComponent<Transform>();
		character = GetComponent<CharacterController>();
		GameObject gameObject = GameObject.Find("PlayerSpawn");
		if ((bool)gameObject)
		{
			thisTransform.position = gameObject.transform.position;
		}
	}

	private void OnEndGame()
	{
		moveTouchPad.Disable();
		jumpTouchPad.Disable();
		base.enabled = false;
	}

	private void Update()
	{
		Vector3 motion = Vector3.zero;
		if (moveTouchPad.position.x > 0f)
		{
			attackComponent.onWalkPressed(Vector3.zero);
			motion = Vector3.right * forwardSpeed * moveTouchPad.position.x;
		}
		else if (moveTouchPad.position.x < 0f)
		{
			attackComponent.onWalkPressed(Vector3.zero);
			motion = Vector3.right * backwardSpeed * moveTouchPad.position.x;
		}
		else if (moveTouchPad.position.x == 0f)
		{
			attackComponent.onStopWalking();
		}
		if (character.isGrounded)
		{
			bool flag = false;
			MyJoystick myJoystick = jumpTouchPad;
			if (!myJoystick.IsFingerDown())
			{
				canJump = true;
			}
			if (canJump && myJoystick.IsFingerDown())
			{
				flag = true;
				canJump = false;
			}
			if (flag)
			{
				attackComponent.onAttackPressed(1);
			}
		}
		else
		{
			velocity.y += Physics.gravity.y * Time.deltaTime;
			motion.x *= inAirMultiplier;
		}
		motion += velocity;
		motion += Physics.gravity;
		motion *= Time.deltaTime;
		character.Move(motion);
		if (character.isGrounded)
		{
			velocity = Vector3.zero;
		}
	}
}
