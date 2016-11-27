using UnityEngine;
using System.Collections;

public class Managers : MonoBehaviour {
    public static RoomNavigationManager RoomNavigationManager { get; set; }
	public static LimbManager LimbManager { get; set; }
	public static LevelFactory LevelFactory { get; set; }

	void Awake () {
        RoomNavigationManager = GetComponent<RoomNavigationManager>();
		LimbManager = GetComponent<LimbManager>();
		LevelFactory = GetComponent<LevelFactory> ();
	}
}
