using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public float attackkDuration;
    public float attackkSpeed;
    public float attackkRange;
    public float attackDamage;
    public float moveSpeed; //to change the navmesh agent speed
    public float lifePoints;
    public Sprite sprite;
}
