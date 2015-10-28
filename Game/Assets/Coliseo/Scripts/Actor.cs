using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Coliseo
{
    public abstract class Actor : MonoBehaviour
    {
        protected float movementSpeed = 6f;
        protected bool isAlreadyDead = false;
		public Slider HealthSlider;                                 
		public Image damageImage;

        protected static int MAX_HEALTH = 100;
        protected int _health = MAX_HEALTH;
		public float flashSpeed = 5f;                               
		public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
		bool damaged;
        
        protected int DAMAGE = 30;
        public int health
        {
            get { return _health; }
            set
            {
                _health = value;
                if (_health <= 0) { this.die(); }
            }
        }

        public bool isDead()
        {
            return _health <= 0;
        }

        protected int moveSpeed;
        protected int attackStrength;

        public abstract void move (float forwardback, float leftright, float vertical);
        public abstract void turn (float x, float y);

        public void attack (Actor target)
        {
            target.health -= attackStrength;
        }

        public abstract void die ();

        protected TextMesh healthDisplay;

        Animator anim;
        SaberController saberCont;

        void Awake()
        {
            healthDisplay = transform.Find("Health").GetComponent<TextMesh>();
            updateHealth();
            anim = GetComponent<Animator>();
            saberCont = anim.GetBoneTransform(HumanBodyBones.RightHand).GetComponentInChildren<SaberController>();
        }

        void updateHealth()
        {
            healthDisplay.text = "" + health;
        }

        protected void checkHealth()
        {
            // If health is less than or equal to 0...
            if (health <= 0f)
            {
                // ... and if the player is not yet dead...
                if (!isAlreadyDead)
                    // ... call the PlayerDying function.
                    die();
                else
                {
                    // Otherwise, if the player is dead, call the PlayerDead and LevelReset functions.
                    //PlayerDead();
                }
            }
        }

        public void TakeDamage(int amount)
        {
			damaged = true;
            // Decrement the player's health by amount.
            health -= amount;
			//System.Console.Write(health);
            updateHealth();
			damageImage.color = flashColour;
			HealthSlider.value = health;
			damaged = false;
			damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            if (health < 0)
            {
                die();
            }
        }

        public void ResetLife()
        {
            isAlreadyDead = false;
            health = MAX_HEALTH;
            updateHealth();
        }

        void OnTriggerEnter(Collider other)
        {
            SaberController cont = other.GetComponentInParent<SaberController>();
            if (IsOpponent(other.gameObject) && cont != null && cont.attacking == true)
            {
                if (saberCont.blocking)
                {
                    cont.attacking = false;
                }
                else
                {
                    TakeDamage(DAMAGE);
                }

            }
        }

        string OpponentTag()
        {
            return (tag == "Player") ? "Enemy" : "Player";
        }

        // I know this could be Collider instead, but then it would be unclear what we really are comparing.
        bool IsOpponent(GameObject other)
        {
            return other.tag == (OpponentTag() + "Beam");
        }

        //a callback for calculating IK
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
                Transform skele = transform.Find("ScientistSkeleton");
                lookAt += ((skele != null) ? skele : transform.Find("Bip001")).forward;
                anim.SetLookAtPosition(lookAt);
                anim.SetLookAtWeight(1.0f);
            }
        }
    }
}
