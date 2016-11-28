using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Door {
	public int doorId;
	public int x;
	public int y;
	public int connectedRoomId;
	public int connectedDoorId;
	public Position position;

	Door(int id, int _x, int _y, Position pos) {
		doorId = id;
		x = _x;
		y = _y;
		position = pos;
	}

	public Door(int id, int _x, int _y, int width, int height) {
		doorId = id;
		x = _x;
		y = _y;
		Normalize (width, height);
	}

	void Normalize(int width, int height) {
		bool isLeft = x < (width - x);
		bool isTop = y < height - y;

		if (isLeft) {
			if (isTop) {
				if (x < y) {
					x = 0;
					position = Position.LEFT;
				} else {
					y = 0;
					position = Position.TOP;
				}
			} else {
				if (x < height - y) {
					x = 0;
					position = Position.LEFT;
				} else {
					y = height;
					position = Position.BOTTOM;
				}
			}
		} else {
			if (isTop) {
				if (width - x < y) {
					x = width;
					position = Position.RIGHT;
				} else {
					y = 0;
					position = Position.TOP;
				}
			} else {
				if (width - x < height - y) {
					x = width;
					position = Position.RIGHT;
				} else {
					y = height;
					position = Position.BOTTOM;
				}
			}
		}
	}


	public Door clone() {
		Door other = new Door (doorId, x, y, position);
		other.connectedDoorId = connectedDoorId;
		other.connectedRoomId = connectedRoomId;
		return other;
	}

	public enum Position {
		LEFT,
		TOP,
		RIGHT,
		BOTTOM

	}

	public static bool Opposite(Position pos1, Position pos2) {
		switch (pos1) {
			case Position.LEFT:
				return pos2 == Position.RIGHT;
			case Position.TOP:
				return pos2 == Position.BOTTOM;
			case Position.RIGHT:
				return pos2 == Position.LEFT;
			case Position.BOTTOM:
				return pos2 == Position.TOP;
		}
		return false;
	}

	public override bool Equals(Object obj) 
	{
		// Check for null values and compare run-time types.
		if (obj == null || GetType() != obj.GetType()) 
			return false;

		Door d = (Door)obj;
		return (doorId == d.doorId) && (x == d.x) && (y == d.y);
	}

	public override int GetHashCode() 
	{
		return doorId * 1000000 + (x * 1000) + y;
	}
}