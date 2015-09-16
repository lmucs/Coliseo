using UnityEngine;
using System.Linq;
using UnityEngine.Networking;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;            // The speed that the player will move at.

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    Collider collider;
    NetworkIdentity identity;
    float distToGround;

    void Awake()
    {
        // Set up references.
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        distToGround = collider.bounds.extents.y;
        identity = GetComponent<NetworkIdentity>();
    }

    // Movement speed
    public float moveSpeed = 10.0F;

    // Controller sensitivity
    public float rotationSpeed = 100.0F;

    void FixedUpdate()
    {
        //Debug.Log("Player ID: " + identity.netId + ", localAuth: " + identity.hasAuthority);
        if (identity.hasAuthority)
        {
        float translationX = Input.GetAxis("Left_X_Axis") * moveSpeed;
        float translationZ = Input.GetAxis("Left_Y_Axis") * moveSpeed;

        float rotationX = Input.GetAxis("Right_X_Axis") * rotationSpeed;
        float rotationY = Input.GetAxis("Right_Y_Axis") * rotationSpeed;

        float h = rotationSpeed * Input.GetAxis("Mouse X");
        float v = rotationSpeed * Input.GetAxis("Mouse Y");

        translationX = (Input.GetKey("a") ? -speed : translationX);
        translationX = (Input.GetKey("d") ? speed : translationX);
        translationZ = (Input.GetKey("s") ? -speed : translationZ);
        translationZ = (Input.GetKey("w") ? speed : translationZ);

        // Move the player around the scene.
        Move(translationX, translationZ);

        // Turn the player to face the mouse cursor.
        Turning(rotationX, rotationY);

        // Turn the player to face the mouse cursor.
        if(Input.GetJoystickNames().Length == 0 || string.IsNullOrEmpty(Input.GetJoystickNames()[0]))
        {
            Turning(h, v);
            rotationX = h;
        }

        // Animate the player.
        Animating(translationX, translationZ, rotationX);
        }
    }

    

    bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 1.0F);
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
        
        anim.SetBool("Grounded", IsGrounded());
    }
}