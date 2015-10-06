using UnityEngine;
using System.Collections;

public class SaberController : MonoBehaviour {


    Animator anim;
    Transform beamTransform;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        //gameObject.SetActive(false);
        beamTransform = transform.Find("Beam");
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("Updating sabercontroller");
	    if(Input.GetButtonDown("Button B") || Input.GetKeyDown(KeyCode.F))
        {
            anim.SetBool("Extended", !anim.GetBool("Extended"));
        }
        
	}
}
