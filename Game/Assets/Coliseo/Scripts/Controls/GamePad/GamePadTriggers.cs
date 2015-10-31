using UnityEngine;
using System.Collections;
using System;

namespace Coliseo
{
    public class GamePadTriggers
    {
        private float _left;
        private float _right;

        public float Left
        {
            get { return _left; }
        }

        public float Right
        {
            get { return _right; }
        }

        public GamePadTriggers()
        {
            _left = Input.GetAxis("LeftTrigger");
            _right = Input.GetAxis("RightTrigger");
        }

    }
}
