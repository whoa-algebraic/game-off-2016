using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Room {
	public int roomId;
	public int width;
	public int height;

	public Dictionary<int, Door> doors;
	public HashSet<Door> doorsWithConnection;
	public HashSet<Door> doorsWithoutConnection;

	public int startX = 0;
	public int startY = 0;
	bool inMap = false;

	public GameObject prefab;

	Room() {
	} 

	public Room(GameObject _prefab) {
		doorsWithConnection = new HashSet<Door>();
		doorsWithoutConnection = new HashSet<Door>();

		prefab = _prefab;
		RoomDetails roomDetail = prefab.GetComponent<RoomDetails> ();
		Bounds bounds = GetBounds(prefab);
		width = (int) bounds.size.x;
		height = (int) bounds.size.y;

		List<GameObject> prefabDoors = roomDetail.GetDoors();
		doors = new Dictionary<int, Door> ();
		int i = 1;
		foreach(GameObject doorObject in prefabDoors) {
			Door door = new Door(
				i,
				((int) doorObject.transform.localPosition.x),
				-((int) doorObject.transform.localPosition.y),
				width,
				height
			);
			doors[i] = door;
			doorsWithoutConnection.Add(door);
			i ++;
		}
	}

	public void SetAsStartingPoint() {
		inMap = true;
	}

	public void SetDoorAsConnected(Door door) {
		doorsWithoutConnection.Remove (door);
		doorsWithConnection.Add (door);
	}

	public void SetDoorAsNotConnected(Door door) {
		doorsWithConnection.Remove (door);
		doorsWithoutConnection.Add (door);
	}

	public int LeftX() {
		return startX;
	}

	public int RightX() {
		return startX + width;
	}

	public int TopY() {
		return startY;
	}

	public int BottomY() {
		return startY + height;
	}

	public Door GetDoor(int doorId) {
		foreach (Door door in doors.Values) {
			if (door.doorId == doorId) {
				return door;
			}
		}
		throw new Exception ("Door (" + doorId + ") not found");
	}
		
	public Room clone() {
		Room room = new Room ();
		room.doors = new Dictionary<int, Door> ();
		int i = 1;
		room.doorsWithConnection = new HashSet<Door>();
		foreach (Door door in doorsWithConnection) {
			Door doorClone = door.clone ();
			room.doorsWithConnection.Add (doorClone);
			room.doors[i] = doorClone;
			i++;
		}
		room.doorsWithoutConnection = new HashSet<Door>();
		foreach (Door door in doorsWithoutConnection) {
			Door doorClone = door.clone ();
			room.doorsWithoutConnection.Add (doorClone);
			room.doors[i] = doorClone;
			i++;
		}
		room.width = width;
		room.height = height;
		room.prefab = prefab;
		room.startX = startX;
		room.startY = startY;

		return room;
	}

	public static void ConnectDoors(Room room, Door door, Room newRoom, Door otherDoor) {
		if (room.doorsWithConnection.Contains (door)) {
			throw new Exception ("Door is already connected to an other room");
		}
		door.connectedRoomId = newRoom.roomId;
		door.connectedDoorId = otherDoor.doorId;

		otherDoor.connectedRoomId = room.roomId;
		otherDoor.connectedDoorId = door.doorId;

		room.SetDoorAsConnected (door);
		newRoom.SetDoorAsConnected (otherDoor);

		newRoom.startX = room.startX + door.x - otherDoor.x;
		newRoom.startY = room.startY + door.y - otherDoor.y;			

	}

	public static bool AreRoomsOverlapping(Room room1, Room room2) {
		if (room1.LeftX () < room2.LeftX () && room1.RightX () > room2.LeftX() && room1.TopY () < room2.TopY () && room1.BottomY () > room2.TopY()) {
			Debug.Log ("Top-Left corner is overlapping");
			return true;
		}

		if (room1.LeftX () < room2.LeftX () && room1.RightX () > room2.LeftX() && room1.BottomY () < room2.TopY () && room1.BottomY () > room2.BottomY()) {
			Debug.Log ("Bottom-Left corner is overlapping");
			return true;
		}

		if (room1.LeftX () < room2.RightX () && room1.RightX () > room2.RightX() && room1.TopY () < room2.TopY () && room1.BottomY () > room2.TopY()) {
			Debug.Log ("Top-Right corner is overlapping");
			return true;
		}

		if (room1.LeftX () < room2.RightX () && room1.RightX () > room2.RightX() && room1.TopY () < room2.BottomY () && room1.BottomY () > room2.BottomY()) {
			Debug.Log ("Bottom-Right corner is overlapping");
			return true;
		}

		return false;
	}

	static Bounds GetBounds(GameObject prefab) {
		Bounds bounds = new Bounds(prefab.transform.position, Vector3.zero);
		foreach (Renderer renderer in prefab.GetComponentsInChildren<Renderer>()) {
			bounds.Encapsulate(renderer.bounds);
		}
		return bounds;
	}

	public override string ToString() {
		return base.ToString() + "(" + roomId + "): " + width + "x" + height + " (" + startX + "," + startY + ")";
	}
}