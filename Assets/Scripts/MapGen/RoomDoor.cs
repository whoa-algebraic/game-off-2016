using System;

public class RoomDoor {

	public Room room;
	public Door door;

	public RoomDoor (Room room, Door door) {
		this.room = room;
		this.door = door;
	}

	public override string ToString() {
		return base.ToString() + ": roomId = " + room.roomId + "; door.position = " + door.position;
	}
}

