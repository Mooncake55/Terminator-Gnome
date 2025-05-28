using System;
using UnityEngine;

public class MeleeEnemySpawner : MonoBehaviour
{
    [SerializeField]private GameObject meleeEnemyPrefab;
    public event Action OnSpawning;
    public event Action DesSpawning;
    public IEnemyState enemyState;
    [SerializeField] EnemySpawnerController enemySpawnerController;

    private void Start()
    {
        enemySpawnerController.OnPreparigToSpawn += SpawnEnemies;
    }
    public void SpawnEnemies(Collider2D[] enemyColliders)
    {

        if (enemyColliders.Length != 0) { OnSpawning?.Invoke(); }
        else
        {
            return;
        }

        foreach (var enemySpawn in enemyColliders)
        {
            if (enemySpawn.CompareTag("MeleeEnemySpawn"))
            {
                InstantiateMeleeEnemy(enemySpawn);
                Destroy(enemySpawn);
            }
        }     

    }
    void InstantiateMeleeEnemy(Collider2D meleeCollider)
    {
        enemyState = new EnemyIdleState();
        GameObject enemy = Instantiate(meleeEnemyPrefab, meleeCollider.transform.position, Quaternion.identity);
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        //enemyState.SetContext(enemyController);
        enemyController.SetState("InitialState");
        Debug.Log("NACE UN NUEVO ENEMIGO");
    }
}
