using UnityEngine;
using System.Collections;
using Tiled2Unity;

public class RoomNavigationManager : MonoBehaviour {
    public GameObject PlayerGameObject;
	public GameObject MapContainer;
    public GameObject[] RoomPrefabs;

	public Map map;

    private GameObject activeRoomPrefab;

    void Awake() {
		map = GetComponent<LevelFactory> ().generate (RoomPrefabs);
		SetActiveRoom (map.activeRoom.prefab);
    }

    // Change room prefab
    public void ChangeRoom(int doorId) {
        // remove current room prefab from scene
        Destroy(activeRoomPrefab);

        // get connected room
		Room oldActiveRoom = map.activeRoom;
		map.moveToOtherRoom(doorId);

		// get connectedDoor 
		int newDoorId = oldActiveRoom.GetDoor(doorId).connectedDoorId;

        // spawn connected room and save reference
		SetActiveRoom (map.activeRoom.prefab);

        // find position of connected door
		Door door = map.activeRoom.GetDoor(newDoorId);

        // position player slightly off of door's location
		float xPos = - GetActiveRoomWidth() / 2;
		float yPos = GetActiveRoomHeight() / 2;
		int adjustedDoorX = door.x;
		int adjustedDoorY = door.y + 1;
		if (door.position == Door.Position.LEFT) {
			adjustedDoorX += 4;
		} else if (door.position == Door.Position.RIGHT) {
			adjustedDoorX -= 4;		
		} else if (door.position == Door.Position.TOP) {
			adjustedDoorX -= 3;
			adjustedDoorY -= 4;
		} else {
			adjustedDoorY += 4;
		}
		PlayerGameObject.transform.position = new Vector3(xPos + adjustedDoorX, yPos - adjustedDoorY, 0);
    }

    public float GetActiveRoomWidth() {
        return activeRoomPrefab.GetComponent<TiledMap>().NumTilesWide;
    }

    public float GetActiveRoomHeight() {
        return activeRoomPrefab.GetComponent<TiledMap>().NumTilesHigh;
    }

	void SetActiveRoom (GameObject prefab) {
		activeRoomPrefab = Instantiate(map.activeRoom.prefab);
		float xPos = GetActiveRoomWidth() / 2;
		float yPos = GetActiveRoomHeight() / 2;
		activeRoomPrefab.transform.position = new Vector3(xPos * -1, yPos, 0);

		Room activeRoom = map.activeRoom;
		MapContainer.transform.position = new Vector3 (
			-MapContainer.transform.position.x,
			-MapContainer.transform.position.y,
			0
		);
		MapContainer.transform.position = new Vector3 (
			-(activeRoom.startX + activeRoom.width / 2) * 0.05f,
			-(activeRoom.startY - activeRoom.height / 2) * 0.05f,
			0
		);

	}
}
 