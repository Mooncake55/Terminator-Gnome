using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float _maxHealth;
    //serialized so its easier to do tests 
    [SerializeField] public float _actualHealth; 

    public event Action OnDeath;
    
    public void SetLifePoints(float lifePoints)
    {
        _maxHealth = lifePoints;
    }

    public void Heal(float amount)
    {
        Debug.Log("The entity Heals");
        _actualHealth += amount;
        if (_actualHealth > _maxHealth)
        {
            _actualHealth = _maxHealth;
        }
    }

    //notifies the death of the GameObject that implements this system 
    public void TakeDamage(float damage)
    {
        Debug.Log($"The"+gameObject+ "is Wounded");
        _actualHealth -= damage;
        if (_actualHealth < 0)
        {
            OnDeath?.Invoke();
        }
    }
    public void Respawn()
    {
        Debug.Log("The entity Arise");
        _actualHealth = _maxHealth;
    }
}

