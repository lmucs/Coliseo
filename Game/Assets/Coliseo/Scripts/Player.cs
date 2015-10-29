using UnityEngine;
using System.Collections;
using Coliseo;

namespace Coliseo
{

    public class Player : Actor
    {
        private Rigidbody rb;
        private Controls controls;

        private float distToGround;
        private Transform cameraTransform;
        private float cameraRotX = 0;

        private static GameObject player;
        
        // For jumps
        public float jumpStrength = 50.0f;

        public bool grounded
        {
            get
            {
                // FIXME: There's a better way to do this involving colliders I think
                return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1F);
            }
        }

        public static Vector3 position
        {
            get
            {
                return player.transform.position;
            }
        }

        // Unity's initalization
        void Start ()
        {
            if (VRCenter.IsVREnabled())
            {
                VRCenter.Setup();
            }
            player = gameObject;
            anim = GetComponent<Animator>(); // I don't think commenting this out is a problem.
            rb = GetComponent<Rigidbody>();
            distToGround = GetComponent<Collider>().bounds.extents.y;
            cameraTransform = anim.GetBoneTransform(HumanBodyBones.Head).Find("CameraRig/Camera");
            cameraRotX = cameraTransform.localEulerAngles.x;

            // This probably doesn't belong here, but oh well. TODO move somewhere else.
            GameObject saber = anim.GetBoneTransform(HumanBodyBones.RightHand).Find("LSaber/Beam").gameObject;
            saber.tag = "Player" + saber.tag;
            saberCont.IsPlayerSword = true;

            controls = new Controls(this);
        }
        
        // We shouldn't have to update here. Controls should move us around with its FixedUpdate()
        void FixedUpdate ()
        {
            controls.FixedUpdate ();
        }

        public override void move (float h, float v, float ignored) // FIXME: should do movement in 3D, instead of just 2D
        {
            // Set the movement vector based on the axis input.
            Vector3 movement = new Vector3(h, 0f, v);
            
            // Normalise the movement vector and make it proportional to the speed per second.
            movement = movement.normalized * moveSpeed * Time.deltaTime;
            
            // Move the player to it's current position plus the movement.
            rb.MovePosition(transform.position + transform.rotation * movement);
        }

        // Now that the camera is directly on the head, we can, for the time being, 
        // have the controller directly move the head to look down. For now.
        public override void turn (float x, float y)
        {
            Vector3 vec = new Vector3(0, x, 0);
            Quaternion deltaRotation = Quaternion.Euler(vec * Time.deltaTime + transform.rotation.eulerAngles);
            
            if (!VRCenter.IsVREnabled())
            {
                cameraRotX = Mathf.Clamp(cameraRotX + y, -90, 90);
                cameraTransform.localEulerAngles = new Vector3(cameraRotX, 0, 0);
            }
            
            rb.AddRelativeTorque(vec * rb.mass / 2);
            rb.MoveRotation(deltaRotation);
        }
        
        // This + GetTrigger belongs in controls, but its here for now. TODO B4 MERGE COMPLETE
        bool rightTriggerActive = false;
        //bool leftTriggerActive = false;

        bool triggerMethodDefault = true;
        int LEFT_TRIGGER = 0;
        int RIGHT_TRIGGER = 1;

        float GetTrigger(string trigger)
        {
            if (triggerMethodDefault)
            {
                return Input.GetAxis(trigger);
            }
            else
            {
                return Input.GetAxis("Desktop_" + trigger);
            }
        }

        bool blocking = false;

        public void animate (float h, float v, float a, float b) 
        {
            anim.SetFloat("Input X", h);
            anim.SetFloat("Input Z", v);
            anim.SetFloat("Rotation X", a);
            anim.SetFloat("Rotation Y", b);
            
            // For now, but soon this should be in controls.
            bool rightTriggerDown = (GetTrigger("RightTrigger") == 1);
            bool leftTriggerDown = (GetTrigger("LeftTrigger") == 1);

            if (leftTriggerDown || Input.GetMouseButton(1))
            {
                anim.SetTrigger("BeginBlock");
                //anim.SetBool("Blocking", true);
            }
            else
            {
                anim.SetBool("Blocking", false);
            }
            //anim.SetBool("Blocking", leftTriggerDown || Input.GetMouseButton(1));
            bool inSwing = anim.GetCurrentAnimatorStateInfo(0).IsTag("swordswing");

            if (!rightTriggerActive && rightTriggerDown && !inSwing || Input.GetMouseButtonDown(0))
            {
                rightTriggerActive = true;
                anim.SetTrigger("AttackDownTrigger");
            }
            rightTriggerActive = rightTriggerDown;

            // I'm leaving this here for when we eventually add the turning animation
            // anim.SetFloat("AngularSpeed", a);
        }

        public void jump ()
        {
            if (grounded) rb.AddForce(Vector3.up * jumpStrength);
        }

        public override void die ()
        {
            gameObject.SetActive(false);
        }
    }
}
