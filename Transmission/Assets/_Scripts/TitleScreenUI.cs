﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenUI : MonoBehaviour {
	public void StartNewGame(){
		SceneManager.LoadScene("Game");
	}

	public void QuitGame(){
		Application.Quit();
	}
}
