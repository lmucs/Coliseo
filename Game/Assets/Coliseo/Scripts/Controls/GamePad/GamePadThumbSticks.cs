using UnityEngine;
using System.Collections;
using System;

namespace Coliseo
{
    public class GamePadThumbSticks
    {
        private StickValue _left;
        private StickValue _right;

        private const uint LEFT_STICK = 0;
        private const uint RIGHT_STICK = 1;

        public StickValue Left
        {
            get { return _left; }
        }

        public StickValue Right
        {
            get { return _right; }
        }

        public GamePadThumbSticks()
        {
            _left = new StickValue(LEFT_STICK);
            _right = new StickValue(RIGHT_STICK);
        }

        public class StickValue
        {
            private float _x;
            private float _y;

            public float X
            {
                get { return _x; }
            }

            public float Y
            {
                get { return _y; }
            }

            public StickValue(uint stick)
            {
                switch(stick) {
                    case LEFT_STICK:
                        _x = Input.GetAxis("Left_X_Axis");
                        _y = Input.GetAxis("Left_Y_Axis");
                        break;
                    case RIGHT_STICK:
                        _x = Input.GetAxis("Right_X_Axis");
                        _y = Input.GetAxis("Right_Y_Axis");
                        break;
                }
            }
        }
    }

    
}