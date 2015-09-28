﻿using UnityEngine;
using System.Linq;
using fof;

namespace fof
{
	public class PlayerControls
	{
		private Player p;

	    // Movement speed
	    public float moveSpeed = 10.0F;

	    // Controller sensitivity
	    public float rotationSpeed = 100.0F;

	    // Mouse sensitivity
	    protected float horizontalSpeed = 4.0F;
	    protected float verticalSpeed = 4.0F;

		public PlayerControls (Player plerer)
		{
			this.p = plerer;
		}

	    public void Update ()
	    {

	        float translationX = Input.GetAxis("Left_X_Axis") * moveSpeed;
	        float translationZ = Input.GetAxis("Left_Y_Axis") * moveSpeed;

	        float rotationX = Input.GetAxis("Right_X_Axis") * rotationSpeed;
	        float rotationY = Input.GetAxis("Right_Y_Axis") * rotationSpeed;

	        float h = rotationSpeed * Input.GetAxis("Mouse X");
	        float v = rotationSpeed * Input.GetAxis("Mouse Y");

	        translationX = (Input.GetKey("a") ? -p.speed : translationX);
	        translationX = (Input.GetKey("d") ? p.speed : translationX);
	        translationZ = (Input.GetKey("s") ? -p.speed : translationZ);
	        translationZ = (Input.GetKey("w") ? p.speed : translationZ);

	        // Move the player around the scene.
	        p.Move(translationX, translationZ);

	        // Turn the player to face the mouse cursor.
//			Debug.Log (Input.GetJoystickNames ().Contains("Controller (Xbox One For Windows)"));
	        if (!Input.GetJoystickNames ().Contains ("Controller (Xbox One For Windows)")) {
				p.Turn (h, v);
//				Debug.Log("h: " + h + " v: " + v);
//				rotationX = h;
			} else {
				p.Turn(rotationX, rotationY);
			}

	        // Animate the player.
	        p.Animating(translationX, translationZ, rotationX);
	        
	    }
	}
}