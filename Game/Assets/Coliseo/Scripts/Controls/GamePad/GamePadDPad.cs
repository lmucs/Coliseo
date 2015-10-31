using UnityEngine;
using System.Collections;
using System;

namespace Coliseo
{
    public class GamePadDPad
    {
        private ButtonState _down, _left, _right, _up;

        public ButtonState Down
        {
            get { return _down; }
        }
        
        public ButtonState Left
        {
            get { return _left; }
        }
        
        public ButtonState Right
        {
            get { return _right; }
        }
        
        public ButtonState Up
        {
            get { return _up; }
        }

        public GamePadDPad()
        {
            // TODO: I'll need a mac to even attempt to make the dpad work here.
            _down = ButtonState.Released;
            _left = ButtonState.Released;
            _right = ButtonState.Released;
            _up = ButtonState.Released;
        }
    }
}
