using UnityEngine;
using System.Collections;

public class Spawn : MonoBehaviour {

    public GameObject enemy;
    Character character;
    int killCount = 0;

	// Use this for initialization
	void Start () {
        enemy = Instantiate<GameObject>(enemy);
        character = enemy.GetComponent<Character>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(character.IsDead())
        {
            killCount++;
            Debug.Log("Enemy Killed. Total killed: " + killCount);
            enemy.transform.position = transform.position;
            enemy.transform.rotation = transform.rotation;
            character.ResetLife();
            enemy.SetActive(true);
        }
	}
}
