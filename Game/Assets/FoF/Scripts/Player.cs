using UnityEngine;
using System.Collections;
using FoF;

namespace FoF
{

	public class Player : Actor
	{
		public float speed = 6f;

		private Rigidbody rb;
		private Animator anim;
		private Controls controls;

		private float distToGround;
		private Transform cameraTransform;
		private float cameraRotX = 0;
		
		// For jumps
		public float jumpStrength = 50.0f;

		public bool grounded
		{
			get
			{
				// FIXME: There's a better way to do this involving colliders I think
				return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1F);
			}
		}

		// Unity's initalization
		void Start ()
		{
			if (Application.platform != RuntimePlatform.OSXEditor && Application.platform == RuntimePlatform.OSXPlayer && UnityEngine.VR.VRDevice.isPresent)
			{
				UnityEngine.VR.InputTracking.Recenter();
				UnityEngine.VR.VRSettings.showDeviceView = true;
				UnityEngine.VR.VRSettings.loadedDevice = UnityEngine.VR.VRDeviceType.Oculus;
			}

			anim = GetComponent<Animator>();
			rb = GetComponent<Rigidbody>();
			distToGround = GetComponent<Collider>().bounds.extents.y;
			cameraTransform = transform.Find("ScientistSkeleton/Hips/Spine/Spine1/Neck/Head/Camera");   // Gotta love long identifiers
			cameraRotX = cameraTransform.localEulerAngles.x;
			controls = new Controls(this);
		}
		
		// We shouldn't have to update here. Controls should move us around with its FixedUpdate()
		void FixedUpdate ()
		{
			controls.FixedUpdate ();
		}

		public override void move (float h, float v, float ignored) // FIXME: should do movement in 3D, instead of just 2D
		{
			Vector3 movement = new Vector3();
			// Set the movement vector based on the axis input.
			movement.Set(h, 0f, v);
			
			// Normalise the movement vector and make it proportional to the speed per second.
			movement = movement.normalized * speed * Time.deltaTime;
			
			// Move the player to it's current position plus the movement.
			rb.MovePosition(transform.position + transform.rotation * movement);
		}

		// Now that the camera is directly on the head, we can, for the time being, 
		// have the controller directly move the head to look down. For now.
		public override void turn (float x, float y)
		{
			Vector3 vec = new Vector3(0, x, 0);
			Quaternion deltaRotation = Quaternion.Euler(vec * Time.deltaTime + transform.rotation.eulerAngles);
			
			if (!UnityEngine.VR.VRDevice.isPresent)
			{
				cameraRotX = Mathf.Clamp(cameraRotX + y, -90, 90);
				cameraTransform.localEulerAngles = new Vector3(cameraRotX, 0, 0);
			}
			
			rb.AddRelativeTorque(vec * rb.mass / 2);
			rb.MoveRotation(deltaRotation);
		}

		public void animate (float h, float v, float a) 
		{
			anim.SetFloat("HSpeed", h);
			anim.SetFloat("VSpeed", v);
			anim.SetFloat("AngularSpeed", a);
		}

		public void jump ()
		{
			if (grounded) rb.AddForce(Vector3.up * jumpStrength);
		}

		public override void die ()
		{
			// TODO: Die.
		}
	}
}
