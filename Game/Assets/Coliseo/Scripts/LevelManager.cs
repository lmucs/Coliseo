using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	// Use this for initialization
	public void LoadScene (string name) {
		Application.LoadLevel (name);
	}
	
	// Update is called once per frame
	public void QuitGame () {
		Application.Quit ();
	}
}
