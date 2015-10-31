using UnityEngine;
using System.Collections;
using System;

namespace Coliseo
{
    public class GamePadState
    {
        private GamePadButtons _buttons;
        private GamePadDPad _dpad;
        private GamePadThumbSticks _thumbSticks;
        private GamePadTriggers _triggers;

        public GamePadButtons Buttons
        {
            get { return _buttons; }
        }

        public GamePadDPad DPad
        {
            get { return _dpad; }
        }
        public bool IsConnected
        {
            get { return Input.GetJoystickNames().Length == 0 || string.IsNullOrEmpty(Input.GetJoystickNames()[0]); }
        }

        public GamePadThumbSticks ThumbSticks
        {
            get { return _thumbSticks; }
        }

        public GamePadTriggers Triggers
        {
            get { return _triggers; }
        }

        public GamePadState()
        {
            _buttons = new GamePadButtons();
            _dpad = new GamePadDPad();
            _thumbSticks = new GamePadThumbSticks();
            _triggers = new GamePadTriggers();
        }
    }
}