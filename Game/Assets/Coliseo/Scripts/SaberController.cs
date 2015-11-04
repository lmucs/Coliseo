using UnityEngine;
using System.Collections;

namespace Coliseo
{
    public class SaberController : MonoBehaviour
    {

        private Animator anim;

        public Collider attackCollider;
        public Collider blockCollider;
        
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