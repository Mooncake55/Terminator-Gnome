using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Entities
{
    [SerializeField] private GameObject objective;

    private void MoveTo(GameObject objective)
    {

        if (objective != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, objective.transform.position, 2 * Time.deltaTime);
        }
    }
    private GameObject SearchObjective() 
    { 

        return objective; 
    }
}
