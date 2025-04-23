using UnityEngine;

[CreateAssetMenu(fileName = "HealthData_000", menuName = "Scriptable Objects/HealthData")]
public class HealthData : ScriptableObject
{
    [SerializeField] private float _maxHealth;

    public float MaxHealth => _maxHealth; 
}
