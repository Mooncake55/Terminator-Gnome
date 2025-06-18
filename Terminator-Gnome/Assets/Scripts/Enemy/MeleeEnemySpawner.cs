using System;
using UnityEngine;

public class MeleeEnemySpawner : MonoBehaviour
{
    [SerializeField]private GameObject meleeEnemyPrefab;
    //public event Action OnSpawning;
    //public event Action DesSpawning;
    public IEnemyState enemyState;
    [SerializeField] EnemySpawnerController enemySpawnerController;

    //either the spawn controller and every specific spawner notifies eachother
    private void Start()
    {
        enemySpawnerController.OnPreparigToSpawn += SpawnEnemies;
    }

    //cheks on the list of spawnpoints and if its a melee spawn spawns the enemy and deletes the 
    //spawnpoint so it doest spawn every time the player goes to and already visited screen
    public void SpawnEnemies(Collider2D[] enemyColliders)
    {
        if (enemyColliders.Length != 0) 
        { 
            //OnSpawning?.Invoke();
            foreach (var enemySpawn in enemyColliders)
            {
                if (enemySpawn.CompareTag("MeleeEnemySpawn"))
                {
                    InstantiateMeleeEnemy(enemySpawn);
                    Destroy(enemySpawn);
                }
            }
        }
    }

    //MeleeEnemyIdleState is the initial state for this enemy
    void InstantiateMeleeEnemy(Collider2D meleeCollider)
    {
        GameObject enemy = Instantiate(meleeEnemyPrefab, meleeCollider.transform.position, Quaternion.identity);
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        enemyState = new MeleeEnemyIdleState(enemyController);
        enemyController.SetState(enemyState);
    }
}
