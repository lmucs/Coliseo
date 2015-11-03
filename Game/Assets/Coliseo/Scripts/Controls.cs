using UnityEngine;
using System.Collections;
using Coliseo;
using System;


namespace Coliseo
{
    public class Controls : UnityEngine.Object
    {
        private Player p;
        private Controller cont;

        public Controls (Player player)
        {
            p = player;
            cont = Controller.isWindows() ? (Controller) new ControllerWin() :(Controller) new ControllerLinOSX();
        }

        // Currently 1 so that controller is more precise.
        public float moveSensitivity = 1f;

        // Controller sensitivity
        public float rotationSpeedHoriz = 100.0F;
        
        // Controller sensitivity
        public float rotationSpeedVert = 5.0F;
        
        public void FixedUpdate ()
        {
            cont.FixedUpdate();

            Vector2 move = checkMove();
            Vector2 turn = checkTurn();
            p.animate(move.x, move.y, turn.x, turn.y);

            checkJump();
            checkBlock();
            checkAttackBegin();
            checkBeamToggle();
        }

        public void setVibration(uint motor, float intensity)
        {
            cont.vibrate(motor, intensity);
        }

        private Vector2 checkMove()
        {
            Vector2 movement = new Vector2();
            movement.x = Input.GetKey(KeyCode.A) ? -1 : Input.GetKey(KeyCode.D) ? 1 : 0;
            movement.y = Input.GetKey(KeyCode.S) ? -1 : Input.GetKey(KeyCode.W) ? 1 : 0;
            movement = cont.IsConnected() ? cont.GetStick(Controller.LeftStick) : movement;
            movement *= moveSensitivity;
            p.move(movement.x, movement.y, 0f);
            return movement;
        }

        private Vector2 checkTurn()
        {
            Vector2 turn = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            turn = cont.IsConnected() ? cont.GetStick(Controller.RightStick) : turn;
            turn.x *= rotationSpeedHoriz;
            turn.y *= rotationSpeedVert;
            p.turn(turn.x, turn.y);
            return turn;
        }

        private bool checkJump()
        {
            bool doJump = cont.GetButton(Controller.A) || Input.GetKey(KeyCode.Space);
            if(doJump) { p.jump(); }
            return doJump;
        }

        private bool checkBlock()
        {
            bool blocking = cont.GetTrigger(Controller.LeftTrigger) || Input.GetMouseButton(1);
            p.block(blocking);
            return blocking;
        }
        
        // Temporarily in use.
        private bool checkAttackBegin()
        {
            bool initAttack = (cont.GetTriggerDown(Controller.RightTrigger) || Input.GetMouseButtonDown(0));
            if(initAttack) { p.attack(); }
            return initAttack;
        }

        // For near-future use.
        private bool checkAttackHold()
        {
            throw new NotSupportedException();
        }

        // For near-future use.
        private bool checkAttackRelease()
        {
            throw new NotSupportedException();
        }

        private bool checkBeamToggle()
        {
            bool doToggle = cont.GetButtonDown(Controller.B) || Input.GetKeyDown(KeyCode.F);
            if(doToggle) { p.ToggleBeam(); }
            return doToggle;
        }
    }
}