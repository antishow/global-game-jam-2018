using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchButton : MonoBehaviour {
	public Mainframe mainframe;

    private UsableItem usableItem;

    void Awake()
    {
        usableItem = GetComponent<UsableItem>();
        usableItem.OnUsed += OnPress;
    }

	void OnPress(GameObject presser, GameObject usedOn){
		mainframe.CheckState();
	}
}
