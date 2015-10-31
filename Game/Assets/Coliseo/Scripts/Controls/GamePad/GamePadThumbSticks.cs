using UnityEngine;
using System.Collections;
using System;

namespace Coliseo
{
    public class GamePadThumbSticks : UnityEngine.Object
    {
        public StickValue Left;
        public StickValue Right;

        public class StickValue
        {
            public float X;
            public float Y;
        }
    }

    
}