using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screwdriver : MonoBehaviour {
	private UsableItem usable;

	void Awake(){
		usable = GetComponent<UsableItem>();
		usable.OnUsed += HandleUse;
	}

	void HandleUse(GameObject usedBy, GameObject usedOn){
		Debug.LogFormat("Screwdriver was used by {0}", usedBy.name);
	}
}
