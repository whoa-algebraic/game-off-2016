using UnityEngine;

public class PlayerActionControls : MonoBehaviour {
	private GameObject[] limbs;

	void Start() {
		GameObject bearFrontLimbPrefab = Managers.LimbManager.LimbPrefabs[0];
		GameObject bearBackLimbPrefab = Managers.LimbManager.LimbPrefabs[1];

		limbs = new GameObject[4];
		
		limbs[0] = Instantiate(bearFrontLimbPrefab);
		limbs[1] = Instantiate(bearFrontLimbPrefab);
		limbs[2] = Instantiate(bearBackLimbPrefab);
		limbs[3] = Instantiate(bearBackLimbPrefab);

		foreach(GameObject limb in limbs) {
			ConfigureLimbInstance(limb);
		}
	}

	void Update () {
		if (Input.GetAxis("Action1") > 0) {
			Debug.Log("Action1 button pressed");
			limbs[0].GetComponent<Limb>().Activate();
		}

		if (Input.GetAxis("Action2") > 0) {
			Debug.Log("Action2 button pressed");
			limbs[1].GetComponent<Limb>().Activate();
		}

		if (Input.GetButtonDown("Action3")) {
			Debug.Log("Action3 button pressed");
			limbs[2].GetComponent<Limb>().Activate();
		}

		if (Input.GetButtonDown("Action4")) {
			Debug.Log("Action4 button pressed");
			limbs[3].GetComponent<Limb>().Activate();
		}
	}

	private void ConfigureLimbInstance(GameObject limb) {
		limb.transform.SetParent(this.transform);
		limb.transform.localPosition = Vector3.zero;
		limb.transform.localScale = Vector3.one;
	}

}
