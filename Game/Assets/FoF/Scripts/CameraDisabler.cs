using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class CameraDisabler : MonoBehaviour {
    void Start()
    {
        if (!GetComponent<NetworkIdentity>().hasAuthority)
        {
            gameObject.transform.Find("Camera").gameObject.SetActive(false);
        }
    }
}
