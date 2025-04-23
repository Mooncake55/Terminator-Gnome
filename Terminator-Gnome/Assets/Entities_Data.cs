using UnityEngine;

[CreateAssetMenu(fileName = "Entities_Data_000", menuName = "Scriptable Objects/Entities_Data")]
public class Entities_Data : ScriptableObject
{
    [SerializeField] private float _movementSpeed;

    public float movementSpeed => _movementSpeed;
}
