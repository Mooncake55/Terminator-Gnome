using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System.Linq;
using System.Collections.Generic;

public class EnemySpawnerController : MonoBehaviour
{
    public float radius = 5f;
    public Color gizmoColor = Color.red;
    public GameObject meleeEnemyPrefab;
    public GameObject rangeEnemyPrefab;
    [SerializeField]private float spawnInterval = 3f;
    [SerializeField] private int maxEnemies = 3;
    private int currentEnemyCount = 0;
    private bool spawningActive = false;
    private float spawnTimer;

    public Transform[] spawnPoints;
    List<Collider2D> meleeSpawns = new List<Collider2D>();
    List<Collider2D> rangeSpawns = new List<Collider2D>();
    List<GameObject> enemiesList = new List<GameObject>();
    void Update()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius); //despues cambiar por overlaparea
        foreach (var col in colliders)
        {
            if (col.CompareTag("MeleeEnemySpawn"))
            {
                Debug.Log("MeleeSpawnPoint detectado: " + col.name);
                meleeSpawns.Add(col);
                
            }
            else if (col.CompareTag("MeleeEnemySpawn"))
            {
                Debug.Log("MeleeSpawnPoint detectado: " + col.name);
                rangeSpawns.Add(col);
            }
        }
        StartCoroutine(WaitSeconds(spawnInterval));
        spawnEnemyHandler();


    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    private void spawnEnemyHandler()
    {
        if(maxEnemies < currentEnemyCount) { return; }  
        foreach (var melee in meleeSpawns)
        {
            GameObject enemy = Instantiate(meleeEnemyPrefab, melee.transform.position, Quaternion.identity);
            enemiesList.Add(enemy);
            currentEnemyCount = currentEnemyCount + 1;
        }
        foreach (var range in meleeSpawns)
        {
            GameObject enemy = Instantiate(meleeEnemyPrefab, range.transform.position, Quaternion.identity);
            enemiesList.Add(enemy);
            currentEnemyCount = currentEnemyCount + 1;
        }
        HandleEnemyDeath();
    }
    //public void StartSpawning()
    //{
    //    spawningActive = true;
    //    spawnTimer = spawnInterval;
    //}

    //public void StopSpawning()
    //{
    //    spawningActive = false;
    //}

    //void SpawnEnemy()
    //{
    //    if (spawnPoints.Length == 0) return;

    //    int index = Random.Range(0, spawnPoints.Length);
    //    Transform spawnPoint = spawnPoints[index];

    //    GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    //    currentEnemyCount++;
    //    newEnemy.GetComponent<Enemy>().OnEnemyDeath += HandleEnemyDeath;
    //}
    IEnumerator WaitSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);  
    }
    void HandleEnemyDeath()
    {
        StartCoroutine(DestroyAllEnemies());
    }
    public IEnumerator DestroyAllEnemies()
    {
        yield return new WaitForSeconds(3f);
        foreach (var enemy in enemiesList)
        {
            enemy.SetActive(false);
        }
        
    }
}

  
