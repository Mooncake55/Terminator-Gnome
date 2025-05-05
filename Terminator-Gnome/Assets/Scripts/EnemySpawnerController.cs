using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System.Linq;
using System.Collections.Generic;

public class EnemySpawnerController : MonoBehaviour
{
    float height;
    float width;
    public EnemySpawner enemySpawner;
    bool isSpawning = false;
    Vector2 center;
    Coroutine enemyPrepareCorrutine;

    private void Start()
    {
        enemySpawner = enemySpawner.GetComponent<EnemySpawner>();
        Camera cam = Camera.main;
        // Altura y ancho en unidades del mundo
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
        // Posición del centro (puede ser cam.transform.position si no se mueve en Z)
        center = cam.transform.position;
        enemySpawner.OnSpawning += Spawning;
        enemySpawner.DesSpawning += DesSpawning;

    }

    private void Update() //cambiar por un evento de la camara
    {
        if (isSpawning) {return; }
        Collider2D[] enemyColliders = Physics2D.OverlapBoxAll(center, new Vector2(width, height), 0f);
        if (enemyPrepareCorrutine == null)
        {
            enemyPrepareCorrutine = StartCoroutine(PrepareEnemySpawn(enemyColliders));
        }
    }
    void Spawning()
    {
        isSpawning = true;
    }
    void DesSpawning()
    {
        isSpawning = false;
    }
    void OnDrawGizmos()
    {
        if (Camera.main == null) return;

        float height = 2f * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;
        Vector2 center = Camera.main.transform.position;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, new Vector3(width, height, 0));
    }
    public IEnumerator PrepareEnemySpawn(Collider2D[] enemyColliders)
    {
        yield return null;
        enemySpawner.SpawnEnemies(enemyColliders);
        isSpawning = true ;
        enemyPrepareCorrutine = null;
    }

   
}

  
