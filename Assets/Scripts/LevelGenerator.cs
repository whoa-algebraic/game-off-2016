using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LevelGenerator {

	int roomLimit;
	Room startingPoint;
	Room[] templates;

	int roomCount = 1;

	public LevelGenerator(Room _startingPoints, Room[] _templates, int limit) {
		templates = _templates;
		roomLimit = limit;
		startingPoint = _startingPoints;
	}

	public void GenerateMap() {
		Room room1 = startingPoint;
		Room room2 = templates [1].clone();
		Room room3 = templates [2].clone();
		Room room4 = templates [3].clone();
		Room.ConnectDoors (room1, room1.doors [2], room2, room2.doors [1]);
		Room.ConnectDoors (room1, room1.doors [1], room3, room3.doors [1]);
		Room.ConnectDoors (room1, room1.doors [3], room4, room4.doors [1]);
	}

}
