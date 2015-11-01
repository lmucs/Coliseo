using UnityEngine;
using System.Collections;

public abstract class Controller {
    // I feel like these would have been better off as an enum, but I  don't know
    // what to name it. That, and it really should belong to Controller anyways.
    public const uint
                    RightTrigger = 0,
                    LeftTrigger = 1,
                    LeftStick = 2,
                    RightStick = 3,
                    A = 4,
                    B = 5,
                    Y = 6,
                    X = 7,
                    RightBumper = 8,
                    LeftBumper = 9,
                    DPadLeft = 10,
                    DPadRight = 11,
                    DPadUp = 12,
                    DPadDown = 13,
                    Start = 14,  // The right one
                    Back = 15,  // The left one
                    LightMotor = 16,
                    HeavyMotor = 17,
                    LeftStickHat = 18, // AKA "clicking" a thumb stick
                    RightStickHat = 19;

    ///<summary>
    /// Must be called each frame to ensure accurate reading of *(Down|Up)
    ///</summary>
    public abstract void FixedUpdate();

    ///<summary>
    /// Returns true if a controller is connected.
    ///</summary>
    public abstract bool IsConnected();

    ///<summary>
    /// Returns true if the specified /trigger/ is fully pressed.
    ///</summary>
    public abstract bool GetTrigger(uint trigger);

    ///<summary>
    /// Returns true during the frame the user fully pressed the given /trigger/.
    ///</summary>
    public abstract bool GetTriggerDown(uint trigger);

    ///<summary>
    /// Returns true during the frame the user fully released the given /trigger/.
    ///</summary>
    public abstract bool GetTriggerUp(uint trigger);

    ///<summary>
    /// Returns the values of the axes of the specified /stick/.
    ///</summary>
    public abstract Vector2 GetStick(uint stick);

    ///<summary>
    /// Returns true while the specified /button/ is held down.
    ///</summary>
    public abstract bool GetButton(uint button);
    ///<summary>
    /// Returns true during the frame the user pressed the given /button/.
    ///</summary>
    public abstract bool GetButtonDown(uint button);

    ///<summary>
    /// Returns true during the frame the user released the given /button/.
    ///</summary>
    public abstract bool GetButtonUp(uint button);

    ///<summary>
    /// Returns true while the specified /bumper/ is held down.
    ///</summary>
    public abstract bool GetBumper(uint bumper);

    ///<summary>
    /// Returns true during the frame the user pressed the given /bumper/.
    ///</summary>
    public abstract bool GetBumperDown(uint bumper);

    ///<summary>
    /// Returns true during the frame the user released the given /bumper/.
    ///</summary>
    public abstract bool GetBumperUp(uint bumper);

    ///<summary>
    /// Returns true while the specified /direction/ is held down.
    ///</summary>
    public abstract bool GetDPad(uint direction);

    ///<summary>
    /// Returns true during the frame the user pressed the given /direction/.
    ///</summary>
    public abstract bool GetDPadDown(uint direction);

    ///<summary>
    /// Returns true during the frame the user released the given /direction/.
    ///</summary>
    public abstract bool GetDPadUp(uint direction);

    ///<summary>
    /// Sets the vibration intensity (0f-1f) of the motor specified by /motor/.
    ///</summary>
    public abstract void vibrate(uint motor, float intensity);

    public static bool isWindows()
    {
        return Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer;
    }
}
