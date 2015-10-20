using UnityEngine;
using System.Collections;

namespace fof
{
	public abstract class Actor : MonoBehaviour
	{
		protected Vector3 movement;                   // The vector to store the direction of the player's movement.
		protected Animator anim;                      // Reference to the animator component.
		new protected Rigidbody rigidbody;          // Reference to the player's rigidbody.

		void Awake()
		{
			// Set up references.
			anim = GetComponent<Animator>();
			rigidbody = GetComponent<Rigidbody>();
		}

		// Currently disabled as it affects movement and rotation speed
		public void Animating(float h, float v, float a)
		{
			// Create a boolean that is true if either of the input axes is non-zero.
			bool walking = h != 0f || v != 0f;
			
			// Tell the animator whether or not the player is walking.
			anim.SetFloat("HSpeed", h);
			anim.SetFloat("VSpeed", v);
			anim.SetFloat("AngularSpeed", a);
		}

		public abstract void Move (float h, float v);
	}
}