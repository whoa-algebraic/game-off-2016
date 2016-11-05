using UnityEngine;

public class HorizontalAIMovement : MonoBehaviour {

    public float MovementSpeed = 1;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        transform.localPosition += new Vector3(MovementSpeed * Time.deltaTime, 0, 0);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Wall") {
            MovementSpeed *= -1;
        }
    }
}
