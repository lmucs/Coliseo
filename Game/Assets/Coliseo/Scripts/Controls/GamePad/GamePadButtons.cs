using UnityEngine;
using System.Collections;
using System;

namespace Coliseo
{
    public class GamePadButtons
    {
        private ButtonState _a, _b, _back, _leftShoulder, _leftStick, _rightShoulder, _rightStick, _start, _x, _y;

        public ButtonState A
        {
            get { return _a; }
        }

        public ButtonState B
        {
            get { return _b; }
        }

        public ButtonState Back
        {
            get { return _back; }
        }

        public ButtonState LeftShoulder
        {
            get { return _leftShoulder; }
        }

        public ButtonState LeftStick
        {
            get { return _leftStick; }
        }

        public ButtonState RightShoulder
        {
            get { return _rightShoulder; }
        }

        public ButtonState RightStick
        {
            get { return _rightStick; }
        }

        public ButtonState Start
        {
            get { return _start; }
        }

        public ButtonState X
        {
            get { return _x; }
        }

        public ButtonState Y
        {
            get { return _y; }
        }

        public GamePadButtons()
        {
            _a = Input.GetButton("Button A") ? ButtonState.Pressed : ButtonState.Released;
            _b = Input.GetButton("Button B") ? ButtonState.Pressed : ButtonState.Released;
            _back = Input.GetButton("Back") ? ButtonState.Pressed : ButtonState.Released;
            _leftShoulder = Input.GetButton("LeftShoulder") ? ButtonState.Pressed : ButtonState.Released;
            _leftStick = Input.GetButton("LStick") ? ButtonState.Pressed : ButtonState.Released; // We shall see if this works
            _rightShoulder = Input.GetButton("RightShoulder") ? ButtonState.Pressed : ButtonState.Released;
            _rightStick = Input.GetButton("RStick") ? ButtonState.Pressed : ButtonState.Released; // We shall see if this works
            _start = Input.GetButton("Start") ? ButtonState.Pressed : ButtonState.Released;
            _x = Input.GetButton("Button X") ? ButtonState.Pressed : ButtonState.Released;
            _y = Input.GetButton("Button Y") ? ButtonState.Pressed : ButtonState.Released;
        }
    }
}
