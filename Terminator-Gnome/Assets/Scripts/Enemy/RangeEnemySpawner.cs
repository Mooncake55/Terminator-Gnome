using UnityEngine;

public class RangeEnemySpawner : MonoBehaviour
{
    [SerializeField]private GameObject rangeEnemyPrefab;
    //public event Action OnSpawning;
    //public event Action DesSpawning;
    public IEnemyState enemyState;
    [SerializeField] EnemySpawnerController enemySpawnerController;

    //either the spawn controller and every specific spawner notifies eachother
    private void Start()
    {
        enemySpawnerController.OnPreparigToSpawn += SpawnEnemies;
    }
    public void SpawnEnemies(Collider2D[] enemyColliders)
    {
        if (enemyColliders.Length != 0) 
        { 
            //OnSpawning?.Invoke();
            foreach (var enemySpawn in enemyColliders)
            {
                if (enemySpawn.CompareTag("RangeEnemySpawn"))
                {
                    InstantiateRangeEnemy(enemySpawn);
                    Destroy(enemySpawn);
                }
            }
        }
    }

    //MeleeEnemyIdleState is the initial state for this enemy
    void InstantiateRangeEnemy(Collider2D meleeCollider)
    {
        GameObject enemy = Instantiate(rangeEnemyPrefab, meleeCollider.transform.position, Quaternion.identity);
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        enemyState = new RangeEnemyIdleState(enemyController);
        
    }
}
