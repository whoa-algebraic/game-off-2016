using UnityEngine;

public class PlayerActionControls : MonoBehaviour {
	private Limb[] limbs;

	void Start() {
		limbs = new Limb[4];
		limbs[0] = new BearLimb();
		limbs[1] = new BearLimb();
		limbs[2] = new BearLimb();
		limbs[3] = new BearLimb();
	}

	void Update () {
		handleCooldowns();

		if (Input.GetButtonDown("Action1")) {
			Debug.Log("Action1 button pressed");
			limbs[0].Activate();
		}

		if (Input.GetButtonDown("Action2")) {
			Debug.Log("Action2 button pressed");
		}

		if (Input.GetButtonDown("Action3")) {
			Debug.Log("Action3 button pressed");
		}

		if (Input.GetButtonDown("Action4")) {
			Debug.Log("Action4 button pressed");
		}
	}

	void handleCooldowns() {
		foreach(Limb limb in limbs) {
			limb.HandleCooldown();
		}
	}

}
