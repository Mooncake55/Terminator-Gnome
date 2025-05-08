using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class NavmeshTest : MonoBehaviour
{
    public Transform target;
    public  NavMeshAgent _agent;
    private Vector2 _startPosition;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        //_startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(target.position);
    }
}
