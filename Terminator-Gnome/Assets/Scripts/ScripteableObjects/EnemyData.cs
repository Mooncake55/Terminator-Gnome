using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public float attackkDuration;
    public float attackkSpeed;
    public float attackkRange;
    public float moveSpeed; //para despues usar de una en el agent
    public float lifePoints;
    public Sprite sprite;

}
