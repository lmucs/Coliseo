using UnityEngine;
using System.Collections;

namespace Coliseo
{
    public class SaberController : MonoBehaviour
    {

        private Animator anim;

        public Collider attackCollider;
        public Collider blockCollider;

        private bool _attacking = false;
        private bool _blocking = false;

        public bool inAttack
        {
            get { return _attacking; }
            set
            {
                _attacking = value;
                if (inAttack)
                {
                    attackCollider.enabled = true;
                    blockCollider.enabled = false;
                }
            }
        }

        public bool inBlock
        {
            get { return _blocking; }
            set
            {
                _blocking = value;
                if (inBlock)
                {
                    attackCollider.enabled = false;
                    blockCollider.enabled = true;
                }
            }
        }

        public bool isPlayerSword;
        
        void Awake()
        {
            anim = GetComponent<Animator>();
            attackCollider = GetComponentInChildren<BoxCollider>();
            blockCollider = GetComponentInChildren<CapsuleCollider>();
        }

        // These are only here because the stupid animator doesn't like accessors.
        public bool attacking;
        public bool blocking;
        
        void Update()
        {
            inAttack = attacking;
            inBlock = blocking;
        }

        public void ToggleBeam()
        {
            anim.SetBool("Extended", !anim.GetBool("Extended"));
        }
    }
}