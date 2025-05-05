using UnityEngine;
using System.Collections;

public class EnemySpawnerController : MonoBehaviour
{
    Collider2D[] spawnPoints = Physics2D.OverlapCircleAll(transform.position, 5f);

    private void Awake()
    {
        spawnPoints = 
    }
}

  
