using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    //[SerializeField] public int _maxHealth;
    public float _maxHealth;
    [SerializeField] public float _actualHealth;

    public event Action OnDeath;
    
    public void SetLifePoints(float lifePoints)
    {
        _maxHealth = lifePoints;
    }

    //Heals the entity
    public void Heal(float amount)
    {
        Debug.Log("The entity Heals");

        _actualHealth += amount;

        if (_actualHealth > _maxHealth)
        {
            _actualHealth = _maxHealth;
        }
    }

    //The entity take damage
    public void TakeDamage(float damage)
    {
        Debug.Log($"The"+gameObject+ "is Wounded");

        _actualHealth -= damage;

        if (_actualHealth < 0)
        {
            //Death();
            OnDeath?.Invoke();
        }
    }
    public void Respawn()
    {
        Debug.Log("The entity Arise");
        _actualHealth = _maxHealth;
    }
}

