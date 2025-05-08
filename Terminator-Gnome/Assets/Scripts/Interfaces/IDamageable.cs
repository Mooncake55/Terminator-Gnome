using UnityEngine;

public interface IDamageable
{
    void HandleDamage(int amount); //modificar para que pase tambien a si mismo como ref
}

