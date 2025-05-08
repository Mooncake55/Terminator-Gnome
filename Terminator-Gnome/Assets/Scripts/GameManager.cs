using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static GameManager Instance => instance; //esto pa que? dice que da mas robustez y protege de escritura pero qsy
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
