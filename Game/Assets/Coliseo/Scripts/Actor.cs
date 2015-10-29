using UnityEngine;
using System.Collections;

namespace Coliseo
{
    public abstract class Actor : MonoBehaviour
    {
        protected const int MAX_HEALTH = 100;
        protected const int attackStrength = 30;
        public const float moveSpeed = 6f;

        protected int _health;

        protected TextMesh healthDisplay;

        protected Animator anim;
        protected SaberController saberCont;

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
            saberCont = anim.GetBoneTransform(HumanBodyBones.RightHand).GetComponentInChildren<SaberController>();
            health = MAX_HEALTH;
        }

        public void ResetLife()
        {
            health = MAX_HEALTH;
        }

        bool IsOpponent(SaberController cont)
        {
            return saberCont.IsPlayerSword != cont.IsPlayerSword;
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
                if (saberCont.blocking)
                {
                    cont.attacking = false;
                    return;
                }
                TakeDamage(attackStrength);
            }
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
                lookAt += (transform.Find("ScientistSkeleton") ?? transform.Find("Bip001")).forward;

                anim.SetLookAtPosition(lookAt);
                anim.SetLookAtWeight(1.0f);
            }
        }
    }
}
