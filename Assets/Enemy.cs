using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;
    public float Health {
        set {
            health = value;

            if(health <= 0) {
                Defeated();
            }
        }
        get {
            return health;
        }
    }

    public float health = 6;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    void OnHit(float damage) {
        Debug.Log("Hit");
        animator.SetTrigger("Hit");
        health -= damage;

    }
	
	//void OnHit (float damage) {
	//		print("Hit");
	//		animator.SetTrigger("Hit");
	//		Health -= damage;
	//}

    public void Defeated(){
        animator.SetTrigger("Defeated");
    }

    public void RemoveEnemy() {
        Destroy(gameObject);
    }
}
