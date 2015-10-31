using UnityEngine;
using System.Collections;

namespace Coliseo
{
    public abstract class Actor : MonoBehaviour
    {
        protected const uint MAX_HEALTH = 100;
        protected const uint attackStrength = 30;
        public const float moveSpeed = 6f;
        public const float jumpStrength = 50.0f;

        protected uint _health;

        protected TextMesh healthDisplay;

        protected Animator anim;
        protected SaberController saberCont;
        protected Rigidbody rb;
        protected GameObject saber;

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
    }
}
