using UnityEngine;
using System.Collections;
using Coliseo;
using System;

public class ControllerLinOSX : Controller {

    GamePadState state;
    GamePadState prevState;

    ///<summary>
    /// Must be called each frame to ensure accurate reading of *(Down|Up)
    ///</summary>
    public override void FixedUpdate()
    {
        prevState = state ?? GamePad.GetState();
        state = GamePad.GetState();
    }

    ///<summary>
    /// Returns true if a controller is connected.
    ///</summary>
    public override bool IsConnected()
    {
        return state.IsConnected;
    }

    ///<summary>
    /// Returns true if the specified /trigger/ is fully pressed.
    ///</summary>
    public override bool GetTrigger(uint trigger)
    {
        switch (trigger)
        {
            case Controller.LeftTrigger:
                return state.Triggers.Left == 1;
            case Controller.RightTrigger:
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
            case Controller.LeftTrigger:
                return state.Triggers.Left == 1 && state.Triggers.Left != prevState.Triggers.Left;
            case Controller.RightTrigger:
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
            case Controller.LeftTrigger:
                return state.Triggers.Left == 0 && state.Triggers.Left != prevState.Triggers.Left;
            case Controller.RightTrigger:
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
            case Controller.LeftStick:
                return new Vector2(state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y);
            case Controller.RightStick:
                return new Vector2(state.ThumbSticks.Right.X, -state.ThumbSticks.Right.Y);
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
            case Controller.A:
                return state.Buttons.A == ButtonState.Pressed;
            case Controller.B:
                return state.Buttons.B == ButtonState.Pressed;
            case Controller.Y:
                return state.Buttons.Y == ButtonState.Pressed;
            case Controller.X:
                return state.Buttons.X == ButtonState.Pressed;
            case Controller.Start:
                return state.Buttons.Start == ButtonState.Pressed;
            case Controller.Back:
                return state.Buttons.Back == ButtonState.Pressed;
            case Controller.LeftStickHat:
                return state.Buttons.LeftStick == ButtonState.Pressed;
            case Controller.RightStickHat:
                return state.Buttons.RightStick == ButtonState.Pressed;
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
            case Controller.A:
                return state.Buttons.A == ButtonState.Pressed && prevState.Buttons.A == ButtonState.Released;
            case Controller.B:
                return state.Buttons.B == ButtonState.Pressed && prevState.Buttons.B == ButtonState.Released;
            case Controller.Y:
                return state.Buttons.Y == ButtonState.Pressed && prevState.Buttons.Y == ButtonState.Released;
            case Controller.X:
                return state.Buttons.X == ButtonState.Pressed && prevState.Buttons.X == ButtonState.Released;
            case Controller.Start:
                return state.Buttons.Start == ButtonState.Pressed && prevState.Buttons.Start == ButtonState.Released;
            case Controller.Back:
                return state.Buttons.Back == ButtonState.Pressed && prevState.Buttons.Back == ButtonState.Released;
            case Controller.LeftStickHat:
                return state.Buttons.LeftStick == ButtonState.Pressed && prevState.Buttons.LeftStick == ButtonState.Released;
            case Controller.RightStickHat:
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
            case Controller.A:
                return state.Buttons.A == ButtonState.Released && prevState.Buttons.A == ButtonState.Pressed;
            case Controller.B:
                return state.Buttons.B == ButtonState.Released && prevState.Buttons.B == ButtonState.Pressed;
            case Controller.Y:
                return state.Buttons.Y == ButtonState.Released && prevState.Buttons.Y == ButtonState.Pressed;
            case Controller.X:
                return state.Buttons.X == ButtonState.Released && prevState.Buttons.X == ButtonState.Pressed;
            case Controller.Start:
                return state.Buttons.Start == ButtonState.Released && prevState.Buttons.Start == ButtonState.Pressed;
            case Controller.Back:
                return state.Buttons.Back == ButtonState.Released && prevState.Buttons.Back == ButtonState.Pressed;
            case Controller.LeftStickHat:
                return state.Buttons.LeftStick == ButtonState.Released && prevState.Buttons.LeftStick == ButtonState.Pressed;
            case Controller.RightStickHat:
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
            case Controller.LeftBumper:
                return state.Buttons.LeftShoulder == ButtonState.Pressed;
            case Controller.RightBumper:
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
            case Controller.LeftBumper:
                return state.Buttons.LeftShoulder == ButtonState.Pressed && prevState.Buttons.LeftShoulder == ButtonState.Released;
            case Controller.RightBumper:
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
            case Controller.LeftBumper:
                return state.Buttons.LeftShoulder == ButtonState.Released && prevState.Buttons.LeftShoulder == ButtonState.Pressed;
            case Controller.RightBumper:
                return state.Buttons.RightShoulder == ButtonState.Released && prevState.Buttons.RightShoulder == ButtonState.Pressed;
            default:
                throw new ArgumentException("That is not a valid bumper");
        }
    }

    ///<summary>
    /// Returns true while the specified /direction/ is held down.
    ///</summary>
    public override bool GetDPad(uint direction)
    {
        switch (direction)
        {
            case Controller.DPadUp:
                return state.DPad.Up == ButtonState.Pressed;
            case Controller.DPadDown:
                return state.DPad.Down == ButtonState.Pressed;
            case Controller.DPadLeft:
                return state.DPad.Left == ButtonState.Pressed;
            case Controller.DPadRight:
                return state.DPad.Right == ButtonState.Pressed;
            default:
                throw new ArgumentException("That is not a valid direction");
        }
    }

    ///<summary>
    /// Returns true during the frame the user pressed the given /direction/.
    ///</summary>
    public override bool GetDPadDown(uint direction)
    {
        switch (direction)
        {
            case Controller.DPadUp:
                return state.DPad.Up == ButtonState.Pressed && prevState.DPad.Up == ButtonState.Released;
            case Controller.DPadDown:
                return state.DPad.Down == ButtonState.Pressed && prevState.DPad.Down == ButtonState.Released;
            case Controller.DPadLeft:
                return state.DPad.Left == ButtonState.Pressed && prevState.DPad.Left == ButtonState.Released;
            case Controller.DPadRight:
                return state.DPad.Right == ButtonState.Pressed && prevState.DPad.Right == ButtonState.Released;
            default:
                throw new ArgumentException("That is not a valid direction");
        }
    }

    ///<summary>
    /// Returns true during the frame the user released the given /direction/.
    ///</summary>
    public override bool GetDPadUp(uint direction)
    {
        switch (direction)
        {
            case Controller.DPadUp:
                return state.DPad.Up == ButtonState.Released && prevState.DPad.Up == ButtonState.Pressed;
            case Controller.DPadDown:
                return state.DPad.Down == ButtonState.Released && prevState.DPad.Down == ButtonState.Pressed;
            case Controller.DPadLeft:
                return state.DPad.Left == ButtonState.Released && prevState.DPad.Left == ButtonState.Pressed;
            case Controller.DPadRight:
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
        // lulz y3ah r1ght
    }
}
