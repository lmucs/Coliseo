// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using UnityEngine;
using Coliseo;

namespace Coliseo{

	public class GameOverManager : MonoBehaviour
	{
		public static GameOverManager manager;
		public float restartDelay = 5f;         // Time to wait before restarting the level
		
		
		Animator anim;                          // Reference to the animator component.
		float restartTimer;                     // Timer to count up to restarting the level
		bool restart = false;
		
		
		void Awake ()
		{
			// Set up the reference.
			anim = GetComponent <Animator> ();
			manager = this;
		}

		public void Update() {
			if (restart) {
				// .. increment a timer to count up to restarting.
				restartTimer += Time.deltaTime;
				// .. if it reaches the restart delay...
				if (restartTimer >= restartDelay) {
					// .. then reload the currently loaded level.
					Debug.Log ("Loading Level");
					restart = false;
					Application.LoadLevel (Application.loadedLevel);
				}
			}
		}
		
		
		public void Gameover ()
		{
			// If the player has run out of health...

				// ... tell the animator the game is over.
				anim.SetTrigger ("GameOver");
				
				restart = true;
		}
	}
}