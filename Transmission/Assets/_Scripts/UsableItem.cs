using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableItem : MonoBehaviour {
    public delegate void Used(GameObject owner, GameObject usedOn);
    public event Used OnUsed;

    public void Use(GameObject user, GameObject usedOn){
        if(OnUsed != null){
            OnUsed(user, usedOn);
        }
    }
}
