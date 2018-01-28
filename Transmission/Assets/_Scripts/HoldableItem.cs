using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldableItem : MonoBehaviour {
	private Rigidbody rigidbody;
	private Transform parent;
	private Collider collider;

	void Awake(){
		rigidbody = GetComponent<Rigidbody>();
		collider = GetComponent<Collider>();
		parent = transform.parent;
	}

    public delegate void Thrown(GameObject thrower);
    public event Thrown OnThrown;

    public delegate void Grabbed(GameObject owner);
    public event Grabbed OnGrabbed;

	public void PickUp(GameObject owner){
		Debug.LogFormat("Pick up {0}", gameObject.name);

		rigidbody.isKinematic = true;
		transform.parent = owner.transform;
		transform.rotation = Quaternion.identity;
		transform.localPosition = Vector3.zero;
		collider.enabled = false;

		if(OnGrabbed != null){
			OnGrabbed(owner);
		}
	}

	public void Drop(GameObject thrower){
		Debug.LogFormat("Drop {0}", gameObject.name);
		transform.parent = parent;
		rigidbody.isKinematic = false;
		collider.enabled = true;

		if(OnThrown != null){
			OnThrown(thrower);
		}
	}
}
