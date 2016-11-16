using UnityEngine;
using System.Collections;

public class CameraPositionController : MonoBehaviour {

	[Tooltip("Player object")]
	public GameObject player;

	private Vector3 offset;

	void Start () {
		offset = transform.position - player.transform.position;
	}

	void LateUpdate () {
		transform.position = player.transform.position + offset;
	}
}
