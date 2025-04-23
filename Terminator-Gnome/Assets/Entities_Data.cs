using UnityEngine;

[CreateAssetMenu(fileName = "Entities_Data_000", menuName = "Scriptable Objects/Entities_Data")]
public class Entities_Data : ScriptableObject
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _attackSpeed;

    public float movementSpeed => _movementSpeed;
    public float maxHealth => _maxHealth;
    public float attackDamage => _attackDamage;
    public float attackSpeed => _attackSpeed;

}
