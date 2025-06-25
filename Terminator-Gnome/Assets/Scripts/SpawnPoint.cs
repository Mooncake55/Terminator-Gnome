using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private Transform spawoPoint;
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.SetPlayerSpawnPoint(this.transform);
        }
    }
}
