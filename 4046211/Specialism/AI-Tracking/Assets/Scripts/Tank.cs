using UnityEngine;

public class Tank : MonoBehaviour
{
    private float timeBetweenShots = 0.75f;
    private float timer;

    [SerializeField] private Rigidbody2D player;
    [SerializeField] private Transform barrel;
    [SerializeField] private Bullet bullet;

    private void Update()
    {
        RotateToShootPlayer();

        timer += Time.deltaTime;

        if (timer > timeBetweenShots) Shoot(); 
    }

    private void RotateToShootPlayer()
    {
        // Rotate tank in direction of player
        var direction = player.transform.position - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; 
        barrel.rotation = Quaternion.Euler(Vector3.forward * (angle - 90));
        
        // Offset rotation to intersection point
        var o = Vector3.Dot(player.velocity, barrel.right);
        if (Mathf.Abs(o) < bullet.force)
        {
            angle = Mathf.Asin(o / bullet.force) * Mathf.Rad2Deg;
        }
        barrel.Rotate(Vector3.back, angle);
        
    }

    // Spawns bullet
    private void Shoot()
    {
        Instantiate(bullet, barrel.position, barrel.rotation);
        timer = 0;
    }
}