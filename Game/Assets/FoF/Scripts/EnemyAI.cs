using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    Rigidbody enemyRigidbody;
    Character character;
    Animator anim;
    float MIN_DIST = 1.5f;

    float speed = 4f; // make this static and accessible to all characters

    // Use this for initialization
    void Start () {
        character = GetComponent<Character>();
        enemyRigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        GameObject saber = anim.GetBoneTransform(HumanBodyBones.RightHand).Find("LSaber/Beam").gameObject;
        saber.tag = "Enemy" + saber.tag;
    }

    // Update is called once per frame
    void Update () {
        Vector3 playerLoc = PlayerMovement.player.transform.position; // For now, try to move "close" to this, and face it.
        float dist = Vector3.Distance(playerLoc, transform.position);
        float moveZ = 0;

        if(dist > MIN_DIST)
        {
            //  Debug.Log("Moving toward player");
            moveZ = speed;
            Vector3 movement = new Vector3(0, 0, speed * Time.deltaTime);

            // Move the player to it's current position plus the movement.
            enemyRigidbody.MovePosition(transform.position + transform.rotation * movement);
        } else
        {
            anim.SetTrigger("AttackDownTrigger");
        }
        anim.SetFloat("Input Z", moveZ);
        enemyRigidbody.MoveRotation(Quaternion.Euler(new Vector3(0, 180 + character.AngleToPlayer(), 0)));
        // Also, beam should be limited to hurting only during an attack. Just sayin. Can / should do in the animations.
	}
}
