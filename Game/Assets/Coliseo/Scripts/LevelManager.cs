using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Coliseo;

public class LevelManager : MonoBehaviour 
{
	public GameObject Scoreboard;
	public GameObject LoginPanel;
	public UnityEngine.UI.Text HighScoreText;
    public UnityEngine.UI.Text UsernameText;
    public UnityEngine.UI.Text PasswordText;
	public UnityEngine.UI.Text ErrorText;

    // Use this for initialization
    public void LoadScene (string name) 
	{
		Application.LoadLevel (name);
	}
	
	// Update is called once per frame
	public void QuitGame () 
	{
		Application.Quit ();
	}

	public void HighScores ()
	{
		Scoreboard.SetActive (true);
	}

	public void BackToMainMenu ()
	{
		Scoreboard.SetActive (false);
	}

	public void LoginPanelOn ()
	{
		LoginPanel.SetActive (true);
	}
	
	public void LoginPanelOff ()
	{
		Debug.Log ("logging in " + UsernameText.text + " with pass " + PasswordText.text);
		WWWForm scoreForm = new WWWForm();
		var headers = scoreForm.headers;
		headers["Authorization"] = "Basic " + System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(UsernameText.text + ":" + PasswordText.text));
		WWW www = new WWW("http://localhost:3000/api/v1/auth", null,  headers);
		while (!www.isDone) { }
		if (www.responseHeaders.Count > 0) // 
		{
			foreach (KeyValuePair<string, string> entry in www.responseHeaders)
			{
				Debug.Log(entry.Value + "=" + entry.Key);
			}
		}
		if (www.responseHeaders ["STATUS"] == "HTTP/1.1 200 OK") {
			Player.username = UsernameText.text;
			Player.password = PasswordText.text;
			LoginPanel.SetActive (false);
		} else {
			ErrorText.text = "Username/Password combo is invalid.";
		}
		//LoginPanel.SetActive (false);
	}

	public void FetchScoreboard()
	{
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
		int rank = 1;
		foreach (ScoreItem score in s.Items)
		{
			string rankStr = rank++ + "";
			string scoreStr = score.score + "";
            HighScoreText.text += string.Format("{0}\t\t{1}\t\t{2}\n", rankStr.PadLeft(4, ' '), score.username, scoreStr);
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
