using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static GameManager Instance => instance;

    [SerializeField] private GameObject playerPrefab;
    //[SerializeField] private Vector2 spawnPosition = Vector2.zero;
    [SerializeField] private Transform spawnPoint;
    private GameObject currentPlayer;
    public event Action OnPlayerSpawn;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Debug.Log("Deberia Spawnear un PLayer");
        SpawnPlayer();
    }
    //Used to player respawn, it reactivates a game object after a time
    public void ScheduleReactivation(GameObject obj, float delay)
    { 
        StartCoroutine(ReactivationCoroutine(obj, delay));
    }

    private IEnumerator ReactivationCoroutine(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnPlayer();
    }

    void SpawnPlayer() 
    {
        if (currentPlayer!= null)
        {
            Debug.Log("Spawneando Player");
            Destroy(currentPlayer);
        }
        currentPlayer = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        currentPlayer.GetComponent<Player>().Init();
        OnPlayerSpawn?.Invoke();
    }
    public GameObject GetPlayer() { return currentPlayer; }
    public Vector2 GetPlayerSpawnPoint() { return spawnPoint.position; }


}
