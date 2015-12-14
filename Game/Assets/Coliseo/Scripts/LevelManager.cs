using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class LevelManager : MonoBehaviour {
	public GameObject Scoreboard;
	public UnityEngine.UI.Text HighScoreText;

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

	public void FetchScoreboard(){
		XmlSerializer ser = new XmlSerializer (typeof(ScoreList));
		WWW scoreRequest = new WWW ("http://localhost:3000/api/v1/scores");

		while (!scoreRequest.isDone)
		{
			HighScoreText.text = string.Format("{0}", scoreRequest.progress);
		}

		ScoreList s = new ScoreList();
		using (TextReader r = new StringReader(scoreRequest.text))
		{
			s = (ScoreList)ser.Deserialize(r);
		}

		HighScoreText.text = "";
		foreach (ScoreItem score in s.Items)
		{
			HighScoreText.text += string.Format ("{0}\t\t{1}\n", score.username, score.score);
		}
	}

	[XmlRoot("scores")]
	public class ScoreList
	{
		public ScoreList() {Items = new List<ScoreItem>();}

		[XmlElement("scores")]
		public List<ScoreItem> Items { get; set; }
	}
	
	public class ScoreItem
	{
		[XmlElement("score")]
		public int score { get; set; }

		[XmlElement("username")]
		public string username { get; set; }
	}
}
