using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class LevelFactory : MonoBehaviour {

	public int roomCount;
	public GameObject MapContainer;
	public GameObject MapCurrentPos;

	Room startingPoint;
	Room[] templates;
	List<Room> rooms;

	HashSet<int> drawnRoomIds = new HashSet<int> ();

	LevelGenerator generator;

	void Awake() {
		Debug.Log ("LevelFactory Awake");
		MapContainer.SetActive (false);
		MapCurrentPos.SetActive (false);
	}

	public Map generate(GameObject[] prefabs) {
		GenerateRooms (prefabs);
		generator = new LevelGenerator (startingPoint, templates, roomCount);
		rooms = generator.GenerateMap ().rooms;
		startingPoint = rooms [0];
		DrawMap ();
		return new Map (startingPoint, rooms);
	}

	void Update() {
		if (Input.GetKeyDown ("tab")) {
			MapContainer.SetActive (true);
			MapCurrentPos.SetActive (true);
		}
		if (Input.GetKeyUp ("tab")) {
			MapContainer.SetActive (false);
			MapCurrentPos.SetActive (false);
		}
	}

	void GenerateRooms(GameObject[] prefabs) {
		templates = new Room[prefabs.Length];
		for (int i = 0; i < prefabs.Length; i++) {
			templates [i] = new Room (prefabs[i]);
		}
		startingPoint = templates [0].clone();
	}

	void DrawMap() {
		DrawRoom (startingPoint);
	}

	void DrawRoom(Room room) {
		if (drawnRoomIds.Contains (room.roomId)) {
			return;
		}
		drawnRoomIds.Add (room.roomId);

		room.AddMapLine (
			DrawLine (
				new Vector3 (room.LeftX (), room.TopY (), 0), 
				new Vector3 (room.LeftX (), room.BottomY (), 0), 
				Color.gray,
				"Room (" + room.roomId + ") left wall"
			)
		);

		room.AddMapLine (
			DrawLine (
				new Vector3 (room.RightX(), room.TopY(), 0), 
				new Vector3 (room.RightX(), room.BottomY(), 0), 
				Color.gray,
				"Room (" + room.roomId + ") right wall"
			)
		); 

		room.AddMapLine (
			DrawLine (
				new Vector3 (room.LeftX(), room.TopY(), 0), 
				new Vector3 (room.RightX(), room.TopY(), 0), 
				Color.gray,
				"Room (" + room.roomId + ") top wall"
			)
		); 

		room.AddMapLine (
			DrawLine (
				new Vector3 (room.LeftX(), room.BottomY(), 0), 
				new Vector3 (room.RightX(), room.BottomY(), 0), 
				Color.gray,
				"Room (" + room.roomId + ") bottom wall"
			)
		); 

		for (int i = 0; i < room.doors.Count; i++) {
			Door door = room.doors [i+1];
			if (room.doorsWithConnection.Contains (door)) {

				int doorStartX = room.LeftX () + door.x;
				int doorStartY = room.TopY () + door.y;
				int doorEndX = doorStartX;
				int doorEndY = doorStartY;

				if (door.x == 0 || door.x == room.width) {
					doorEndY += 2;
				} else {
					doorEndX += 2;
				}

				room.AddMapLine (
					DrawLine (
						new Vector3 (doorStartX, doorStartY, -1), 
						new Vector3 (doorEndX, doorEndY, -1), 
						Color.green,
						"Room (" + room.roomId + ") door (" + door.doorId + ")"
					)
				); 

				DrawRoom (GetRoom(door.connectedRoomId));
			}
		}
	}

	LineRenderer DrawLine(Vector3 start, Vector3 end, Color color, String name)	{
		start.y = -start.y;
		end.y = -end.y;

		start.Scale(new Vector3(0.05f, 0.05f, 0.05f));
		end.Scale(new Vector3(0.05f, 0.05f, 0.5f));

		GameObject myLine = new GameObject();
		myLine.name = name;
		myLine.transform.parent = MapContainer.transform;
		myLine.AddComponent<LineRenderer>();
		LineRenderer lr = myLine.GetComponent<LineRenderer>();
		lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
		lr.SetColors(color, color);
		lr.SetWidth(0.1f, 0.1f);
		lr.SetPosition(0, start);
		lr.SetPosition(1, end);
		lr.enabled = false;
		lr.useWorldSpace = false;

		return lr;
	}


	Room GetRoom(int roomId) {
		for (int i = 0; i < rooms.Count; i++) {
			if (rooms [i].roomId == roomId) {
				return rooms [i];
			}
		}
		throw new Exception ("Room " + roomId + " not found");
	}
}