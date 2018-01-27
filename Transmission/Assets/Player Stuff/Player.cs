using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public class Player : MonoBehaviour {
	Rigidbody rb;
	Camera camera;

	GameObject target = null;

	public float Speed = 10.0f;
	public float MouseSensitivityX = 1.0f;
	public float MouseSensitivityY = 1.0f;
	public float RollSpeed = 1.0f;
	public float ArmLength = 1.0f;
	public GameObject Hand;
	HoldableItem heldItem;
	UsableItem usableItem;


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
		rb = GetComponent<Rigidbody>();
		rb.freezeRotation = true;
		camera = Camera.main;
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

		GameObject objectInCrosshairs = GetObjectInCrosshairs();

		if(objectInCrosshairs != target){
            if (target != null)
            {
                target.GetComponent<Outline>().enabled = false;
				target = null;
            }

            if (objectInCrosshairs != null)
            {
                objectInCrosshairs.GetComponent<Outline>().enabled = true;
                target = objectInCrosshairs;
            }
		}


		if(Input.GetMouseButtonDown(0)){
			if(heldItem == null){
                if (objectInCrosshairs != null)
                {
                    HoldableItem holdable = objectInCrosshairs.GetComponent<HoldableItem>();
                    if (holdable)
                    {
                        holdable.PickUp(Hand);
                        heldItem = holdable;
                        usableItem = heldItem.GetComponent<UsableItem>();
                    } else {
						UsableItem usable = objectInCrosshairs.GetComponent<UsableItem>();
						if(usable){
							usable.Use(gameObject);
						}
					}
                }
			} else {
                if (usableItem)
                {
                    usableItem.Use(gameObject);
                }
			}
		}

		if(heldItem != null && Input.GetMouseButtonDown(1)){
			heldItem.Drop(Hand);
			heldItem = null;
		}
	}

	void OnDrawGizmos(){
		if(!camera) return;
		Vector3 cameraLine = camera.transform.forward * ArmLength;
		Gizmos.DrawLine(camera.transform.position, camera.transform.position + cameraLine);
	}

	GameObject GetObjectInCrosshairs(){
		RaycastHit hit;
		LayerMask layerMask = LayerMask.GetMask("Interactive");

		GameObject ret = null;
		Ray ray = new Ray(camera.transform.position, camera.transform.forward);
		if(Physics.Raycast(ray, out hit, ArmLength, layerMask)){
			ret = hit.collider.gameObject;
		}

		return ret;
	}
}
