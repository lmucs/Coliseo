using UnityEngine;
using System.Collections;

public class SaberController : MonoBehaviour {


    Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        //gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log("Updating sabercontroller");
	    if(Input.GetButtonDown("Button B") || Input.GetKeyDown("f"))
        {
            anim.SetBool("Extended", !anim.GetBool("Extended"));
        }
	}
}
