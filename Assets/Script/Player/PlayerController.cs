﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	[Tooltip("Movement speed")]
	public float speed;
	[Tooltip("Character controller")]
	public CharacterController controller;
    [Tooltip("Character animator")]
    public Animator animator;

	private Vector3 movementPosition;
	private Vector3 attackPosition;
    private float distance;
	private State state;

	private readonly float RAY_DISTANCE = 1000;

	void Start () {
		movementPosition = transform.position;
		state = State.IDLE;
	}
	
	void Update () {
		if(Input.GetMouseButtonDown (0) && isNotAttacking ()) {
			Debug.Log ("LeftMouseButton - start");
			locatePosition (out movementPosition);
			stateRunning ();
			Debug.Log ("LeftMouseButton - end");
		} else if (Input.GetMouseButtonDown (1) && isNotAttacking ()) {
			Debug.Log ("RightMouseButton - start");
			locatePosition (out attackPosition);
			stateAttacking ();
			Debug.Log ("RightMouseButton - end");
		}

		if (isRunning())
			moveToPosition ();
	}

	bool isRunning() {
		return state == State.RUNNING;
	}

	bool isNotRunning() {
		return !isRunning ();
	}

	bool isAttacking() {
		return state == State.ATTACKING;
	}

	bool isNotAttacking() {
		return !isAttacking ();
	}

	void stateRunning() {
		state = State.RUNNING;
		animator.SetBool("IsRunning", true);
		animator.SetBool("IsAttacking", false);
	}

	void stateAttacking() {
		Debug.Log ("StateAttacking - start");
		state = State.ATTACKING;
		animator.SetBool("IsRunning", false);
		animator.SetBool("IsAttacking", true);
		Debug.Log ("StateAttacking - end");
	}

	void stateIdle() {
		state = State.IDLE;
		animator.SetBool("IsRunning", false);
		animator.SetBool("IsAttacking", false);
	}

	void locatePosition(out Vector3 position) {
		position = transform.position;
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

		if (Physics.Raycast (ray, out hit, RAY_DISTANCE)) {
			position = hit.point;
			position.y = 0;

			Quaternion newRotation = Quaternion.LookRotation(position - transform.position, Vector3.forward);
			newRotation.x = newRotation.z = 0;

			transform.rotation = newRotation;
		}
	}

	void moveToPosition() {
	    getDistanceToPosition();
		if (distance > 0.5) {
			controller.SimpleMove(transform.forward * speed);
	    } else {
			stateIdle ();
	    }
	}

    void getDistanceToPosition()
    {
        distance = Vector3.Distance(transform.position, movementPosition);
    }

	public void endAttacking() {
		Debug.Log ("EndAttacking - start");
		stateIdle ();
		Debug.Log ("EndAttacking - stop");
	}

	enum State {
		IDLE,
		ATTACKING,
		RUNNING,
		DEAD
	}
}
