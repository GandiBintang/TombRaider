using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public int damage;  
    private Rigidbody2D rb;
    
    void Start()
    {
     rb = GetComponent<Rigidbody2D>();
     rb.velocity = transform.right * speed;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Enemy enemy = collision.GetComponent<Enemy>();

        if(enemy != null) {
            collision.SendMessage("OnHit", damage);
        }
        Destroy(gameObject);
    }
}
