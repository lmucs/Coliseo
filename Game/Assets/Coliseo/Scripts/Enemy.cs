using UnityEngine;
using System.Collections;
using Coliseo;
using System;

namespace Coliseo
{
    public class Enemy : Actor
    {

        private const float MIN_DIST = 1.5f;
        private const float TOO_CLOSE = 0.5f;
        
        void Start()
        {
            saberCont.isPlayerSword = false;
        }

        // Update is called once per frame
        void Update()
        {
            healthDisplay.transform.rotation = Quaternion.Euler(new Vector3(0, AngleToPlayer(), 0));

            Vector3 playerLoc = Player.position; // For now, try to move "close" to this, and face it.
            float dist = Vector3.Distance(playerLoc, transform.position);
            float moveZ = 0;

            if (dist > MIN_DIST)
            {
                moveZ = moveSpeed;
                Vector3 movement = new Vector3(0, 0, moveSpeed * Time.deltaTime);
                rb.MovePosition(transform.position + transform.rotation * movement);
            }
            else if (dist < TOO_CLOSE)
            {
                moveZ = -moveSpeed;
                Vector3 movement = new Vector3(0, 0, moveSpeed * Time.deltaTime);
                rb.MovePosition(transform.position + transform.rotation * movement);
            }
            else
            {
                anim.SetTrigger("AttackDownTrigger");
            }
            anim.SetFloat("Input Z", moveZ);
            rb.MoveRotation(Quaternion.Euler(new Vector3(0, 180 + AngleToPlayer(), 0)));
        }

        // The angle needed to properly rotate the health toward the player.
        public float AngleToPlayer()
        {
            Vector3 localPosition = transform.position - Player.position;
            return 180f - (Mathf.Rad2Deg * Mathf.Atan2(localPosition.z, localPosition.x) + 90f);
        }

        public override int TakeDamage(int amount)
        {
            health -= amount;
            return amount;
        }

        public override void die()
        {
            gameObject.SetActive(false);
        }

        // Leaving this stuff till the AI update, which will come after combat-perfection probably
        public override void move(float h, float v, float ignored) // FIXME: should do movement in 3D, instead of just 2D
        {
            throw new NotSupportedException();
        }
        
        public override void turn(float x, float y)
        {
            throw new NotSupportedException();
        }
    }
}