using UnityEngine;
using System.Collections;
using Coliseo;

public class Spawn : MonoBehaviour {

    public GameObject enemy;
    Actor actor;
    int killCount = 0;

	// Use this for initialization
	void Start () {
        enemy = Instantiate<GameObject>(enemy);
        enemy.transform.position = transform.position;
        actor = enemy.GetComponent<Actor>();
	}
	
	// Update is called once per frame
	void Update () {
	    if(actor.isDead())
        {
            killCount++;
            Debug.Log("Enemy Killed. Total killed: " + killCount);
            enemy.transform.position = transform.position;
            enemy.transform.rotation = transform.rotation;
            actor.ResetLife();
            enemy.SetActive(true);
        }
	}
}
