using UnityEngine;

public class MyCameraRelativeControl : MonoBehaviour
{
    private AttackComponent attackComponent;

    public MyJoystick moveJoystick;

    public Transform cameraPivot;

    public Transform cameraTransform;

    public float speed = 5f;

    public float jumpSpeed = 8f;

    public float inAirMultiplier = 0.25f;

    public Vector2 rotationSpeed = new Vector2(50f, 25f);

    private Transform thisTransform;

    private CharacterController character;

    private Vector3 velocity;

    private bool canJump = true;

    private void Start()
    {
        attackComponent = GetComponent<AttackComponent>();
        moveJoystick.attackComponent = attackComponent;
        thisTransform = GetComponent<Transform>();
        character = GetComponent<CharacterController>();
        GameObject gameObject = GameObject.Find("PlayerSpawn");
        if ((bool)gameObject)
        {
            thisTransform.position = gameObject.transform.position;
        }
    }

    private void FaceMovementDirection()
    {
        Vector3 vector = character.velocity;
        vector.y = 0f;
        if (vector.magnitude > 0.1f)
        {
            thisTransform.forward = vector.normalized;
        }
    }

    private void OnEndGame()
    {
        moveJoystick.Disable();
        base.enabled = false;
    }

    public void toggle(bool on)
    {
        attackComponent.onStopWalking();
        base.enabled = on;
    }

    private void Update()
    {
        if (PlayerAttackComponent.allowMoving)
        {
            bool flag = attackComponent.isHurting();
            bool flag2 = attackComponent.isAttacking();
            Vector2 vector = new Vector2(moveJoystick.position.x, moveJoystick.position.y);
            if (GameStart.isZeemoteConnected)
            {
                vector = new Vector2(ZeemoteInput.GetAxis("Horizontal"), ZeemoteInput.GetAxis("Vertical"));
            }

            // Added WASD keyboard support for movement:
            Vector2 keyboardInput = Vector2.zero;
            if (Input.GetKey(KeyCode.W)) keyboardInput.y = 1;
            if (Input.GetKey(KeyCode.S)) keyboardInput.y = -1;
            if (Input.GetKey(KeyCode.A)) keyboardInput.x = -1;
            if (Input.GetKey(KeyCode.D)) keyboardInput.x = 1;
            if (keyboardInput != Vector2.zero)
            {
                vector = keyboardInput;
            }

            Vector3 vector2 = cameraTransform.TransformDirection(new Vector3(vector.x, 0f, vector.y));
            vector2.y = 0f;
            vector2.Normalize();
            Vector2 vector3 = new Vector2(Mathf.Abs(vector.x), Mathf.Abs(vector.y));
            float num = ((!(vector3.x > vector3.y)) ? vector3.y : vector3.x);
            if (attackComponent.isRun)
            {
                num *= attackComponent.runSpeedMultiple;
            }
            vector2 *= speed * num;
            if (vector2 == Vector3.zero)
            {
                attackComponent.onStopWalking();
            }
            else
            {
                attackComponent.onWalkPressed(vector2);
            }
            vector2 += velocity;
            vector2 += Physics.gravity;
            vector2 *= Time.deltaTime;
            if (!flag && !flag2)
            {
                character.Move(vector2);
            }
            if (character.isGrounded)
            {
                velocity = Vector3.zero;
            }
            if (!flag2)
            {
                FaceMovementDirection();
            }
        }

        // --- Added Mouse Camera Movement ---

        // Press F1 to lock the cursor and hide it
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                // Unlock the cursor
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                // Lock the cursor
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }


        // Read mouse input
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (cameraPivot != null)
        {
            // Rotate the cameraPivot horizontally (yaw)
            cameraPivot.Rotate(Vector3.up, mouseX * rotationSpeed.x * Time.deltaTime, Space.World);

            // Adjust the vertical rotation (pitch) with clamping to prevent extreme angles
            float currentPitch = cameraPivot.localEulerAngles.x;
            if (currentPitch > 180f)
            {
                currentPitch -= 360f;
            }
            float newPitch = Mathf.Clamp(currentPitch - mouseY * rotationSpeed.y * Time.deltaTime, -30f, 60f);
            Vector3 localEuler = cameraPivot.localEulerAngles;
            localEuler.x = newPitch;
            cameraPivot.localEulerAngles = localEuler;
        }
    }
}
