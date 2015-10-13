using UnityEngine;
using System.Collections;

public class SaberController : MonoBehaviour {


    Animator anim;
    Transform beamTransform;

    public bool attacking = false;
    public bool blocking = false;

    public Collider attackCollider;
    public Collider blockCollider;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        //gameObject.SetActive(false);
        beamTransform = transform.Find("Beam");
        attackCollider = beamTransform.GetComponent<BoxCollider>();
        blockCollider = beamTransform.GetComponent<CapsuleCollider>();
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("Updating sabercontroller");
	    if(Input.GetButtonDown("Button B") || Input.GetKeyDown(KeyCode.F))
        {
            anim.SetBool("Extended", !anim.GetBool("Extended"));
        }
        if(attacking)
        {
            attackCollider.enabled = true;
            blockCollider.enabled = false;
        }
        if(blocking)
        {
            attackCollider.enabled = false;
            blockCollider.enabled = true;
        }
        
	}


}
