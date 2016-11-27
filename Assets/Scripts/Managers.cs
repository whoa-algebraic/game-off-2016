using UnityEngine;
using System.Collections;

public class Managers : MonoBehaviour {
    public static RoomNavigationManager RoomNavigationManager { get; set; }
	public static LimbManager LimbManager { get; set; }

	void Awake () {
        RoomNavigationManager = GetComponent<RoomNavigationManager>();
		LimbManager = GetComponent<LimbManager>();
	}
}
