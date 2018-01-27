using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableItem : MonoBehaviour {
    public delegate void Used(GameObject owner);
    public event Used OnUsed;

    public void Use(GameObject user){
        if(OnUsed != null){
            OnUsed(user);
        }
    }
}
