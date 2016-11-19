using UnityEngine;

public class Limb {

	public float coolDown;

	private bool isDisabled;
	private float CDTimer;

	public virtual bool Activate() { // must return a bool so that inheriting class can escape if on cool down
		if(isDisabled) { return false; }

		isDisabled = true;
		CDTimer = 0;

		Debug.Log("Limb Activate called");
		return true;
	}

	public void HandleCooldown() {
		if(!isDisabled) { return; }

		CDTimer += Time.deltaTime;

		if(CDTimer >= coolDown) {
			isDisabled = false;
			Debug.Log("cooldown reached, action no longer disabled");
		}
	}

}
