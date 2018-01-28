using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueCube : MonoBehaviour {

	private UsableItem usableItem;

	void Awake(){
		usableItem = GetComponent<UsableItem>();
		usableItem.OnUsed += HandleUse;
	}

	void HandleUse(GameObject usedBy, GameObject usedOn){
		Debug.LogFormat("{0} screwed with the blue cube!", usedBy.name);
	}
}
