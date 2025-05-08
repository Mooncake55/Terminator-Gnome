using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] public int _maxHealth;
    [SerializeField] public int _actualHealth;
    public event Action OnDeath;

    public HealthSystem(int maxHealth, int actualHealth)
    {
        _maxHealth = maxHealth;
        _actualHealth = actualHealth;
    }
    private void Start()
    {
        //collider = GetComponent<Collider2D>();
    }

    //Heals the entity
    public void Heal(int amount)
    {
        Debug.Log("The entity Heals");

        _actualHealth += amount;

        if (_actualHealth > _maxHealth)
        {
            _actualHealth = _maxHealth;
        }
    }

    //The entity take damage
    public void TakeDamage(int damage)
    {
        Debug.Log("The entity is Wounded");

        _actualHealth -= damage;

        if (_actualHealth < 0)
        {
            //Death();
            OnDeath?.Invoke();
        }
    }

    //The entity dies
    //public void Death()
    //{
    //    Debug.Log("The entity Dies");
    //    _actualHealth = 0;
    //    Destroy(gameObject);
    //}

    //The entity respawn
    public void Respawn()
    {
        Debug.Log("The entity Arise");
        _actualHealth = _maxHealth;
    }
}

