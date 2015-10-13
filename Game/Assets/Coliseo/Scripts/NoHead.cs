using UnityEngine;
using System.Collections;

public class NoHead : MonoBehaviour {

	// Use this for initialization


	void Start () {
        Transform transform = gameObject.GetComponent<Transform>();
        Transform headTransform = transform.FindChild("Head");
        transform.localScale = new Vector3(1.0F, 0.1F, 1.0F);
        headTransform.localScale = new Vector3(0.1F, 0.1F, 0.1F);


        Transform cameraRigTransform = headTransform.FindChild("CameraRig");
        cameraRigTransform.localPosition = new Vector3(2.0F, 13.0F, 2.0F); // comment this out to place head correctly.
    }
}
