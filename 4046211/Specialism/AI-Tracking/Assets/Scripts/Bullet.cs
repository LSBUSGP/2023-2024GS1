using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    
    public float force = 5f;
    private int hits;
    private bool hitTank;

    private void Start()
    { 
        rb = GetComponent<Rigidbody2D>(); // Gets the rigid body of the bullet.

        rb.velocity = transform.up * force; // Applies the velocity to the bullet towards the players position.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Y") rb.velocity = Vector3.Reflect(rb.velocity, Vector3.up);
        if (collision.gameObject.tag == "X") rb.velocity = Vector3.Reflect(rb.velocity, Vector3.right);

        hits++;
        if (hits > 6 || collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Tank")
        {
            if (hitTank) Destroy(gameObject);
            hitTank = true;
        }
    }
}