using UnityEngine;
using System.Collections;

public class PlayerAnimationController : MonoBehaviour {

	public PlayerController player;

	public void endAttacking() {
		player.endAttacking ();
	}
}
