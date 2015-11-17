using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Coliseo;

namespace Coliseo
{

    public class Player : Actor
    {

        private static GameObject player;

        private Controls controls;
        
        private Transform cameraTransform;
        private float cameraRotX = 0;

        public Slider HealthSlider;
        public Image damageImage;
        public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

        public float flashSpeed = 1f;
        private float dt = 1f;

        void Start()
        {
            if (VRCenter.VREnabled)
            {
                VRCenter.Setup();
            }
            player = gameObject;
            cameraTransform = anim.GetBoneTransform(HumanBodyBones.Head).Find("CameraRig/Camera");
            cameraRotX = cameraTransform.localEulerAngles.x;
            controls = new Controls(this);
        }
        
        void FixedUpdate()
        {
            playDamagedAnimation();
            controls.FixedUpdate();
        }

        public override uint TakeDamage(uint amount)
        {
            dt = 0;
            if(amount > health)
            {
                amount = health;
            }
            health -= amount;
            return health;
        }

        void playDamagedAnimation()
        {
            damageImage.color = flashColour;
            HealthSlider.value = health;
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, dt);
            if (dt < 1)
            {
                dt += Time.deltaTime / flashSpeed;
            }
        }

        public bool grounded
        {
            // FIXME: There's a better way to do this involving colliders I think
            get { return Physics.Raycast(transform.position, -Vector3.up, 0.01F); }
        }

        public static Vector3 position
        {
            get { return player.transform.position; }
        }
        
        public override void move (float h, float v, float ignored) // FIXME: should do movement in 3D, instead of just 2D
        {
            // Set the movement vector based on the axis input.
            Vector3 movement = new Vector3(h, 0f, v);
            
            // Make movement vector proportional to the speed per second.
            movement *= moveSpeed * Time.deltaTime;
            
            // Move the player to it's current position plus the movement.
            rb.MovePosition(transform.position + transform.rotation * movement);
        }

        // Now that the camera is directly on the head, we can, for the time being, 
        // have the controller directly move the head to look down. For now.
        public override void turn (float x, float y)
        {
            Vector3 vec = new Vector3(0, x, 0);
            Quaternion deltaRotation = Quaternion.Euler(vec * Time.deltaTime + transform.rotation.eulerAngles);
            
            if (!VRCenter.VREnabled)
            {
                cameraRotX = Mathf.Clamp(cameraRotX - y, -90, 90);
                cameraTransform.localEulerAngles = new Vector3(cameraRotX, 0, 0);
            }
            
            rb.AddRelativeTorque(vec * rb.mass / 2);
            rb.MoveRotation(deltaRotation);
        }
        
        public void animate (float h, float v, float a, float b) 
        {
            anim.SetFloat("Input X", h);
            anim.SetFloat("Input Z", v);
            anim.SetFloat("Rotation X", a);
            anim.SetFloat("Rotation Y", b);
        }

        // A callback for calculating IK
        void OnAnimatorIK()
        {
            if (anim)
            {
                // The next 5 lines are probably unnecessary, but I'm leaving them in for now.
                // TODO: Check if necessary.
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);

                anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);

                // Mostly solves head bobbing issue, except when idle. However is currently model-dependent.
                // TODO: Make model independent. (Can possibly go to root then grab parent)
                // TODO2: Also may need update to support AdvVRTracker.cs
                Vector3 lookAt = anim.GetBoneTransform(HumanBodyBones.Head).position;
                lookAt += (transform.Find("ScientistSkeleton") ?? transform.Find("Bip001")).forward;

                anim.SetLookAtPosition(lookAt);
                anim.SetLookAtWeight(1.0f);
            }
        }

        public void attack()
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("swordswing"))
            {
                anim.SetTrigger("AttackDownTrigger");
            }
        }

        public void block(bool blocking)
        {
            // Most of the conditions in this method belong in animation transitions 
            // but the animation controller is about to be changed anyways...
            if (blocking)
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("swordswing"))
                {
                    anim.SetTrigger("BeginBlock");
                } else
                {
                    anim.ResetTrigger("BeginBlock");
                    anim.SetBool("Blocking", false);
                }
            } else
            {
                anim.SetBool("Blocking", false);
            }
        }

        public void jump ()
        {
            if (grounded) rb.AddForce(Vector3.up * jumpStrength);
        }

        public override void die ()
        {
			GameOverManager.manager.Gameover();
            //gameObject.SetActive(false);
        }

        public void ToggleBeam()
        {
            saberCont.ToggleBeam();
        }

    }
}
