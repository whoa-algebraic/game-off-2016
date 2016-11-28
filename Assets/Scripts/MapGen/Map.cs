using System;
using System.Collections.Generic;

public class Map {

	public Room activeRoom;
	public Dictionary<int, Room> rooms;

	public Map (Room startingRoom, List<Room> rooms) {
		this.activeRoom = startingRoom;
		this.rooms = new Dictionary<int, Room> ();
		foreach (Room room in rooms) {
			this.rooms.Add (room.roomId, room);
		}
	}

	public void moveToOtherRoom(int doorId) {
		this.activeRoom = rooms [this.activeRoom.GetDoor (doorId).connectedRoomId];
	}
}
