using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static GameManager Instance => instance; 
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

    //Used to player respawn, it reactivates a game object after a time
    public void ScheduleReactivation(GameObject obj, float delay)
    {
        StartCoroutine(ReactivationCoroutine(obj, delay));
    }

    private IEnumerator ReactivationCoroutine(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(true);
    }

}
