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
    bool isSpawning = false;
    Vector2 center;
    Coroutine enemyPrepareCorrutine;
    private Camera cam;
    private CameraController camController;
    public event Action<Collider2D[]> OnPreparigToSpawn;

    private void Start() //sets the camera to get the spawn points detection area
    {
        cam = GetComponent<Camera>();
        camController = cam.GetComponent<CameraController>();
        camController.OnCameraMoved += CameraUpdate;

        // Height and width in world units
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
    }
    //when the screen has changed, detects the spawnpoints in the screen area
    public void CameraUpdate()
    {
        if (isSpawning) {return; }
        center = cam.transform.position;
        Collider2D[] enemyColliders = Physics2D.OverlapBoxAll(center, new Vector2(width, height), 0f);
        if (enemyPrepareCorrutine == null)
        {
            enemyPrepareCorrutine = StartCoroutine(PrepareEnemySpawn(enemyColliders));
        }
    }
    void OnDrawGizmos()
    {
        if (Camera.main == null) { return; }

        float height = 2f * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;
        Vector2 center = Camera.main.transform.position;

        //draws the detection area in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, new Vector3(width, height, 0));
    }
    public IEnumerator PrepareEnemySpawn(Collider2D[] enemyColliders)
    {
        yield return null;
        OnPreparigToSpawn?.Invoke(enemyColliders);
        isSpawning = true ;
        yield return null;
        enemyPrepareCorrutine = null;
        isSpawning = false ;
    }
}

  
