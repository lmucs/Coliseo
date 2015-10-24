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

        public float joystickMoveSensitivity = 10.0F;
        
        // Controller sensitivity
        public float joystickRotationSpeedHoriz = 100.0F;
        
        // Controller sensitivity
        public float joystickRotationSpeedVert = 10.0F;
        
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

            translationX = (Input.GetKey(KeyCode.A) ? -p.speed : translationX);
            translationX = (Input.GetKey(KeyCode.D) ? p.speed : translationX);
            translationZ = (Input.GetKey(KeyCode.S) ? -p.speed : translationZ);
            translationZ = (Input.GetKey(KeyCode.W) ? p.speed : translationZ);
            
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
            
            // Animate the player.
            p.animate(translationX, translationZ, rotationX, rotationY);
        }
    }
}