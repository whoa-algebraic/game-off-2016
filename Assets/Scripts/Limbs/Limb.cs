using UnityEngine;

public class Limb : MonoBehaviour {

	public float coolDown;
	public float activeDuration;

	private SpriteRenderer sprite;
	private BoxCollider2D col;
	private bool isActive;
	private bool isDisabled;
	private float CDTimer;

	void Start() {
		sprite = GetComponent<SpriteRenderer>();
		col = GetComponent<BoxCollider2D>();
		sprite.enabled = false;
		col.enabled = false;
		isActive = false;
		isDisabled = false;
	}

	public virtual bool Activate() { // must return a bool so that inheriting class can escape if on cool down
		if(isDisabled) { return false; }

		toggleSpriteAndCollider();
		isActive = true;
		isDisabled = true;
		CDTimer = 0;

		Debug.Log("Limb Activate called");
		return true;
	}

	public void Update() {
		if(!isDisabled) { return; }

		manageCoolDownTimer();
		manageActiveTimer();
	}

	private void manageCoolDownTimer() {
		CDTimer += Time.deltaTime;

		if (CDTimer >= coolDown) {
			isDisabled = false;
			Debug.Log("cooldown reached, action no longer disabled");
		}
	}

	private void manageActiveTimer() {
		if(!isActive) { return; }

		if(CDTimer >= activeDuration) { // using CDTimer rather than introducing new time for now
			toggleSpriteAndCollider();
			isActive = false;
		}
	}

	private void toggleSpriteAndCollider() {
		sprite.enabled = !sprite.enabled;
		col.enabled = !col.enabled;
	}

}
