using UnityEngine;

public class PlayerActionControls : MonoBehaviour {
	public GameObject BearLimbPrefab; // TODO make LimbPrefabManager
	private GameObject[] limbs;

	void Start() {
		limbs = new GameObject[1];
		limbs[0] = Instantiate(BearLimbPrefab);
		limbs[0].transform.SetParent(this.transform);
		limbs[0].transform.localPosition = Vector3.zero;
		limbs[0].transform.localScale = Vector3.one;
	}

	void Update () {
		if (Input.GetButtonDown("Action1")) {
			Debug.Log("Action1 button pressed");
			limbs[0].GetComponent<Limb>().Activate();
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
