using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleBay : MonoBehaviour {
    public delegate void Update(Module module);
    public event Update OnUpdate;
	private Module _module;
	public Module module {
		get { return _module; }
		set {
			_module = value;
			if(_module != null){
                _module.transform.parent = transform;
                _module.transform.rotation = Quaternion.Euler(-90.0f, 0, 0);
                _module.transform.localPosition = Vector3.zero;
                _module.transform.localScale = new Vector3(1, 0.5f, 2);
			}

			if(OnUpdate != null){
				OnUpdate(module);
			}
		}
	}
}
