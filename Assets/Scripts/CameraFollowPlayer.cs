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
        float startingRoomWidth = Managers.RoomNavigationManager.GetActiveRoomWidth();
        float startingRoomHeight = Managers.RoomNavigationManager.GetActiveRoomHeight();

        maxLeft = ((startingRoomWidth / 2) * -1) + horizontalEdgeBuffer;
        maxRight = (startingRoomWidth / 2) - horizontalEdgeBuffer;
        maxTop = (startingRoomHeight / 2) - verticalEdgeBuffer;
        maxBottom = ((startingRoomHeight / 2) * -1) + verticalEdgeBuffer;
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
