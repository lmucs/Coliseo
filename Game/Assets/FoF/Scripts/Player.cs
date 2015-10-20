using UnityEngine;
using System.Collections;
using fof;

public class Player : Actor
{
	public float speed = 6f;
	public float movementSpeed = 10f;
	private float cameraRotationX = 0f;
	private Transform cameraTransform;

	PlayerControls controller;
	// Use this for initialization
	void Start ()
	{
        controller = new PlayerControls(this);
		cameraTransform = transform.Find("ScientistSkeleton/Hips/Spine/Spine1/Neck/Head/Camera");
	}
	
	// Update is called once per frame
	void Update ()
	{
        controller.Update();
	}

	public override void Move(float h, float v)
	{
		Animating (h, v, 0);
	}

	// Now that the camera is directly on the head, we can, for the time being, 
	// have the controller directly move the head to look down. For now.
	public void Turn(float x, float y)
	{
		Vector3 vec = new Vector3(0, x, 0);
		Quaternion deltaRotation = Quaternion.Euler(vec * Time.deltaTime + transform.rotation.eulerAngles);
		
		if (!UnityEngine.VR.VRDevice.isPresent)
		{
			cameraRotationX = Mathf.Clamp(cameraRotationX + y, -90, 90);
			cameraTransform.localEulerAngles = new Vector3(cameraRotationX, 0, 0);
		}
		
		Animating (0, 0, a);
	}
}
