using UnityEngine;
using System.Collections;
using fof;

public class Player : Actor
{
	public float speed = 6f;
	public float movementSpeed = 10f;

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

	public override void Move(float h, float v)
	{
		// Set the movement vector based on the axis input.
		movement.Set(h, 0f, v);
		
		// Normalise the movement vector and make it proportional to the speed per second.
		movement = movement.normalized * speed * Time.deltaTime;
		
		// Move the player to it's current position plus the movement.
		this.rigidbody.MovePosition(transform.position + transform.rotation * movement);
	}

	public void Turn (float x, float y)
	{

//		Debug.Log ("turnX: " + x + ", turnY: " + y);
		Vector3 vec = new Vector3(y, x, 0f);
		Quaternion deltaRotation = Quaternion.Euler(vec * Time.deltaTime + transform.rotation.eulerAngles);
		
		this.rigidbody.AddRelativeTorque(vec * this.rigidbody.mass / 2f);
		this.rigidbody.MoveRotation(deltaRotation);
	}
}
