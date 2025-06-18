using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float DestroyInSeconds = 3;
    private void Start()
    {
        StartCoroutine(Destroy());
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.TryGetComponent<IDamageable>(out var damageable))
            {
                Debug.Log("proyectil Ataca al jugador");
                damageable.HandleDamage(20);
            }
            Destroy(gameObject);
        }
        
    }
    public IEnumerator Destroy()
    {
        yield return new WaitForSeconds(DestroyInSeconds);
        Destroy(gameObject);
    }
}