using EnemyMelee;
using UnityEngine;
using UnityEngine.AI;
//using static UnityEngine.RuleTile.TilingRuleOutput;

public class MeleeMovementState : MonoBehaviour, IEnemyState
{
    EnemyController enemyController;
    IEnemyState attackState;
    private NavMeshAgent _agent;
    private Vector2 _startPosition;
    //private Vector2 direction;
    private Vector2 lastTargetPosition;
    private float distanceToTarget;
    private Transform target;
    private float attackRange = 0.16f; //valor por defecto, se puede cambiar

    //public MeleeMovementState(EnemyController controller, NavMeshAgent navMeshAgent, Transform player)
    //{
    //    enemyController = controller;
    //    _agent = navMeshAgent;
    //    target = player;
    //}
    public MeleeMovementState(EnemyController controller, Transform player)
    {
        target = player;
        enemyController = controller;
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _startPosition = transform.position;
        _agent.SetDestination(target.position);
        lastTargetPosition = target.position;
    }

    //public void Enter()
    //{
    //    _agent.updateRotation = false;
    //    _agent.updateUpAxis = false;
    //    _startPosition = transform.position;
    //    _agent.SetDestination(target.position);
    //    lastTargetPosition = target.position;
    //}

    public void Update()
    {
        if (target == null) { return; }
        distanceToTarget = Vector2.Distance(lastTargetPosition, target.position);
        if (distanceToTarget > 0.5f)
        {
            _agent.SetDestination(target.position);
            lastTargetPosition = target.position;
        }
        CheckForAttack();
    }

    public void CheckForAttack()
    {
        if (target == null) { return; }
        if (distanceToTarget <= attackRange)
        {
            Debug.Log("¡En rango para atacar!");
            attackState = new MeleeAttackState(); //o lo creo antes y uso uno solo?
        }
    }
    public void SetAtkRange(float range)
    {
        attackRange = range;
    }
    public void Exit()
    {
        if(attackState != null)
        {
            enemyController.SetState(attackState);
        }
        //this = null;??
    }
}
