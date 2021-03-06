﻿using UnityEngine;
using WhoaAlgebraic;

public class Limb : MonoBehaviour {

	public float coolDown;
	public float activeDuration;
	
	private BoxCollider2D col;
	private Animator m_Anim;
	private bool isActive;
	private bool isDisabled;
	private float CDTimer;
    private int damageDoneByLimb = 15;


    void Start() {
		col = GetComponent<BoxCollider2D>();
		m_Anim = transform.parent.GetComponent<Animator>();
		col.enabled = false;
		isActive = false;
		isDisabled = false;
	}

	public virtual bool Activate() { // must return a bool so that inheriting class can escape if on cool down
		if(isDisabled) { return false; }

		toggleCollider();
		isActive = true;
		isDisabled = true;
		m_Anim.SetBool("Attacking", true);
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
			toggleCollider();
			isActive = false;
			m_Anim.SetBool("Attacking", false);
		}
	}

	private void toggleCollider() {
		col.enabled = !col.enabled;
	}


    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        // If the EnemyHealth component exist...
        if (enemyHealth != null)
        {
            // ... the enemy should take damage.
            enemyHealth.TakeDamage(damageDoneByLimb, this.transform.position);
        }
    }

}
