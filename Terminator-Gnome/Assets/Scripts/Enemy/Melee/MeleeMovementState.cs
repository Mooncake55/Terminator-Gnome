
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MeleeMovementState :  IEnemyState
{
    EnemyController context;
    IEnemyState nextState;
    private NavMeshAgent _agent;
    private Transform target;
    private float distanceToTarget;
    Vector2 direction;
    private bool isAttacking = false;
    private float attackRange;

    public MeleeMovementState(EnemyController enemy)
    {
        Debug.Log("Entradndo a MOVEMENT");
        context = enemy;
        _agent = context.GetComponent<NavMeshAgent>();
        target = context.GetTarget();
        Enter();
        _agent.SetDestination(target.position);
    }
    //Sets the enemyData values
    public void Enter()
    {
        attackRange = context.GetEnemyData().attackkRange;
        Debug.Log(attackRange);
    }

    public void UpdateAction()
    {
        if (target == null) { target = context.GetTarget(); return; }
        direction = _agent.velocity.normalized;
        context.SetFaceTo(direction);
        if (target == null) { return; }
        distanceToTarget = Vector2.Distance(context.transform.position, target.position);
        if (distanceToTarget > attackRange)
        {
            _agent.SetDestination(target.position);
        }
        else
        {
            CheckForAttack();
        }
    }

    //cheks if its at attack range and if its the case, changes the state
    public void CheckForAttack()
    {
        if (target == null) { return; }
        if (distanceToTarget <= attackRange)
        {
            Debug.Log("¡En rango para atacar!");
            Debug.Log("¡En rango para atacar! Distancia actual: " + distanceToTarget);
            isAttacking = true;
            Exit();
        }
    }

    //controls to know which state to exit
    public void Exit()
    {
        Debug.Log("Saliendo de MOVEMENT");
        if (isAttacking)
        {
            nextState = new MeleeAttackState(context);
        }
        if (nextState != null)
        {
            context.SetState(nextState);
        }
    }
}
