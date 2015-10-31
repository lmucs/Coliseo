using UnityEngine;
using Coliseo;

public class XInputTestCS : MonoBehaviour
{
    Controller cont;

    // Use this for initialization
    void Start()
    {
        cont = Controller.isWindows() ? (Controller)new ControllerWin() : (Controller)new ControllerLinOSX();
    }

    // Update is called once per frame
    void Update()
    {
        
        cont.FixedUpdate();

        // Set vibration according to triggers
        cont.vibrate(Controller.HEAVY_MOTOR, cont.GetBumper(Controller.LEFT_BUMPER)?1:0);
        cont.vibrate(Controller.LIGHT_MOTOR, cont.GetBumper(Controller.RIGHT_BUMPER)?1:0);

        // Make the current object turn
        transform.localRotation *= Quaternion.Euler(0.0f, cont.GetStick(Controller.LEFT_STICK).x * 25.0f * Time.deltaTime, 0.0f);
    }

    void OnGUI()
    {
        string text = "Use left stick to turn the cube, hold A to change color\n";
        text += string.Format("IsConnected {0}\n", cont.IsConnected());
        text += string.Format("\tTriggers {0} {1}\n", cont.GetTrigger(Controller.LEFT_TRIGGER), cont.GetTrigger(Controller.RIGHT_TRIGGER));
        text += string.Format("\tD-Pad {0} {1} {2} {3}\n", cont.GetDPad(Controller.DPAD_UP), cont.GetDPad(Controller.DPAD_RIGHT), cont.GetDPad(Controller.DPAD_DOWN), cont.GetDPad(Controller.DPAD_LEFT));
        text += string.Format("\tButtons Start {0} Back {1}\n", cont.GetButton(Controller.BUTTON_START), cont.GetButton(Controller.BUTTON_BACK));
        text += string.Format("\tButtons LeftStick {0} RightStick {1} LeftShoulder {2} RightShoulder {3}\n", cont.GetButton(Controller.BUTTON_LEFT_STICK), cont.GetButton(Controller.BUTTON_RIGHT_STICK), cont.GetButton(Controller.BUTTON_LEFT_SHOULDER), cont.GetButton(Controller.BUTTON_RIGHT_SHOULDER));
        text += string.Format("\tButtons A {0} B {1} X {2} Y {3}\n", cont.GetButton(Controller.BUTTON_A), cont.GetButton(Controller.BUTTON_B), cont.GetButton(Controller.BUTTON_X), cont.GetButton(Controller.BUTTON_Y));
        text += string.Format("\tSticks Left {0} {1} Right {2} {3}\n", cont.GetStick(Controller.LEFT_STICK).x, cont.GetStick(Controller.LEFT_STICK).y, cont.GetStick(Controller.RIGHT_STICK).x, cont.GetStick(Controller.RIGHT_STICK).y);
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), text);
    }
}
