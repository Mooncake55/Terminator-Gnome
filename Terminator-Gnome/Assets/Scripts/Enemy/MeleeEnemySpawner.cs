using System;
using UnityEngine;

public class MeleeEnemySpawner : MonoBehaviour
{
    public GameObject meleeEnemyPrefab;
    public event Action OnSpawning;
    public event Action DesSpawning;
    public IEnemyState enemyState;

    public void SpawnEnemies(Collider2D[] enemyColliders)
    {

        if (enemyColliders.Length != 0) { OnSpawning?.Invoke(); }
        else
        {
            return;
        }

        foreach (var enemy in enemyColliders)
        {
            if (enemy.CompareTag("MeleeEnemySpawn"))
            {
                InstantiateMeleeEnemy(enemy);
                Destroy(enemy);
            }
        }
        void InstantiateMeleeEnemy(Collider2D meleeCollider)
        {
            enemyState = new EnemyIdleState();
            GameObject enemy = Instantiate(meleeEnemyPrefab, meleeCollider.transform.position, Quaternion.identity);
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            enemyState.SetContext(enemyController);
            enemyController.SetState(enemyState);
        }

    }
}
