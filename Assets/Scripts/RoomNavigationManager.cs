using UnityEngine;
using System.Collections;
using Tiled2Unity;

public class RoomNavigationManager : MonoBehaviour {
    public GameObject PlayerGameObject;
    public GameObject[] RoomPrefabs;

    private GameObject activeRoomPrefab;

    void Awake() {
        activeRoomPrefab = Instantiate(RoomPrefabs[0]);
        float xPos = GetActiveRoomWidth() / 2;
        float yPos = GetActiveRoomHeight() / 2;
        activeRoomPrefab.transform.position = new Vector3(xPos * -1, yPos, 0);
    }

    // Change room prefab
    public void ChangeRoom(int doorId) {
        // remove current room prefab from scene
        Destroy(activeRoomPrefab);

        // get connected room

        // spawn connected room and save reference

        // find position of connected door

        // determin if player should be on the left or right side of the door's location

        // position player slightly off of door's location
    }

    public float GetActiveRoomWidth() {
        return activeRoomPrefab.GetComponent<TiledMap>().NumTilesWide;
    }

    public float GetActiveRoomHeight() {
        return activeRoomPrefab.GetComponent<TiledMap>().NumTilesHigh;
    }
}
 