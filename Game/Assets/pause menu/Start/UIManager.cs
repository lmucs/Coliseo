//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//
//public class UIManager : MonoBehaviour {
//
//	public GameObject pausePanel;
//	public bool isPaused;
//	// Use this for initialization
//	void Start () {
//		isPaused = false;
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		if (isPaused) {
//			PauseGame (true);
//		} else {
//			PauseGame (false);
//		}
//
//		if (Input.GetButtonDown ("start") || Input.GetButtonDown("desktop_start")) {
//			switchPause ();
//		}
//	
//	}
//
//	void PauseGame(bool state){
//		if (state) {
//			pausePanel.SetActive (true);
//			Time.timeScale = 0.0f;
//		} else {
//			Time.timeScale = 1.0f;
//			pausePanel.SetActive(false);
//		}
//	}
//
//	public void switchPause(){
//		if (isPaused) {
//			isPaused = false;
//		} else {
//			isPaused = true;
//		}
//	}
//}
