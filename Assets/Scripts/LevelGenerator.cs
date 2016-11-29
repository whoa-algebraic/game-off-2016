using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class LevelGenerator {
	private static System.Random rnd = new System.Random();

	int roomLimit;
	int counter = 0;
	Room startingPoint;
	Room[] templates;

	List<MapState> mapStates = new List<MapState> ();
	MapState actualState;
	MapState winningState;

	public LevelGenerator(Room _startingPoints, Room[] _templates, int limit) {
		templates = _templates;
		roomLimit = limit;
		startingPoint = _startingPoints;
		mapStates.Add(new MapState(startingPoint));
		actualState = mapStates [0];
		GenerateApplicableOperators ();
	}

	public MapState GenerateMap() {
		step ();
		return winningState;
	}


	void step() {
//		Debug.Log ("Step forward");
//		Debug.Log ("Actual state: " + actualState);
		counter++;
		if (counter == 10) {
			throw new Exception ("StackOverflow");
		}
		ApplicableOperator chosenOperator = actualState.getRandomOperator ();
		if (chosenOperator == null) {
			stepBack ();
		} else {
//		Debug.Log ("Searching connection for [Room: " + randomOpenDoor.room.roomId + ", Door: (" + randomOpenDoor.door.doorId + ") " + randomOpenDoor.door.position + "]");
			actualState = actualState.Apply (chosenOperator);
			mapStates.Add (actualState);

			if (IsWinningState ()) {
				winningState = actualState;
				return;
			}
			GenerateApplicableOperators ();
		}
		step ();
	}

	void stepBack() {
//		Debug.Log ("Step back");
		counter--;
		mapStates.RemoveAt (mapStates.Count - 1);
		if (mapStates.Count == 0) {
			throw new Exception ("Could not find acceptable map setup for given inputs");
		}
		actualState = mapStates [mapStates.Count - 1];
		actualState.ReAddLastDoor ();
	}

	void GenerateApplicableOperators() {
		foreach (RoomDoor openDoor in actualState.GetOpenDoors ()) {
			List<RoomDoor> applicableConnections = GetApplicableConnections (openDoor);
			foreach (RoomDoor connectionRoomDoor in applicableConnections) {
				actualState.applicableConnections.Add (
					new ApplicableOperator (
						openDoor,
						connectionRoomDoor
					)
				);
			}
		}
	}

	bool isRoomApplicable(RoomDoor roomDoor, Room newRoom) {
		if (actualState.RoomCount() + actualState.OpenDoorCount() + newRoom.doors.Count - 1 > roomLimit) {
			return false;
		}
		return true;
	}

	bool isDoorApplicable(RoomDoor roomDoor, Room otherRoom, Door otherDoor) {
		if (!Door.Opposite(roomDoor.door.position, otherDoor.position)) {
			return false;
		}
		Room cloneOther = otherRoom.clone ();
		Room.ConnectDoors (roomDoor.room.clone (), roomDoor.door.clone (), cloneOther, otherDoor.clone ());
		if (actualState.Overlaps(cloneOther)) {
			return false;
		}
		return true;
	}

	bool IsWinningState() {
		return actualState.RoomCount() == roomLimit && actualState.OpenDoorCount() == 0;
	}

	List<RoomDoor> GetApplicableConnections(RoomDoor openRoomDoor) {
		List<RoomDoor> applicableConnections = new List<RoomDoor> ();
		for (int i = 0; i < templates.Length; i++) {
			Room act = templates [i];
			if (isRoomApplicable(openRoomDoor, act)) {
				Room actRoom = act.clone();
				foreach (Door door in actRoom.doors.Values) {
					if (isDoorApplicable(openRoomDoor, actRoom, door)) {
//						Debug.Log ("Applicable room: [Room: " + actRoom.width + "x" + actRoom.height + ", Door: " + door.position + "]");
						applicableConnections.Add (new RoomDoor (actRoom, door));
					}
				}
			}
		}
		Shuffle (applicableConnections);
		return applicableConnections;
	}

	static void Shuffle<T>(List<T> list)  {  
		int n = list.Count;  
		while (n > 1) {  
			n--;  
			int k = rnd.Next(n + 1);  
			T value = list[k];  
			list[k] = list[n];  
			list[n] = value;  
		}  
	}
}
