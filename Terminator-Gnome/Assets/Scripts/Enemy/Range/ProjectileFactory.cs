using UnityEngine;

public class ProjectileFactory : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    //[SerializeField] private Transform firePoint;
    [SerializeField] private float projectileSpeed = 5f;

    public void Fire(Vector2 direction)
    {
        Debug.Log("Fire");
        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = proj.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.linearVelocity = direction.normalized * projectileSpeed;
    }
}
