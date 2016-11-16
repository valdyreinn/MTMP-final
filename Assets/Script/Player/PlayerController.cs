using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[Tooltip("Movement speed")]
	public float speed;

	[Tooltip("Character controller")]
	public CharacterController controller;

	private Vector3 position;

	private readonly float RAY_DISTANCE = 1000;

	void Start () {
		position = transform.position;
	}
	
	void Update () {
		if(Input.GetMouseButton (0)) {
			locatePosition ();
		} else if (Input.GetMouseButton (1)) {
						
		}
		moveToPosition ();
	}

	void locatePosition() {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		if (Physics.Raycast (ray, out hit, RAY_DISTANCE)) {
			position = hit.point;
			position.y = 0;
		}
	}

	void moveToPosition() {
		if (Vector3.Distance (transform.position, position) > 0.5) {
			Quaternion newRotation = Quaternion.LookRotation (position - transform.position, Vector3.forward);
			newRotation.x = newRotation.z = 0;

			transform.rotation = newRotation;
			controller.SimpleMove (transform.forward * speed);
		}
	}
}
