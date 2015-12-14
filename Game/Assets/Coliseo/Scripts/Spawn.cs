using UnityEngine;
using System.Collections;
using Coliseo;

namespace Coliseo
{
    public class Spawn : MonoBehaviour
    {

        public GameObject enemy;
        Actor actor;
        public static int killCount = 0;
        
        void Start()
        {
            enemy = Instantiate<GameObject>(enemy);
            enemy.transform.position = transform.position;
            actor = enemy.GetComponent<Actor>();
        }
        
        void Update()
        {
            if (actor.isDead)
            {
				killCount+=10;
				ScoreManager.score = killCount * 10;
                Debug.Log("Enemy Killed. Total killed: " + killCount);
                enemy.transform.position = transform.position;
                enemy.transform.rotation = transform.rotation;
                actor.ResetLife();
                enemy.SetActive(true);
            }
        }
    }
}