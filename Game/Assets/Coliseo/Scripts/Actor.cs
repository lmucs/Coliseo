using UnityEngine;
using System.Collections;

namespace Coliseo
{
    public abstract class Actor : MonoBehaviour
    {
        protected const int MAX_HEALTH = 100;
        protected const int attackStrength = 30;
        public const float moveSpeed = 6f;
        public const float jumpStrength = 50.0f;

        protected int _health;

        protected TextMesh healthDisplay;

        protected Animator anim;
        protected SaberController saberCont;
        protected Rigidbody rb;
        protected GameObject saber;

        public int health
        {
            get { return _health; }
            set
            {
                _health = value;

                if (isDead)
                {
                    this.die();
                }

                healthDisplay.text = "" + health;
            }
        }

        public bool isDead
        {
            get { return health <= 0; }
        }

        bool damaged {
            get { return health != MAX_HEALTH; }
        }

        abstract public int TakeDamage(int amount);

        // I don't think this will ever be used, at least not while the colliders are set up as they are.
        public void attack(Actor target)
        {
            target.TakeDamage(attackStrength);
        }

        public abstract void die();

        public abstract void move (float forwardback, float leftright, float vertical);
        public abstract void turn (float x, float y);
        
        void Awake()
        {
            healthDisplay = transform.Find("Health").GetComponent<TextMesh>();
            anim = GetComponent<Animator>();
            Transform rightHand = anim.GetBoneTransform(HumanBodyBones.RightHand);
            saber = rightHand.Find("LSaber/Beam").gameObject;
            saberCont = rightHand.GetComponentInChildren<SaberController>();
            rb = GetComponent<Rigidbody>();
            health = MAX_HEALTH;
        }

        public void ResetLife()
        {
            health = MAX_HEALTH;
        }

        void initiateAttack()
        {
            saberCont.attacking = true;
        }

        void finishAttack()
        {
            saberCont.attacking = false;
        }

        void initiateBlock()
        {
            saberCont.blocking = true;
        }

        bool IsOpponent(SaberController cont)
        {
            return saberCont.isPlayerSword != cont.isPlayerSword;
        }

        bool IsValidAttack(SaberController cont)
        {
            return cont && IsOpponent(cont) && cont.attacking;
        }

        // I know this isn't as pretty as it probably could be.
        void OnTriggerEnter(Collider other)
        {
            SaberController cont = other.GetComponentInParent<SaberController>();
            if (IsValidAttack(cont))
            {
                cont.attacking = false;
                if (saberCont.blocking)
                {
                    return;
                }
                TakeDamage(attackStrength);
            }
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
    }
}
