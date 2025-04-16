using UnityEngine;

public class Health_System
{
    public int _maxHealth;
    public int _actualHealth;

    public Health_System(int maxHealth, int actualHealth)
    {
        _maxHealth = maxHealth;
        _actualHealth = actualHealth;
    }

    //Heals the entity
    public void Heal(int amount)
    {
        Debug.Log("The entity Heal");

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
            _actualHealth = 0;
        }
    }

    //The entity dies
    public void Death()
    {
        Debug.Log("The entity Dies");
        _actualHealth = 0;
    }

    //The entity respawn
    public void Respawn()
    {
        Debug.Log("The entity Arise");
        _actualHealth = _maxHealth;
    }
}
