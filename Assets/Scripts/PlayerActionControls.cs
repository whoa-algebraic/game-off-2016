using UnityEngine;
using System.Collections;

public class PlayerActionControls : MonoBehaviour {
	
	void Update () {
		if(Input.GetButtonDown("Action1")) {
			Debug.Log("Action1 button pressed");
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
