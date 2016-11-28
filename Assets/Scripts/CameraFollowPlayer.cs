using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour {
    public Transform PlayerCharacter;
    public float horizontalEdgeBuffer;
    public float verticalEdgeBuffer;
	
    private int mapTileHorizontalUnits = 4;
    private int mapTileVerticalUnits = 2;
    private float unitSize = 5;

    private float maxLeft;
    private float maxRight;
    private float maxTop;
    private float maxBottom;

	void Start() {
		InitForRoomDimenstions (
			Managers.RoomNavigationManager.GetActiveRoomWidth (), 
			Managers.RoomNavigationManager.GetActiveRoomHeight ()
		);
	}

	public void InitForRoomDimenstions(float width, float height) {
		maxLeft = ((width / 2) * -1) + horizontalEdgeBuffer;
		maxRight = (width / 2) - horizontalEdgeBuffer;
		maxTop = (height / 2) - verticalEdgeBuffer;
		maxBottom = ((height / 2) * -1) + verticalEdgeBuffer;
	}
        
	void Update () {
        float playerX = PlayerCharacter.position.x;
        float playerY = PlayerCharacter.position.y;

        if(playerX < maxLeft) {
            playerX = maxLeft;
        }

        if(playerX > maxRight) {
            playerX = maxRight;
        }

        if(playerY < maxBottom) {
            playerY = maxBottom;
        }

        if(playerY > maxTop) {
            playerY = maxTop;
        }

        Vector3 newPosition = new Vector3(playerX, playerY, -10);
        transform.position = newPosition;
	}
}
