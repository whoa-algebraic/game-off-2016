using UnityEngine;

public class PlayerActionControls : MonoBehaviour {

	public Limb Limb1;
	
	void Update () {
		if(Input.GetButtonDown("Action1")) {
			Debug.Log("Action1 button pressed");
			Limb1.Activate();
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

}
