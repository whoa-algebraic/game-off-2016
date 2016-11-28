using System;
using System.Collections.Generic;

public class MapState {

	private static Random rnd = new Random();

	public List<Room> rooms;
	List<RoomDoor> openDoors;

	public List<RoomDoor> applicableConnections;
	public RoomDoor lastDoor;

	MapState() {
		rooms = new List<Room> ();
		openDoors = new List<RoomDoor> ();
	}

	public MapState (Room firstRoom) {
		rooms = new List<Room> ();
		openDoors = new List<RoomDoor> ();
		AddRoom (firstRoom);
	}

	public int RoomCount() {
		return rooms.Count;
	}

	public int OpenDoorCount() {
		return openDoors.Count;
	}

	public void AddRoom(Room room) {
		room.roomId = rooms.Count;
		rooms.Add (room);
		foreach (Door door in room.doorsWithoutConnection) {
			openDoors.Add(new RoomDoor(room, door));
		}
	}

	public RoomDoor RandomOpenDoor() {
		int index = rnd.Next (openDoors.Count);
		RoomDoor roomDoor = openDoors[index];
		lastDoor = roomDoor;
		openDoors.Remove (roomDoor);
		return roomDoor;
	}

	public void ReAddLastDoor() {
		openDoors.Add (lastDoor);
		lastDoor = null;
	}

	public MapState clone() {
		MapState state = new MapState ();
		foreach (Room room in rooms) {
			state.AddRoom (room.clone());
		}
		return state;
	}

	public MapState Apply(RoomDoor roomDoor, RoomDoor otherRoom) {
		otherRoom.room.roomId = rooms.Count;
		this.applicableConnections.Remove (otherRoom);
		MapState newState = new MapState ();
		foreach (Room room in rooms) {
			if (room.roomId == roomDoor.room.roomId) {
				Room clonedRoom = roomDoor.room.clone ();
				Door clonedDoor = clonedRoom.GetDoor(roomDoor.door.doorId);
				Room.ConnectDoors (clonedRoom, clonedDoor, otherRoom.room, otherRoom.door);
				newState.AddRoom (clonedRoom);
			} else {
				newState.AddRoom (room.clone());
			}
		}
		newState.AddRoom (otherRoom.room);
		return newState;
	}

	public bool Overlaps(Room newRoom) {
		foreach (Room room in rooms) {
			if (Room.AreRoomsOverlapping (room, newRoom)) {
				return true;
			}
		}
		return false;
	}

	public override string ToString() {
		String result = base.ToString() + ": \n" + 
			"Rooms: [";
		foreach (Room room in rooms) {
			result += room.ToString () + ",";
		}
		result += "]\nOpenDoors: [";
		foreach (RoomDoor roomDoor in openDoors) {
			result += roomDoor.ToString () + ",";
		}
		result += "]";
		return result;
	}

}

