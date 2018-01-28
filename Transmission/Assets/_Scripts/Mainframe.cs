using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainframe : MonoBehaviour {
	ModuleBay[] moduleBays;
	RadioButtons[] radioButtons;

	public Module[] moduleAnswer;
	public int[] stationAnswer;

	void Awake(){
		moduleBays = GetComponentsInChildren<ModuleBay>();
		radioButtons = GetComponentsInChildren<RadioButtons>();
	}

	public void CheckState(){
		int correct = 0;

		for(int i=0; i<4; i++){
			Module m = moduleBays[i].module;
			int? p = radioButtons[i].Value;

			if(p != null){
				p = p+1;
			}

			if(m == moduleAnswer[i]){
				correct++;
			}
			if(p == stationAnswer[i]){
				correct++;
			}
		}

		if(correct >= 8){
			Pass();
		} else {
			Fail();
		}
	}

	void Pass(){
		SceneManager.LoadScene("WIN");
	}

	void Fail(){
        SceneManager.LoadScene("LOSE");
	}
}
