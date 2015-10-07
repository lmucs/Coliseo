using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    public float health = 100f;                         // How much health the player has left.
    public float resetAfterDeathTime = 5f;              // How much time from the player dying to the level reseting.
    private float DAMAGE = 30f;

    private float timer;                                // A timer for counting to the reset of the level once the player is dead.
    private bool playerDead;                            // A bool to show if the player is dead or not.

    TextMesh healthDisplay;

    void Awake()
    {
        healthDisplay = transform.Find("Health").GetComponent<TextMesh>();
        updateHealth();
    }

    // The angle needed to properly rotate the health toward the player.
    public float AngleToPlayer()
    {
        Vector3 localPosition = transform.position - PlayerMovement.player.transform.position;
        return 180f - (Mathf.Rad2Deg * Mathf.Atan2(localPosition.z, localPosition.x) + 90f);
    }


    void Update()
    {
        if(tag != "Player")
        {
            healthDisplay.transform.rotation = Quaternion.Euler(new Vector3(0, AngleToPlayer(), 0));
        }

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

    void updateHealth()
    {
        healthDisplay.text = "" + health;
    }

    void PlayerDying()
    {
        // The player is now dead.
        playerDead = true;
        gameObject.SetActive(false);
    }

    void PlayerDead()
    {
        // R.I.P.
    }

    public void TakeDamage(float amount)
    {
        // Decrement the player's health by amount.
        health -= amount;

        updateHealth();
        if (health < 0)
        {
            PlayerDying();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("My tag: " + tag);
        //Debug.Log("Other tag: " + other.tag);
        //Debug.Log("IsOpponent(): " + IsOpponent(other.gameObject));
        if (IsOpponent(other.gameObject))
        {
            TakeDamage(DAMAGE);
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
}