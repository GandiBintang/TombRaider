using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
	public Transform shootingPoint;
	public GameObject bulletPrefab;
	public Transform shootingPoint2;
	public GameObject bulletPrefab2;
    public ContactFilter2D movementFilter;
	public WhipAttack whipAttack;
	private int currentAmmo;
	int weaponIndex = 0;
    Vector2 movementInput;
	SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
	Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
	
	bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		currentAmmo = 10;
		
    }

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("First weapon chosen.");
			weaponIndex = 0;

        } else if (Input.GetKeyDown(KeyCode.Keypad1) | Input.GetKeyDown(KeyCode.Alpha1)){
			Debug.Log("Second weapon chosen");
			weaponIndex = 1;
		}

	
		
	}

    private void FixedUpdate() {
		
		if(canMove) {
        // To check if movement is not 0
        if(movementInput != Vector2.zero) {
            bool success = TryMove(movementInput);
			
			if(!success) {
				success = TryMove(new Vector2(movementInput.x, 0));
			}
				if(!success) {
					success = TryMove(new Vector2(0, movementInput.y));
				}
			
			animator.SetBool("isMoving", success);
			} else {
				animator.SetBool("isMoving", false);
			}
			
			// Set direction of sprite according to the direction
			if(movementInput.x < 0) {
			spriteRenderer.flipX = true;
		}
			else if(movementInput.x > 0) {
			spriteRenderer.flipX = false;
		}
		
		
    }

	
}
	private bool TryMove(Vector2 direction) {
		if(direction != Vector2.zero) {
        int count = rb.Cast(direction // X and Y values between -1 and 1 that represent the direction from the body to look for collisions
			, movementFilter // The settings that determine where a collision can occur on such as layers to collide with
				, castCollisions // List of collisions to store the found collisions into after the Cast is finished
					, moveSpeed * Time.fixedDeltaTime + collisionOffset); // The amount to cast equal to the movement plus an offset
		
		if(count == 0){
				rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
				return true;
			} else {
				return false;
			}
			} else {
				// no direction to move
				return false;
			}
		}
	
    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>();
    }
	void OnFire() {
		if(weaponIndex == 0){
			animator.SetTrigger("whipAttack");
		}
		if(weaponIndex == 1){
			if(currentAmmo !=0) {
				if(spriteRenderer.flipX == true) {
				Instantiate(bulletPrefab2,shootingPoint2.position,transform.rotation);
			} else {
				Instantiate(bulletPrefab,shootingPoint.position,transform.rotation);
			}
			currentAmmo -= 1;
			Debug.Log("Current Ammo :" + currentAmmo);
			
			}else {
			Debug.Log("No Ammo Left");
		}
			
		}
		
		
	}

	
	public void WhipAttack() {
		LockMovement();
		if(spriteRenderer.flipX == true) {
			whipAttack.AttackLeft();
				} else {
					whipAttack.AttackRight();
		}
	}
	
    public void EndWhipAttack() {
        UnlockMovement();
        whipAttack.StopAttack();
    }
	public void LockMovement() {
		canMove = false;
	}
	public void UnlockMovement() {
		canMove = true;
	}
}
