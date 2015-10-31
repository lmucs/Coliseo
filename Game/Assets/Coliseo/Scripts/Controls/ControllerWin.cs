﻿using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System;

public class ControllerWin : Controller {

    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    float lightMotor = 0;
    float heavyMotor = 0;

    ///<summary>
    /// Must be called each frame to ensure accurate reading of *(Down|Up)
    ///</summary>
    public override void FixedUpdate() {

        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);
        
    }

    public new bool connected
    {
        get { return state.IsConnected; }
    }

    ///<summary>
    /// Returns true if the specified /trigger/ is fully pressed.
    ///</summary>
    public override bool GetTrigger(uint trigger)
    {
        switch(trigger) {
            case Controller.LEFT_TRIGGER:
                return state.Triggers.Left == 1;
            case Controller.RIGHT_TRIGGER:
                return state.Triggers.Right == 1;
            default:
                throw new ArgumentException("That is not a valid trigger");
        }
    }

    ///<summary>
    /// Returns true during the frame the user fully pressed the given /trigger/.
    ///</summary>
    public override bool GetTriggerDown(uint trigger)
    {
        switch (trigger)
        {
            case Controller.LEFT_TRIGGER:
                return state.Triggers.Left == 1 && state.Triggers.Left != prevState.Triggers.Left;
            case Controller.RIGHT_TRIGGER:
                return state.Triggers.Right == 1 && state.Triggers.Right != prevState.Triggers.Right;
            default:
                throw new ArgumentException("That is not a valid trigger");
        }
    }

    ///<summary>
    /// Returns true during the frame the user fully released the given /trigger/.
    ///</summary>
    public override bool GetTriggerUp(uint trigger)
    {
        switch (trigger)
        {
            case Controller.LEFT_TRIGGER:
                return state.Triggers.Left == 0 && state.Triggers.Left != prevState.Triggers.Left;
            case Controller.RIGHT_TRIGGER:
                return state.Triggers.Right == 0 && state.Triggers.Right != prevState.Triggers.Right;
            default:
                throw new ArgumentException("That is not a valid trigger");
        }
    }

    ///<summary>
    /// Returns the values of the axes of the specified /stick/.
    ///</summary>
    public override Vector2 GetStick(uint stick)
    {
        switch (stick)
        {
            case Controller.LEFT_STICK:
                return new Vector2(state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y);
            case Controller.RIGHT_STICK:
                return new Vector2(state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y);
            default:
                throw new ArgumentException("That is not a valid stick");
        }
    }

    ///<summary>
    /// Returns true while the specified /button/ is held down.
    ///</summary>
    public override bool GetButton(uint button)
    {
        switch (button)
        {
            case Controller.BUTTON_A:
                return state.Buttons.A == ButtonState.Pressed;
            case Controller.BUTTON_B:
                return state.Buttons.B == ButtonState.Pressed;
            case Controller.BUTTON_Y:
                return state.Buttons.Y == ButtonState.Pressed;
            case Controller.BUTTON_X:
                return state.Buttons.X == ButtonState.Pressed;
            case Controller.BUTTON_START:
                return state.Buttons.Start == ButtonState.Pressed;
            case Controller.BUTTON_BACK:
                return state.Buttons.Back == ButtonState.Pressed;
            case Controller.BUTTON_LEFT_STICK:
                return state.Buttons.LeftStick == ButtonState.Pressed;
            case Controller.BUTTON_RIGHT_STICK:
                return state.Buttons.LeftStick == ButtonState.Pressed;
            default:
                throw new ArgumentException("That is not a valid button");
        }
    }
    ///<summary>
    /// Returns true during the frame the user pressed the given /button/.
    ///</summary>
    public override bool GetButtonDown(uint button)
    {
        switch (button)
        {
            case Controller.BUTTON_A:
                return state.Buttons.A == ButtonState.Pressed && prevState.Buttons.A == ButtonState.Released;
            case Controller.BUTTON_B:
                return state.Buttons.B == ButtonState.Pressed && prevState.Buttons.B == ButtonState.Released;
            case Controller.BUTTON_Y:
                return state.Buttons.Y == ButtonState.Pressed && prevState.Buttons.Y == ButtonState.Released;
            case Controller.BUTTON_X:
                return state.Buttons.X == ButtonState.Pressed && prevState.Buttons.X == ButtonState.Released;
            case Controller.BUTTON_START:
                return state.Buttons.Start == ButtonState.Pressed && prevState.Buttons.Start == ButtonState.Released;
            case Controller.BUTTON_BACK:
                return state.Buttons.Back == ButtonState.Pressed && prevState.Buttons.Back == ButtonState.Released;
            case Controller.BUTTON_LEFT_STICK:
                return state.Buttons.LeftStick == ButtonState.Pressed && prevState.Buttons.LeftStick == ButtonState.Released;
            case Controller.BUTTON_RIGHT_STICK:
                return state.Buttons.RightStick == ButtonState.Pressed && prevState.Buttons.RightStick == ButtonState.Released;
            default:
                throw new ArgumentException("That is not a valid button");
        }
    }

    ///<summary>
    /// Returns true during the frame the user released the given /button/.
    ///</summary>
    public override bool GetButtonUp(uint button)
    {
        switch (button)
        {
            case Controller.BUTTON_A:
                return state.Buttons.A == ButtonState.Released && prevState.Buttons.A == ButtonState.Pressed;
            case Controller.BUTTON_B:
                return state.Buttons.B == ButtonState.Released && prevState.Buttons.B == ButtonState.Pressed;
            case Controller.BUTTON_Y:
                return state.Buttons.Y == ButtonState.Released && prevState.Buttons.Y == ButtonState.Pressed;
            case Controller.BUTTON_X:
                return state.Buttons.X == ButtonState.Released && prevState.Buttons.X == ButtonState.Pressed;
            case Controller.BUTTON_START:
                return state.Buttons.Start == ButtonState.Released && prevState.Buttons.Start == ButtonState.Pressed;
            case Controller.BUTTON_BACK:
                return state.Buttons.Back == ButtonState.Released && prevState.Buttons.Back == ButtonState.Pressed;
            case Controller.BUTTON_LEFT_STICK:
                return state.Buttons.LeftStick == ButtonState.Released && prevState.Buttons.LeftStick == ButtonState.Pressed;
            case Controller.BUTTON_RIGHT_STICK:
                return state.Buttons.RightStick == ButtonState.Released && prevState.Buttons.RightStick == ButtonState.Pressed;
            default:
                throw new ArgumentException("That is not a valid button");
        }
    }

    ///<summary>
    /// Returns true while the specified /bumper/ is held down.
    ///</summary>
    public override bool GetBumper(uint bumper)
    {
        switch (bumper)
        {
            case Controller.LEFT_BUMPER:
                return state.Buttons.LeftShoulder == ButtonState.Pressed;
            case Controller.RIGHT_BUMPER:
                return state.Buttons.RightShoulder == ButtonState.Pressed;
            default:
                throw new ArgumentException("That is not a valid bumper");
        }
    }

    ///<summary>
    /// Returns true during the frame the user pressed the given /bumper/.
    ///</summary>
    public override bool GetBumperDown(uint bumper)
    {
        switch (bumper)
        {
            case Controller.LEFT_BUMPER:
                return state.Buttons.LeftShoulder == ButtonState.Pressed && prevState.Buttons.LeftShoulder == ButtonState.Released;
            case Controller.RIGHT_BUMPER:
                return state.Buttons.RightShoulder == ButtonState.Pressed && prevState.Buttons.RightShoulder == ButtonState.Released;
            default:
                throw new ArgumentException("That is not a valid bumper");
        }
    }

    ///<summary>
    /// Returns true during the frame the user released the given /bumper/.
    ///</summary>
    public override bool GetBumperUp(uint bumper)
    {
        switch (bumper)
        {
            case Controller.LEFT_BUMPER:
                return state.Buttons.LeftShoulder == ButtonState.Released && prevState.Buttons.LeftShoulder == ButtonState.Pressed;
            case Controller.RIGHT_BUMPER:
                return state.Buttons.RightShoulder == ButtonState.Released && prevState.Buttons.RightShoulder == ButtonState.Pressed;
            default:
                throw new ArgumentException("That is not a valid bumper");
        }
    }

    ///<summary>
    /// Returns true while the specified /direction/ is held down.
    ///</summary>
    public override bool GetDpad(uint direction)
    {
        switch (direction)
        {
            case Controller.DPAD_UP:
                return state.DPad.Up == ButtonState.Pressed;
            case Controller.DPAD_DOWN:
                return state.DPad.Down == ButtonState.Pressed;
            case Controller.DPAD_LEFT:
                return state.DPad.Left == ButtonState.Pressed;
            case Controller.DPAD_RIGHT:
                return state.DPad.Right == ButtonState.Pressed;
            default:
                throw new ArgumentException("That is not a valid direction");
        }
    }

    ///<summary>
    /// Returns true during the frame the user pressed the given /direction/.
    ///</summary>
    public override bool GetDpadDown(uint direction)
    {
        switch (direction)
        {
            case Controller.DPAD_UP:
                return state.DPad.Up == ButtonState.Pressed && prevState.DPad.Up == ButtonState.Released;
            case Controller.DPAD_DOWN:
                return state.DPad.Down == ButtonState.Pressed && prevState.DPad.Down == ButtonState.Released;
            case Controller.DPAD_LEFT:
                return state.DPad.Left == ButtonState.Pressed && prevState.DPad.Left == ButtonState.Released;
            case Controller.DPAD_RIGHT:
                return state.DPad.Right == ButtonState.Pressed && prevState.DPad.Right == ButtonState.Released;
            default:
                throw new ArgumentException("That is not a valid direction");
        }
    }

    ///<summary>
    /// Returns true during the frame the user released the given /direction/.
    ///</summary>
    public override bool GetDpadUp(uint direction)
    {
        switch (direction)
        {
            case Controller.DPAD_UP:
                return state.DPad.Up == ButtonState.Released && prevState.DPad.Up == ButtonState.Pressed;
            case Controller.DPAD_DOWN:
                return state.DPad.Down == ButtonState.Released && prevState.DPad.Down == ButtonState.Pressed;
            case Controller.DPAD_LEFT:
                return state.DPad.Left == ButtonState.Released && prevState.DPad.Left == ButtonState.Pressed;
            case Controller.DPAD_RIGHT:
                return state.DPad.Right == ButtonState.Released && prevState.DPad.Right == ButtonState.Pressed;
            default:
                throw new ArgumentException("That is not a valid direction");
        }
    }

    ///<summary>
    /// Sets the vibration intensity (0f-1f) of the motor specified by /motor/.
    ///</summary>
    public override void vibrate(uint motor, float intensity)
    {
        switch (motor)
        {
            case Controller.LIGHT_MOTOR:
                lightMotor = intensity;
                break;
            case Controller.HEAVY_MOTOR:
                heavyMotor = intensity;
                break;
            default:
                throw new ArgumentException("That is not a valid motor");
        }
        GamePad.SetVibration(playerIndex, heavyMotor, lightMotor);
    }
}
