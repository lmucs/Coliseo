using UnityEngine;
using System.Collections;
using System;

namespace Coliseo
{
    public class GamePad
    {
        // Pretty useless, yes, but it lets me basically copy ControllerWin.cs to ControllerLinOSX.cs
        public static GamePadState GetState()
        {
            return new GamePadState();
        }
    }
}
