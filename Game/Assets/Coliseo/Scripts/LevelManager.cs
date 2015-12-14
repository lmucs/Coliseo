using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	public GameObject Scoreboard;

	// Use this for initialization
	public void LoadScene (string name) {
		Application.LoadLevel (name);
	}
	
	// Update is called once per frame
	public void QuitGame () {
		Application.Quit ();
	}

	public void HighScores (){
		Scoreboard.SetActive (true);
	}

	public void BackToMainMenu (){
		Scoreboard.SetActive (false);
	}
}
