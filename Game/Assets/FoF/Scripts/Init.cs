using UnityEngine;
using System.Collections;

public class Init : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("VR device found: " + UnityEngine.VR.VRDevice.isPresent);
        if (UnityEngine.VR.VRDevice.isPresent)
        {
            UnityEngine.VR.InputTracking.Recenter();
            UnityEngine.VR.VRSettings.showDeviceView = true;
            UnityEngine.VR.VRSettings.loadedDevice = UnityEngine.VR.VRDeviceType.Oculus;
        }
	}
}
