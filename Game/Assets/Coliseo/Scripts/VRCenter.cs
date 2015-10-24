using UnityEngine;
using System.Collections;

public class VRCenter : MonoBehaviour {

    public static void Setup()
    {
        UnityEngine.VR.VRSettings.showDeviceView = true;
        UnityEngine.VR.VRSettings.loadedDevice = UnityEngine.VR.VRDeviceType.Oculus;
        Recenter();
    }

    public static bool IsVRPresent()
    {
        return UnityEngine.VR.VRDevice.isPresent;
    }

    public static bool IsVREnabled()
    {
        return Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.OSXPlayer && UnityEngine.VR.VRDevice.isPresent;
    }

    public static void Recenter()
    {
        UnityEngine.VR.InputTracking.Recenter();
    }
}
