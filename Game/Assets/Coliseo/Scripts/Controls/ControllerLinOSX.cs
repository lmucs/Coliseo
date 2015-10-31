using UnityEngine;
using System.Collections;
using System;

public class ControllerLinOSX : Controller {

    ///<summary>
    /// Must be called each frame to ensure accurate reading of *(Down|Up)
    ///</summary>
    public override void FixedUpdate()
    {
        throw new NotSupportedException();
    }

    public new bool connected
    {
        get { throw new NotSupportedException(); }
    }

    ///<summary>
    /// Returns true if the specified /trigger/ is fully pressed.
    ///</summary>
    public override bool GetTrigger(uint trigger)
    {
        throw new NotSupportedException();
    }

    ///<summary>
    /// Returns true during the frame the user fully pressed the given /trigger/.
    ///</summary>
    public override bool GetTriggerDown(uint trigger)
    {
        throw new NotSupportedException();
    }

    ///<summary>
    /// Returns true during the frame the user fully released the given /trigger/.
    ///</summary>
    public override bool GetTriggerUp(uint trigger)
    {
        throw new NotSupportedException();
    }

    ///<summary>
    /// Returns the values of the axes of the specified /stick/.
    ///</summary>
    public override Vector2 GetStick(uint stick)
    {
        throw new NotSupportedException();
    }

    ///<summary>
    /// Returns true while the specified /button/ is held down.
    ///</summary>
    public override bool GetButton(uint button)
    {
        throw new NotSupportedException();
    }
    ///<summary>
    /// Returns true during the frame the user pressed the given /button/.
    ///</summary>
    public override bool GetButtonDown(uint button)
    {
        throw new NotSupportedException();
    }

    ///<summary>
    /// Returns true during the frame the user released the given /button/.
    ///</summary>
    public override bool GetButtonUp(uint button)
    {
        throw new NotSupportedException();
    }

    ///<summary>
    /// Returns true while the specified /bumper/ is held down.
    ///</summary>
    public override bool GetBumper(uint bumper)
    {
        throw new NotSupportedException();
    }

    ///<summary>
    /// Returns true during the frame the user pressed the given /bumper/.
    ///</summary>
    public override bool GetBumperDown(uint bumper)
    {
        throw new NotSupportedException();
    }

    ///<summary>
    /// Returns true during the frame the user released the given /bumper/.
    ///</summary>
    public override bool GetBumperUp(uint bumper)
    {
        throw new NotSupportedException();
    }

    ///<summary>
    /// Returns true while the specified /direction/ is held down.
    ///</summary>
    public override bool GetDpad(uint direction)
    {
        throw new NotSupportedException();
    }

    ///<summary>
    /// Returns true during the frame the user pressed the given /direction/.
    ///</summary>
    public override bool GetDpadDown(uint direction)
    {
        throw new NotSupportedException();
    }

    ///<summary>
    /// Returns true during the frame the user released the given /direction/.
    ///</summary>
    public override bool GetDpadUp(uint direction)
    {
        throw new NotSupportedException();
    }

    ///<summary>
    /// Sets the vibration intensity (0f-1f) of the motor specified by /motor/.
    ///</summary>
    public override void vibrate(uint motor, float intensity)
    {
        // lulz y3ah r1ght
    }
}
