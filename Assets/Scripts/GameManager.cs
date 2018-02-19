using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Animator panelGameOverAnim;
	public Text gameScore;
	public Text menuScore;

	public void GameOver()
	{
		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled = false;
		panelGameOverAnim.SetTrigger("Open");
		menuScore.text = gameScore.text;
		gameScore.gameObject.SetActive(false);
	}

	public void PlayAgain()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}
}
