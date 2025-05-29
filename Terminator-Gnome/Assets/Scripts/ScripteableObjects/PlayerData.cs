using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public float meleeAtkDuration; //despues desacoplar
    public float lifePoints;
    public float moveSpeed;
    public float dashSpeed;
    public float dashDuration;
}
