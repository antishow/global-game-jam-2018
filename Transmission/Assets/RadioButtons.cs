using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioButtons : MonoBehaviour {
    public delegate void Update(int? value);
    public event Update OnUpdate;

	private int? _value;
	public int? Value {
		get { return _value; }
		set {
			if(_value != value){
				_value = value;

				for(int i=0; i<Buttons.Length; i++){
					Buttons[i].Active = (i == _value);
				}

                if (OnUpdate != null)
                {
                    OnUpdate(_value);
                }
			}
		}
	}

	private ToggleButton[] Buttons;

	void Awake(){
		Buttons = GetComponentsInChildren<ToggleButton>();

        for (int i = 0; i < Buttons.Length; i++)
        {
			ToggleButton button = Buttons[i];
			button.OnToggled += OnChildToggle;
        }
	}

	public void OnChildToggle(ToggleButton button, bool Active){
		if(!Active){
			Value = null;
		} else {
			for(int i=0; i<Buttons.Length; i++){
				if(Buttons[i] == button){
					Debug.LogFormat("Set {0} to {1}", gameObject.name, i);
					Value = i;
					break;
				}
			}
		}
	}

}
