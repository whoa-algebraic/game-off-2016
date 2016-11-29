using System;

public class ApplicableOperator	{

	public RoomDoor sourceRoomDoor;
	public RoomDoor connectionRoomDoor;

	public ApplicableOperator(RoomDoor roomDoor, RoomDoor otherRoom) {
		sourceRoomDoor = roomDoor;
		connectionRoomDoor = otherRoom;
	}

}
