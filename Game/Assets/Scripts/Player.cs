using UnityEngine;
using System.Collections;
using fof;

public class Player : Actor
{
	PlayerControls controller;
	// Use this for initialization
	void Start ()
	{
		this.controller = new PlayerControls(this);
	}
	
	// Update is called once per frame
	void Update ()
	{
		this.controller.Update();
	}

	public void Move (float h, float v)
	{
		// Set the movement vector based on the axis input.
		this.movement.Set(h, 0f, v);
		
		// Normalise the movement vector and make it proportional to the speed per second.
		this.movement = movement.normalized * speed * Time.deltaTime;
		
		// Move the player to it's current position plus the movement.
		this.rigidbody.MovePosition(transform.position + transform.rotation * movement);
	}

	public void Turn (float x, float y)
	{
		Vector3 vec = new Vector3(y, x, 0);
		Quaternion deltaRotation = Quaternion.Euler(vec * Time.deltaTime + transform.rotation.eulerAngles);
		
		this.rigidbody.AddRelativeTorque(vec * this.rigidbody.mass / 2);
		this.rigidbody.MoveRotation(deltaRotation);
		
	}
}
