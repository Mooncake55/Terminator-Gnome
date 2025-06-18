using UnityEngine;

[System.Serializable]
public class AttackEntry
{
    public AttackIdentifier id;
    public MonoBehaviour attackComponent; // debe implementar IAttack
}
