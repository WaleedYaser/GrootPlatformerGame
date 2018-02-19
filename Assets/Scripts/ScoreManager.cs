using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public Text textScore;

	void Start () {
		textScore.text = "Score: 0";
	}
	
	public void UpdateScore (int score) {
		textScore.text = "Score: " + (score / 3) * 10;
	}
}
