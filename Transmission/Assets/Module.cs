using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour {
	public Color color;
	public string name;
    private UsableItem usable;
	private HoldableItem holdable;
	private Rigidbody rb;
	private Collider collider;
	private ModuleBay bay;
    void Awake()
    {
        usable = GetComponent<UsableItem>();
		holdable = GetComponent<HoldableItem>();
		rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();

        usable.OnUsed += HandleUse;
		holdable.OnGrabbed += HandleGrab;
    }

	// Use this for initialization
	void Start () {
		Renderer renderer = GetComponent<Renderer>();

		for(int i=0; i<renderer.materials.Length; i++){
			Material mat = renderer.materials[i];
			if(mat.name == "Module_Shell (Instance)"){
				mat.SetColor("_Color", color);
			}
		}
	}

	void HandleGrab(GameObject owner){
		if(bay){
			bay.module = null;
			bay = null;
		}
	}

	void HandleUse(GameObject user, GameObject usedOn){
		if(usedOn){
			Debug.LogFormat("{0} used {1} on {2}", user.name, name, usedOn.name);
		}
		ModuleBay mb = usedOn.GetComponent<ModuleBay>();
		if(mb && !mb.module){
			holdable.Drop(user);
			mb.module = this;
			bay = mb;
			rb.isKinematic = true;
			collider.isTrigger = true;
		}
	}
}
