using UnityEngine;
using Coliseo;

public class XInputTestCS : MonoBehaviour
{
    Controller cont;

    // Use this for initialization
    void Start()
    {
        cont = Controller.isWindows() ? (Controller) new ControllerWin() : (Controller) new ControllerLinOSX();
    }

    // Update is called once per frame
    void Update()
    {
        
        cont.FixedUpdate();

        // Set vibration according to triggers
        cont.vibrate(Controller.HeavyMotor, cont.GetBumper(Controller.LeftBumper)?1:0);
        cont.vibrate(Controller.LightMotor, cont.GetBumper(Controller.RightBumper)?1:0);

        // Make the current object turn
        transform.localRotation *= Quaternion.Euler(0.0f, cont.GetStick(Controller.LeftStick).x * 25.0f * Time.deltaTime, 0.0f);
    }

    void OnGUI()
    {
        string text = string.Format("IsConnected {0}\n", cont.IsConnected());
        text += string.Format("\tTriggers {0} {1}\n", cont.GetTrigger(Controller.LeftTrigger), cont.GetTrigger(Controller.RightTrigger));
        text += string.Format("\tD-Pad {0} {1} {2} {3}\n", cont.GetDPad(Controller.DPadUp), cont.GetDPad(Controller.DPadRight), cont.GetDPad(Controller.DPadDown), cont.GetDPad(Controller.DPadLeft));
        text += string.Format("\tButtons Start {0} Back {1}\n", cont.GetButton(Controller.Start), cont.GetButton(Controller.Back));
        text += string.Format("\tButtons LeftStick {0} RightStick {1} LeftShoulder {2} RightShoulder {3}\n", cont.GetButton(Controller.LeftStickHat), cont.GetButton(Controller.RightStickHat), cont.GetBumper(Controller.LeftBumper), cont.GetBumper(Controller.RightBumper));
        text += string.Format("\tButtons A {0} B {1} X {2} Y {3}\n", cont.GetButton(Controller.A), cont.GetButton(Controller.B), cont.GetButton(Controller.X), cont.GetButton(Controller.Y));
        text += string.Format("\tSticks Left {0} {1} Right {2} {3}\n", cont.GetStick(Controller.LeftStick).x, cont.GetStick(Controller.LeftStick).y, cont.GetStick(Controller.RightStick).x, cont.GetStick(Controller.RightStick).y);
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), text);
    }
}
