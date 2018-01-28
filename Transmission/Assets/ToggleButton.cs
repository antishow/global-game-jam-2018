using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour {
    public delegate void Toggled(ToggleButton button, bool Active);
    public event Toggled OnToggled;
	private bool _active;
	public bool Active{
		get {
			return _active;
		}
		set {
			_active = value;
            if (_active)
            {
                material.SetColor("_EmissionColor", OnLight);
            }
            else
            {
                material.SetColor("_EmissionColor", OffLight);
            }
		}
	}

	public void SetActive(bool active){
		_active = active;
	}

	private UsableItem usableItem;
	private Material material;

	public Color OnLight;
	public Color OffLight;

	void Awake(){
		usableItem = GetComponent<UsableItem>();
		usableItem.OnUsed += OnPress;
		material = GetComponent<Renderer>().material;
		Active = false;
	}

	void OnPress(GameObject presser){
		Active = !Active;
		if(OnToggled != null){
			OnToggled(this, Active);
		}
	}
}
