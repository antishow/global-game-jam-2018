using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mainframe : MonoBehaviour {
	ModuleBay[] moduleBays;
	RadioButtons[] radioButtons;

	public Module[] moduleAnswer;
	public int[] stationAnswer;

	void Awake(){
		moduleBays = GetComponentsInChildren<ModuleBay>();
		radioButtons = GetComponentsInChildren<RadioButtons>();

		for(int m=0; m<moduleBays.Length; m++){
			ModuleBay mb = moduleBays[m];
			mb.OnUpdate += OnModuleBayUpdate;
		}

		for(int r=0; r<radioButtons.Length; r++){
			RadioButtons rb = radioButtons[r];
			rb.OnUpdate += OnRadioButtonUpdate;
		}
	}

	void OnModuleBayUpdate(Module module){
		CheckState();
	}

	void OnRadioButtonUpdate(int? value){
		CheckState();
	}

	void CheckState(){
		Debug.Log("MAINFRAME STATE CHANGED. DID YOU WIN?");

		int correct = 0;

		for(int i=0; i<4; i++){
			Module m = moduleBays[i].module;
			int? p = radioButtons[i].Value;

			if(p != null){
				p = p+1;
			}

			if(m != null && p != null){
				Debug.LogFormat("Module Bay {0}: {1} Power Station {2}", i, m.name, p);
				Debug.LogFormat("   Should be {0} and {1}", moduleAnswer[i].name, stationAnswer[i]);
			}
			if(m == moduleAnswer[i]){
				Debug.Log("CORRECT MODULE!");
				correct++;
			}
			if(p == stationAnswer[i]){
				Debug.Log("CORRECT POWER STATION!");
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
		Debug.Log(" YOU WIN!!!!");
	}

	void Fail(){
        //Debug.Log(" YOU LOSE!!!!");
	}
}
