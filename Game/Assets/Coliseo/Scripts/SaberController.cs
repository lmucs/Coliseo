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

        public bool attacking
        {
            get { return _attacking; }
            set
            {
                _attacking = value;
                if (attacking)
                {
                    attackCollider.enabled = true;
                    blockCollider.enabled = false;
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

        public void ToggleBeam()
        {
            anim.SetBool("Extended", !anim.GetBool("Extended"));
        }
    }
}