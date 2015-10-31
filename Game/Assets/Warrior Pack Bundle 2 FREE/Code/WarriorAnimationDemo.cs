using UnityEngine;
using System.Collections;

public class WarriorAnimationDemo : MonoBehaviour {

	public Animator animator;

	float rotationSpeed = 30;
	Vector3 inputVec;

	bool isMoving;
	bool isStunned;
	
	//Warrior types
	public enum Warrior{Karate, Ninja, Brute, Sorceress, Knight, Mage, Archer, TwoHanded};

	public Warrior warrior;
	
	void Update()
	{
		//Get input from controls
		float z = Input.GetAxisRaw("Horizontal");
		float x = -(Input.GetAxisRaw("Vertical"));
		inputVec = new Vector3(x, 0, z);

		//Apply inputs to animator
		animator.SetFloat("Input X", z);
		animator.SetFloat("Input Z", -(x));

		if (x != 0 || z != 0 )  //if there is some input
		{
			//set that character is moving
			animator.SetBool("Moving", true);
			isMoving = true;
			animator.SetBool("Running", true);
		}
		else
		{
			//character is not moving
			animator.SetBool("Moving", false);
			animator.SetBool("Running", false);
			isMoving = false;
		}

		if (Input.GetButtonDown("Fire1"))
		{
			animator.SetTrigger("Attack1Trigger");
			if (warrior == Warrior.Brute)
				StartCoroutine (COStunPause(1.2f));
			else if (warrior == Warrior.Sorceress)
				StartCoroutine (COStunPause(1.2f));
			else
				StartCoroutine (COStunPause(.6f));
		}

		UpdateMovement();  //update character position and facing
	}

	public IEnumerator COStunPause(float pauseTime)
	{
		isStunned = true;
		yield return new WaitForSeconds(pauseTime);
		isStunned = false;
	}
	
	void RotateTowardsMovementDir()  //face character along input direction
	{
		if (inputVec != Vector3.zero)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(inputVec), Time.deltaTime * rotationSpeed);
		}
	}

	float UpdateMovement()
	{
		Vector3 motion = inputVec;  //get movement input from controls

		//reduce input for diagonal movement
		motion *= (Mathf.Abs(inputVec.x) == 1 && Mathf.Abs(inputVec.z) == 1)?.7f:1;
		
		RotateTowardsMovementDir();  //if not strafing, face character along input direction

		return inputVec.magnitude;  //return a movement value for the animator, not currently used
	}

	void OnGUI () 
	{
		if (GUI.Button (new Rect (25, 85, 100, 30), "Attack1")) 
		{
			animator.SetTrigger("Attack1Trigger");

			if (warrior == Warrior.Brute || warrior == Warrior.Sorceress)  //if character is Brute or Sorceress
				StartCoroutine (COStunPause(1.2f));
			else
				StartCoroutine (COStunPause(.6f));
		}
	}
}