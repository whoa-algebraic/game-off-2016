using System.Collections.Generic;
using UnityEngine;

public class RoomDetails : MonoBehaviour {
    private List<GameObject> Doors;

    void Start() {
        Doors = new List<GameObject>();
        int doorIndex = 1;

        for(int i = 0; i < transform.childCount; i++) {
            GameObject child = transform.GetChild(i).gameObject;
            if (child.tag == "door") {
                Doors.Add(child);
                child.GetComponent<DoorDetails>().ID = doorIndex;
                doorIndex++;
            }
        }
    }

    public List<GameObject> GetDoors() {
		Doors = new List<GameObject>();
		int doorIndex = 1;

		for(int i = 0; i < transform.childCount; i++) {
			GameObject child = transform.GetChild(i).gameObject;
			if (child.tag == "door") {
				Doors.Add(child);
				child.GetComponent<DoorDetails>().ID = doorIndex;
				doorIndex++;
			}
		}
        return Doors;
    }
}
