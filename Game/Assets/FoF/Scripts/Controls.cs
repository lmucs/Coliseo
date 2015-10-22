using UnityEngine;
using System.Collections;
using FoF;

namespace FoF
{
	public class Controls : Object
	{
		private Player p;

		public Controls (Player player)
		{
			p = player;
		}

		public float joystickMoveSensitvity = 10.0F;
		
		// Controller sensitivity
		public float joystickRotationSpeedHoriz = 100.0F;
		
		// Controller sensitivity
		public float joystickRotationSpeedVert = 10.0F;
		
		// Mouse sensitivity
		public float mouseHorizontalSensitvity = 10.0F;
		
		// Update is called once per frame
		public void FixedUpdate ()
		{
			float translationX = Input.GetAxis("Left_X_Axis") * joystickMoveSensitvity;
			float translationZ = Input.GetAxis("Left_Y_Axis") * joystickMoveSensitvity;
			
			float rotationX = Input.GetAxis("Right_X_Axis") * joystickRotationSpeedHoriz;
			float rotationY = Input.GetAxis("Right_Y_Axis") * joystickRotationSpeedVert;
			
			float h = joystickRotationSpeedHoriz * Input.GetAxis("Mouse X") * mouseHorizontalSensitvity;
			float v = joystickRotationSpeedVert * Input.GetAxis("Mouse Y");

			translationX = (Input.GetKey("a") ? -p.speed : translationX);
			translationX = (Input.GetKey("d") ? p.speed : translationX);
			translationZ = (Input.GetKey("s") ? -p.speed : translationZ);
			translationZ = (Input.GetKey("w") ? p.speed : translationZ);
			
			// Move the player around the scene.
			p.move(translationX, translationZ, 0f);
			
			// Turn the player according to controller input
			p.turn(rotationX, rotationY);
			
			// Turn the player to face the mouse cursor.
			if(Input.GetJoystickNames().Length == 0 || string.IsNullOrEmpty(Input.GetJoystickNames()[0]))
			{
				p.turn(h, v);
				rotationX = h;
			}
			
			if(Input.GetButton("Button A"))
			{
				p.jump();
			}
			
			// Animate the player.
			p.animate(translationX, translationZ, rotationX);
		}
	}
}