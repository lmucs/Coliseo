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
            get
            {
                string[] names = Input.GetJoystickNames();
                if (names.Length == 0)
                {
                    return false;
                }

                for (int i = 0; i < Input.GetJoystickNames().Length; i++)
                {
                    if (!string.IsNullOrEmpty(Input.GetJoystickNames()[i]))
                    {
                        return true;
                    }
                }

                return false;
            }
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