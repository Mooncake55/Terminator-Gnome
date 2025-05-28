using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System.Linq;
using System.Collections.Generic;
using System;


public class EnemySpawnerController : MonoBehaviour
{
    float height;
    float width;
    //public EnemySpawner enemySpawner;
    bool isSpawning = false;
    Vector2 center;
    Coroutine enemyPrepareCorrutine;

    private Camera cam;
    private CameraController camController;
    public event Action<Collider2D[]> OnPreparigToSpawn;

    private void Start() //setea la camara/tamaño del area a detectar
    {

        cam = GetComponent<Camera>();
        camController = cam.GetComponent<CameraController>();
        camController.OnCameraMoved += CameraUpdate;

        //enemySpawner = enemySpawner.GetComponent<MeleeEnemySpawner>();
        //Camera cam = Camera.main;
        // Altura y ancho en unidades del mundo
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
        // Posición del centro (puede ser cam.transform.position si no se mueve en Z)
        center = cam.transform.position;
        //enemySpawner.OnSpawning += Spawning;
        //enemySpawner.DesSpawning += DesSpawning;
    }
    //antes era solo el update
    public void CameraUpdate() //cambiar por un evento de la camara
    {
        if (isSpawning) {return; }
        center = cam.transform.position;
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
        //enemySpawner.SpawnEnemies(enemyColliders);
        OnPreparigToSpawn?.Invoke(enemyColliders);
        isSpawning = true ;
        yield return null;
        enemyPrepareCorrutine = null;
        isSpawning = false ;
    }

   
}

  
