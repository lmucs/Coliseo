using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    public float health = 100f;                         // How much health the player has left.
    public float resetAfterDeathTime = 5f;              // How much time from the player dying to the level reseting.

    private float timer;                                // A timer for counting to the reset of the level once the player is dead.
    private bool playerDead;                            // A bool to show if the player is dead or not.


    void Update()
    {
        // If health is less than or equal to 0...
        if (health <= 0f)
        {
            // ... and if the player is not yet dead...
            if (!playerDead)
                // ... call the PlayerDying function.
                PlayerDying();
            else
            {
                // Otherwise, if the player is dead, call the PlayerDead and LevelReset functions.
                PlayerDead();
            }
        }
    }


    void PlayerDying()
    {
        // The player is now dead.
        playerDead = true;
    }


    void PlayerDead()
    {
        
    }

    public void TakeDamage(float amount)
    {
        // Decrement the player's health by amount.
        health -= amount;
    }
}