using UnityEngine;
using System.Collections;
using Coliseo;


namespace Coliseo
{
    public class Controls : Object
    {
        private Player p;

        public Controls (Player player)
        {
            p = player;
        }

        public float joystickMoveSensitivity = 1f;//10.0F;
        
        // Controller sensitivity
        public float joystickRotationSpeedHoriz = 100.0F;
        
        // Controller sensitivity
        public float joystickRotationSpeedVert = 5.0F;
        
        // Mouse sensitivity
        public float mouseHorizontalSensitivity = 10.0F;
        
        // Update is called once per frame
        public void FixedUpdate ()
        {
            float translationX = Input.GetAxis("Left_X_Axis") * joystickMoveSensitivity;
            float translationZ = Input.GetAxis("Left_Y_Axis") * joystickMoveSensitivity;
            
            float rotationX = Input.GetAxis("Right_X_Axis") * joystickRotationSpeedHoriz;
            float rotationY = Input.GetAxis("Right_Y_Axis") * joystickRotationSpeedVert;
            
            float h = joystickRotationSpeedHoriz * Input.GetAxis("Mouse X") * mouseHorizontalSensitivity;
            float v = joystickRotationSpeedVert * Input.GetAxis("Mouse Y");

            translationX = (Input.GetKey(KeyCode.A) ? -1 : translationX);
            translationX = (Input.GetKey(KeyCode.D) ? 1 : translationX);
            translationZ = (Input.GetKey(KeyCode.S) ? -1 : translationZ);
            translationZ = (Input.GetKey(KeyCode.W) ? 1 : translationZ);
            
            // Move the player around the scene.
            p.move(translationX, translationZ, 0f);
            
            // Turn the player according to controller input
            p.turn(rotationX, rotationY);
            
            // Turn the player to face the mouse cursor.
            if(Input.GetJoystickNames().Length == 0 || string.IsNullOrEmpty(Input.GetJoystickNames()[0]))
            {
                p.turn(h, v);
                rotationX = h;
                rotationY = v;
            }
            
            if(Input.GetButton("Button A") || Input.GetKey(KeyCode.Space))
            {
                p.jump();
            }
            
            p.animate(translationX, translationZ, rotationX, rotationY);
            
            rightTriggerDown = (GetTrigger("RightTrigger") == 1);
            leftTriggerDown = (GetTrigger("LeftTrigger") == 1);
            
            p.block(leftTriggerDown || Input.GetMouseButton(1));
            
            if (!rightTriggerActive && (rightTriggerDown || Input.GetMouseButtonDown(0) ))
            {
                rightTriggerActive = true;
                p.attack();
            }

            rightTriggerActive = rightTriggerDown;

            if (Input.GetButtonDown("Button B") || Input.GetKeyDown(KeyCode.F))
            {
                p.ToggleBeam();
            }
        }
        
        private bool rightTriggerActive = false;
        private bool leftTriggerActive = false;

        private bool triggerMethodDefault = true;

        // For now, but soon this should be in controls.
        private bool rightTriggerDown;
        private bool leftTriggerDown;

        float GetTrigger(string trigger)
        {
            return Input.GetAxis( (triggerMethodDefault ? "" : "Desktop_") + trigger);
        }
    }
}