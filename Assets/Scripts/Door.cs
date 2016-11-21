using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Door {
	public int doorId;
	public int x;
	public int y;
	public Room connectedRoom;
	public int connectedDoorId;

	Door(int id, int _x, int _y) {
		doorId = id;
		x = _x;
		y = _y;
	}

	public Door(int id, int _x, int _y, int width, int height) {
		doorId = id;
		x = _x;
		y = _y;
		Normalize (width, height);
	}

	void Normalize(int width, int height) {
		if (x == 0 || x == width || y == 0 || y == height) {
			return;
		}

		bool isLeft = x < (width - x);
		bool isTop = y < height - y;

		if (isLeft) {
			if (isTop) {
				if (x < y) {
					x = 0;
				} else {
					y = 0;
				}
			} else {
				if (x < height - y) {
					x = 0;
				} else {
					y = height;
				}
			}
		} else {
			if (isTop) {
				if (width - x < y) {
					x = width;
				} else {
					y = 0;
				}
			} else {
				if (width - x < height - y) {
					x = width;
				} else {
					y = height;
				}
			}
		}
	}


	public Door clone() {
		return new Door (doorId, x, y);
	}

}