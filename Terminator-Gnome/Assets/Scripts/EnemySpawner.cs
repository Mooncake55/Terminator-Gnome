using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject meleeEnemyPrefab;
    public GameObject rangeEnemyPrefab;

    public event Action OnSpawning;
    public event Action DesSpawning;
    List<Collider2D> meleeSpawns = new List<Collider2D>();
    List<Collider2D> rangeSpawns = new List<Collider2D>();
    List<GameObject> enemiesList = new List<GameObject>();

    Coroutine enemyEndCorrutine;

    public void SpawnEnemies(Collider2D[] enemyColliders)
    {

        if(enemyColliders.Length != 0) { OnSpawning?.Invoke(); }
        else
        {
            return;
        }

        foreach (var enemy in enemyColliders)
        {
            if (enemy.CompareTag("MeleeEnemySpawn"))
            {
                InstantiateMeleeEnemy(enemy);
            }
            else if (enemy.CompareTag("RangeEnemySpawn"))
            {
                InstantiateRangeEnemy(enemy);
            }
            Destroy(enemy);
        }
        //if (enemyEndCorrutine == null)
        //{
        //    enemyEndCorrutine = StartCoroutine(DestroyEnemies());
        //}
    }

    void InstantiateMeleeEnemy(Collider2D meleeCollider)
    {
        GameObject enemy =  Instantiate(meleeEnemyPrefab, meleeCollider.transform.position, Quaternion.identity);
        enemiesList.Add(enemy);
    }
    void InstantiateRangeEnemy(Collider2D meleeCollider)
    {
        GameObject enemy = Instantiate(rangeEnemyPrefab, meleeCollider.transform.position, Quaternion.identity);
        enemiesList.Add(enemy);
    }
    
    //public IEnumerator DestroyEnemies()
    //{
    //    yield return new WaitForSeconds(3f);
    //    foreach (var enemy in enemiesList)
    //    {
    //        Destroy(enemy);

    //    }
    //    yield return new WaitForSeconds(2f);
    //    DesSpawning?.Invoke();
    //}
    
}
