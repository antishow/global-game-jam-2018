using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	Camera camera;
	Rigidbody rb;

	public float Speed = 10.0f;
	public float MouseSensitivityX = 1.0f;
	public float MouseSensitivityY = 1.0f;
	public float RollSpeed = 1.0f;

private Quaternion originalRotation;


	Vector2 GetMovementInput(){
		return new Vector2(
			Input.GetAxis("Horizontal"),
			Input.GetAxis("Vertical")
		);
	}

	Vector2 GetMouseInput(){
		return new Vector2(
			Input.GetAxis("Mouse X") * MouseSensitivityX,
			Input.GetAxis("Mouse Y") * MouseSensitivityY
		);
	}

	float GetRotationInput(){
		float ret = 0;
		if(Input.GetKey(KeyCode.Q)){
			ret -= 1.0f;
		}
		if(Input.GetKey(KeyCode.E)){
			ret += 1.0f;
		}

		return ret;
	}

	float GetVerticalInput(){
        float ret = 0;
        if (Input.GetKey(KeyCode.Space))
        {
            ret += 1.0f;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            ret -= 1.0f;
        }

        return ret;
	}


	// Use this for initialization
	void Start () {
		camera = Camera.main;
		rb = GetComponent<Rigidbody>();

		originalRotation = transform.localRotation;
		rb.freezeRotation = true;
	}

	// Update is called once per frame
	void Update () {
		Vector2 input = GetMovementInput();
		Vector2 mouseInput = GetMouseInput();
		float rollInput = GetRotationInput();
		float vInput = GetVerticalInput();

        Quaternion lookX = Quaternion.AngleAxis(mouseInput.x, Vector3.up);
		Quaternion lookY = Quaternion.AngleAxis(mouseInput.y, -Vector3.right);
		Quaternion lookRoll = Quaternion.AngleAxis(rollInput, -Vector3.forward);

		transform.localRotation = transform.localRotation * lookX * lookY * lookRoll;

		Vector3 move = (transform.forward * input.y) + (transform.right * input.x) + (transform.up * vInput);

		rb.velocity = move * Speed;
	}
}
