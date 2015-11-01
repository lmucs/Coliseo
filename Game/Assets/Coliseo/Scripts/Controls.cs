using UnityEngine;
using System.Collections;
using Coliseo;


namespace Coliseo
{
    public class Controls : Object
    {
        private Player p;
        private Controller cont;

        public Controls (Player player)
        {
            p = player;
            cont = Controller.isWindows() ? (Controller) new ControllerWin() :(Controller) new ControllerLinOSX();
        }

        public float joystickMoveSensitivity = 1f;

        // Controller sensitivity
        public float joystickRotationSpeedHoriz = 100.0F;
        
        // Controller sensitivity
        public float joystickRotationSpeedVert = 5.0F;
        
        // Mouse sensitivity
        public float mouseHorizontalSensitivity = 10.0F;
        
        // Update is called once per frame
        public void FixedUpdate ()
        {
            cont.FixedUpdate();

            Vector2 leftStick = cont.GetStick(Controller.LeftStick);
            Vector2 rightStick = cont.GetStick(Controller.RightStick);

            float translationX =  leftStick.x * joystickMoveSensitivity;
            float translationZ = leftStick.y * joystickMoveSensitivity;
            
            float rotationX = rightStick.x * joystickRotationSpeedHoriz;
            float rotationY = rightStick.y * joystickRotationSpeedVert;

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
            if(!cont.IsConnected())
            {
                p.turn(h, v);
                rotationX = h;
                rotationY = v;
            }
            
            if(cont.GetButton(Controller.A) || Input.GetKey(KeyCode.Space))
            {
                p.jump();
            }
            
            p.animate(translationX, translationZ, rotationX, rotationY);

            p.block(cont.GetTrigger(Controller.LeftTrigger) || Input.GetMouseButton(1));
            
            if ((cont.GetTriggerDown(Controller.RightTrigger) || Input.GetMouseButtonDown(0) ))
            {
                p.attack();
            }

            if (cont.GetButtonDown(Controller.B) || Input.GetKeyDown(KeyCode.F))
            {
                p.ToggleBeam();
            }
        }
    }
}