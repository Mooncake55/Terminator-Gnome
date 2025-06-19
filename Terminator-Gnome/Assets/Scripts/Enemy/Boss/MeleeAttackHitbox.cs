using System;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public Action OnFinishedAttack;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable damageable))
        {
            Debug.Log("Se Detecto el Player");
            damageable.HandleDamage(30f); // o el valor que corresponda
        }
        OnFinishedAttack?.Invoke();
    }
}
