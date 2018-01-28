using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Module : MonoBehaviour {
	public Color color;

	// Use this for initialization
	void Start () {
		Renderer renderer = GetComponent<Renderer>();

		for(int i=0; i<renderer.materials.Length; i++){
			Material mat = renderer.materials[i];
			Debug.Log(mat.name);
			if(mat.name == "Module_Shell (Instance)"){
				mat.SetColor("_Color", color);
			}
		}
	}
}
