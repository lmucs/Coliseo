using UnityEngine;
using System.Collections;

public class BasicCameraControl : MonoBehaviour
{

    // Movement speed
    public float speed = 10.0F;

    // Controller sensitivity
    public float rotationSpeed = 100.0F;

    // Mouse sensitivity
    public float horizontalSpeed = 4.0F;
    public float verticalSpeed = 4.0F;

    float leftTriggerLast = -1;
    float rightTriggerLast = -1;

    void Update()
    {

        // Controller input, specifically XBONE controller
        float translationX = Input.GetAxis("Left_X_Axis") * speed;
        float translationZ = Input.GetAxis("Left_Y_Axis") * speed;
        float rotationX = Input.GetAxis("Right_X_Axis") * rotationSpeed;
        float rotationY = Input.GetAxis("Right_Y_Axis") * rotationSpeed;

        // Keyboard and mouse input, for those who lack an XBONE controller
        float h = horizontalSpeed * Input.GetAxis("Mouse X");
        float v = verticalSpeed * Input.GetAxis("Mouse Y");

        // The keyboard overrides the controller if a key is pressed down
        translationX = (Input.GetKey("a") ? -speed : translationX);
        translationX = (Input.GetKey("d") ? speed : translationX);
        translationZ = (Input.GetKey("s") ? -speed : translationZ);
        translationZ = (Input.GetKey("w") ? speed : translationZ);

        // Scaled down for time
        translationZ *= Time.deltaTime;
        translationX *= Time.deltaTime;
        rotationX *= Time.deltaTime;
        rotationY *= Time.deltaTime;

        // From controller
        transform.Rotate(0, rotationX, 0);
        transform.Rotate(rotationY, 0, 0);

        // From mouse
        transform.Rotate(v, h, 0);

        // To prevent the camera from rolling 
        transform.Rotate(0, 0, -transform.eulerAngles.z);

        // Translation, placed after rotation to ensure accurate movement
        transform.Translate(0, 0, translationZ);
        transform.Translate(translationX, 0, 0);

        /*
           Input name     -> Axis Name in Unity  =>  Description
         * Left_X_Axis    ->  Joystick X axis    =>  x axis of left stick of XBONE controller
         * Left_Y_Axis    ->  Joystick Y axis    =>  y axis of left stick of XBONE controller
         * LeftTrigger    ->  Joystick 3rd axis  =>  left trigger of XBONE controller
         * Right_X_Axis   ->  Joystick 4th axis  =>  x axis of right stick of XBONE controller
         * Right_Y_Axis   ->  Joystick 5th axis  =>  y axis of right stick of XBONE controller
         * RightTrigger   ->  Joystick 6th axis  =>  right trigger of XBONE controller
         */

        if (leftTriggerLast != Input.GetAxis("LeftTrigger"))
        {
            leftTriggerLast = Input.GetAxis("LeftTrigger");
            Debug.Log("LeftTrigger: " + leftTriggerLast);
        }

        if (rightTriggerLast != Input.GetAxis("RightTrigger"))
        {
            rightTriggerLast = Input.GetAxis("RightTrigger");
            Debug.Log("RightTrigger: " + rightTriggerLast);
        }

    }
}