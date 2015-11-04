using UnityEngine;
using System.Collections;

namespace Coliseo
{
    public abstract class Actor : MonoBehaviour
    {
        protected const uint MAX_HEALTH = 100;
        protected const uint attackStrength = 30;
        public const float moveSpeed = 6f;
        public const float jumpStrength = 300.0f;

        protected uint _health;

        protected TextMesh healthDisplay;

        protected Animator anim;
        protected SaberController saberCont;
        protected Rigidbody rb;
        protected GameObject saber;

        private bool _attacking = false;
        private bool _blocking = false;

        public bool attacking
        {
            get { return _attacking; }
            set
            {
                _attacking = value;
                if (attacking)
                {
                    saberCont.attackCollider.enabled = true; // These really belong in the animator, but whatever for now.
                    saberCont.blockCollider.enabled = false;
                }
            }
        }

        public bool blocking
        {
            get { return _blocking; }
            set
            {
                _blocking = value;
                if (blocking)
                {
                    saberCont.attackCollider.enabled = false;
                    saberCont.blockCollider.enabled = true;
                }
            }
        }


        public uint health
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
            get { return health == 0; }
        }

        bool damaged {
            get { return health != MAX_HEALTH; }
        }

        abstract public uint TakeDamage(uint amount);

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
            attacking = true;
        }

        void finishAttack()
        {
            attacking = false;
        }

        void initiateBlock()
        {
            blocking = true;
        }

        bool IsOpponent(Actor other)
        {
            return this.GetType() != other.GetType();
        }

        bool IsValidAttack(Actor other)
        {
            return other && IsOpponent(other) && other.attacking;
        }

        // I know this isn't as pretty as it probably could be.
        void OnTriggerEnter(Collider other)
        {
            Actor actor = other.transform.root.GetComponent<Actor>();
            if (IsValidAttack(actor))
            {
                actor.attacking = false;
                if (!blocking)
                {
                    TakeDamage(attackStrength);
                }
            }
        }
    }
}
