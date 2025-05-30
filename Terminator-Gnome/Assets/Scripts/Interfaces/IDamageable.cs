using UnityEngine;

//Implemented by player and enemies, indicates that it can take damage
public interface IDamageable
{
    void HandleDamage(float amount); 
}

