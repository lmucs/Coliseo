using UnityEngine;
using System.Collections;
using Coliseo;
using System;

public class Enemy : Actor {

    Rigidbody rb;
    private float MIN_DIST = 1.5f;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        //anim = GetComponent<Animator>(); // I don't think commenting this out is a problem.
        GameObject saber = anim.GetBoneTransform(HumanBodyBones.RightHand).Find("LSaber/Beam").gameObject;
        saber.tag = "Enemy" + saber.tag;
        saberCont.IsPlayerSword = false;
    }

    // The angle needed to properly rotate the health toward the player.
    public float AngleToPlayer()
    {
        Vector3 localPosition = transform.position - Player.position;
        return 180f - (Mathf.Rad2Deg * Mathf.Atan2(localPosition.z, localPosition.x) + 90f);
    }

    public override int TakeDamage(int amount)
    {
        health -= amount;
        return amount;
    }

    // Update is called once per frame
    void Update () {
        if (/*tag != "Player"*/ healthDisplay != null)
        {
            healthDisplay.transform.rotation = Quaternion.Euler(new Vector3(0, AngleToPlayer(), 0));
        }
        
        Vector3 playerLoc = Player.position; // For now, try to move "close" to this, and face it.
        float dist = Vector3.Distance(playerLoc, transform.position);
        float moveZ = 0;

        if (dist > MIN_DIST)
        {
            moveZ = moveSpeed;
            Vector3 movement = new Vector3(0, 0, moveSpeed * Time.deltaTime);

            // Move the player to it's current position plus the movement.
            rb.MovePosition(transform.position + transform.rotation * movement);
        }
        else
        {
            anim.SetTrigger("AttackDownTrigger");
        }
        anim.SetFloat("Input Z", moveZ); 
        rb.MoveRotation(Quaternion.Euler(new Vector3(0, 180 + AngleToPlayer(), 0)));
        // Also, beam should be limited to hurting only during an attack. Just sayin. Can / should do in the animations.
    }

    public override void move(float h, float v, float ignored) // FIXME: should do movement in 3D, instead of just 2D
    {
        
    }

    // Now that the camera is directly on the head, we can, for the time being, 
    // have the controller directly move the head to look down. For now.
    public override void turn(float x, float y)
    {
        
    }

    public override void die()
    {
        gameObject.SetActive(false);
    }
}
