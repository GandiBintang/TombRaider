using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhipAttack : MonoBehaviour
{
    public Collider2D whipCollider;
    public float damage = 3f;
	public float knockbackForce = 500f;
    Vector2 rightAttackOffset;
	

    private void Start() {
        rightAttackOffset = transform.position;
		
    }

    public void AttackRight() {
        whipCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
    }

    public void AttackLeft() {
        whipCollider.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
    }

    public void StopAttack() {
        whipCollider.enabled = false;
    }
	
	
	
	private void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Enemy") {
	//Get Enemy Tag Component
			Enemy enemy = other.GetComponent<Enemy>();
				if(enemy != null) {
			other.SendMessage("OnHit",damage);
			}
		}
	}
}