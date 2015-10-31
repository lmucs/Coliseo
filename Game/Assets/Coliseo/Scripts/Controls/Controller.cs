using UnityEngine;
using System.Collections;

public abstract class Controller : Object {
    public const uint
                    RIGHT_TRIGGER = 0,
                    LEFT_TRIGGER = 1,
                    LEFT_STICK = 2,
                    RIGHT_STICK = 3,
                    BUTTON_A = 4,
                    BUTTON_B = 5,
                    BUTTON_Y = 6,
                    BUTTON_X = 7,
                    RIGHT_BUMPER = 8,
                    LEFT_BUMPER = 9,
                    DPAD_LEFT = 10,
                    DPAD_RIGHT = 11,
                    DPAD_UP = 12,
                    DPAD_DOWN = 13,
                    BUTTON_START = 14,
                    BUTTON_BACK = 15,
                    LIGHT_MOTOR = 16,
                    HEAVY_MOTOR = 17,
                    BUTTON_LEFT_STICK = 18,
                    BUTTON_RIGHT_STICK = 19;

    ///<summary>
    /// Must be called each frame to ensure accurate reading of *(Down|Up)
    ///</summary>
    public abstract void FixedUpdate();

    ///<summary>
    /// Returns true if a controller is connected.
    ///</summary>
    public bool connected;

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
    public abstract bool GetDpad(uint direction);

    ///<summary>
    /// Returns true during the frame the user pressed the given /direction/.
    ///</summary>
    public abstract bool GetDpadDown(uint direction);

    ///<summary>
    /// Returns true during the frame the user released the given /direction/.
    ///</summary>
    public abstract bool GetDpadUp(uint direction);

    ///<summary>
    /// Sets the vibration intensity (0f-1f) of the motor specified by /motor/.
    ///</summary>
    public abstract void vibrate(uint motor, float intensity);

}
