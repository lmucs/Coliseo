using UnityEngine;
using System.Linq;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 4;            // The speed that the player will move at.

    Vector3 movement;                   // The vector to store the direction of the player's movement.
    Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
    
    float distToGround;
    Transform cameraTransform;
    float cameraRotX = 0;

    void Awake()
    {
        // Copied from Init, just to be certain its working.
		if (Application.platform != RuntimePlatform.OSXEditor && Application.platform == RuntimePlatform.OSXPlayer && UnityEngine.VR.VRDevice.isPresent)
        {
            UnityEngine.VR.InputTracking.Recenter();
            UnityEngine.VR.VRSettings.showDeviceView = true;
            UnityEngine.VR.VRSettings.loadedDevice = UnityEngine.VR.VRDeviceType.Oculus;
            //Debug.Log("EyeHeight: " + OVRManager.profile.eyeHeight);
        }

        // Set up references.
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
        cameraTransform = transform.Find("ScientistSkeleton/Hips/Spine/Spine1/Neck/Head/CameraRig/Camera");   // Gotta love long identifiers
        cameraRotX = cameraTransform.localEulerAngles.x;

    }

    // Movement speed
    public float moveSpeed = 10.0F;

    // Controller sensitivity
    public float rotationSpeedHoriz = 100.0F;

    // Controller sensitivity
    public float rotationSpeedVert = 10.0F;

    // Mouse sensitivity
    public float mousePushHoriz = 10.0F;

    // For jumps
    public float jumpStrength = 50.0f;

    void FixedUpdate()
    {

        float translationX = Input.GetAxis("Left_X_Axis") * moveSpeed;
        float translationZ = Input.GetAxis("Left_Y_Axis") * moveSpeed;

        float rotationX = Input.GetAxis("Right_X_Axis") * rotationSpeedHoriz;
        float rotationY = Input.GetAxis("Right_Y_Axis") * rotationSpeedVert;

        float h = rotationSpeedHoriz * Input.GetAxis("Mouse X") * mousePushHoriz;
        float v = rotationSpeedVert * Input.GetAxis("Mouse Y");

        translationX = (Input.GetKey(KeyCode.A) ? -speed : translationX);
        translationX = (Input.GetKey(KeyCode.D) ? speed : translationX);
        translationZ = (Input.GetKey(KeyCode.S) ? -speed : translationZ);
        translationZ = (Input.GetKey(KeyCode.W) ? speed : translationZ);

        // Move the player around the scene.
        Move(translationX, translationZ);

        // Turn the player according to controller input
        Turning(rotationX, rotationY);

        // Turn the player to face the mouse cursor.
        if(Input.GetJoystickNames().Length == 0 || string.IsNullOrEmpty(Input.GetJoystickNames()[0]))
        {
            Turning(h, v);
            rotationX = h;
            rotationY = v;
        }

        if(IsGrounded() && (Input.GetButton("Button A") || Input.GetKey(KeyCode.Space)))
        {
            playerRigidbody.AddForce(Vector3.up*jumpStrength);
        }

        // Animate the player.
        Animating(translationX, translationZ, rotationX, rotationY);
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
        Vector3 vec = new Vector3(0, x, 0);
        Quaternion deltaRotation = Quaternion.Euler(vec * Time.deltaTime + transform.rotation.eulerAngles);

        if (!UnityEngine.VR.VRDevice.isPresent)
        {
            cameraRotX = Mathf.Clamp(cameraRotX + y, -90, 90);
            cameraTransform.localEulerAngles = new Vector3(cameraRotX, 0, 0);
        }

        playerRigidbody.AddRelativeTorque(vec * playerRigidbody.mass / 2);
        playerRigidbody.MoveRotation(deltaRotation);
    }

    bool rightTriggerActive = false;

    void Animating(float h, float v, float a, float b)
    {
        // Tell the animator whether or not the player is walking.
        bool isMoving = (h != 0 || v != 0);

        // a > 0 = turning left, a < 0 = turning right
        // b > 0 = down, b < 0 = up

        anim.SetFloat("Input X", h);
        anim.SetFloat("Input Z", v);
        anim.SetFloat("Rotation X", a);
        anim.SetFloat("Rotation Y", b);
        bool rightTriggerDown = (Input.GetAxis("RightTrigger") == 1);
        bool inSwing = anim.GetCurrentAnimatorStateInfo(0).IsTag("swordswing");

        if (!rightTriggerActive && rightTriggerDown && !inSwing || Input.GetMouseButtonDown(0))
        {
            rightTriggerActive = true;
            anim.SetTrigger("AttackDownTrigger");
        }
        rightTriggerActive = rightTriggerDown;

        // I'm leaving this here for when we eventually add the turning animation
        // anim.SetFloat("AngularSpeed", a);
    }

    
}