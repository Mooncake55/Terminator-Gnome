using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public float attackDuration;
    public float attackCooldDown;
    public float attackRange;
    public float attackDamage;
    public float moveSpeed; //to change the navmesh agent speed
    public float lifePoints;
    public Sprite sprite;
    public float damageCooldown;
}
