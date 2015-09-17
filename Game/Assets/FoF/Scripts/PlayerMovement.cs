using UnityEngine;
using System.Linq;
using XInputDotNetPure;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;            // The speed that the player will move at.

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    GamePadState state;
    GamePadState prevState;

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    float distToGround;

    void Awake()
    {
        // Set up references.
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    // Movement speed
    public float moveSpeed = 10.0F;

    // Controller sensitivity
    public float rotationSpeed = 100.0F;

    // For jumps
    public float jumpStrength = 50.0f;

    void FixedUpdate()
    {
        /// This controller state code is straight from the example file for XInput
        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);
        
        float translationX = state.ThumbSticks.Left.X * moveSpeed;
        float translationZ = state.ThumbSticks.Left.Y * moveSpeed;

        float rotationX = state.ThumbSticks.Right.X * rotationSpeed;
        float rotationY = -state.ThumbSticks.Right.Y * rotationSpeed;

        float h = rotationSpeed * Input.GetAxis("Mouse X");
        float v = rotationSpeed * Input.GetAxis("Mouse Y");

        translationX = (Input.GetKey("a") ? -speed : translationX);
        translationX = (Input.GetKey("d") ? speed : translationX);
        translationZ = (Input.GetKey("s") ? -speed : translationZ);
        translationZ = (Input.GetKey("w") ? speed : translationZ);

        // Move the player around the scene.
        Move(translationX, translationZ);

        // Turn the player according to controller input
        Turning(rotationX, rotationY);

        // Turn the player to face the mouse cursor.
        if(!state.IsConnected)
        {
            Turning(h, v);
            rotationX = h;
        }

        if(IsGrounded() && state.Buttons.A == ButtonState.Pressed)
        {
            playerRigidbody.AddForce(Vector3.up*jumpStrength);
        }

        // Animate the player.
        Animating(translationX, translationZ, rotationX);
        
    }
    
    // This is probably the source of the crashes. Just sayin.
    bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1F);
    }

    void Move(float h, float v)
    {
        // Set the movement vector based on the axis input.
        movement.Set(h, 0f, v);

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(transform.position + transform.rotation * movement);
    }

    // Now that the camera is directly on the head, we can, for the time being, 
    // have the controller directly move the head to look down. For now.
    void Turning(float x, float y)
    {
        Vector3 vec = new Vector3(y, x, 0);
        Quaternion deltaRotation = Quaternion.Euler(vec * Time.deltaTime + transform.rotation.eulerAngles);
        
        playerRigidbody.AddRelativeTorque(vec * playerRigidbody.mass / 2);
        playerRigidbody.MoveRotation(deltaRotation);
    }

    void Animating(float h, float v, float a)
    {
        // Tell the animator whether or not the player is walking.
        anim.SetFloat("HSpeed", h);
        anim.SetFloat("VSpeed", v);
        anim.SetFloat("AngularSpeed", a);
    }
}