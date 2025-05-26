using EnemyMelee;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;

//using static UnityEngine.RuleTile.TilingRuleOutput;

public class MeleeMovementState : MonoBehaviour, IEnemyState
{
    EnemyController context;
    IEnemyState attackState;
    private Transform target;

    private NavMeshAgent _agent;
    private Vector2 _startPosition;
    private Vector2 direction;
    private Vector2 lastTargetPosition;
    private float distanceToTarget;

    [SerializeField]private float attackRange = 0.16f; //valor por defecto, se puede cambiar
    [SerializeField] private LayerMask playerLayer;

    public void Enter()
    {
        target = context.GetTarget();
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _startPosition = transform.position;
        _agent.SetDestination(target.position);
        lastTargetPosition = target.position;
    }

    public void UpdateAction()
    {
        if (target == null) { return; }
        distanceToTarget = Vector2.Distance(lastTargetPosition, target.position);
        if (distanceToTarget > 0.5f)
        {
            _agent.SetDestination(target.position);
            lastTargetPosition = target.position;
        }
        if (target == null) { return; }
        direction = _agent.velocity.normalized; //creo que no lo necesito
        CheckForAttack();
    }

    public void CheckForAttack()
    {
        if (target == null) { return; }
        if (distanceToTarget <= attackRange)
        {
            Debug.Log("¡En rango para atacar!");
            //attackState = new MeleeAttackState(); //o lo creo antes y uso uno solo?
            context.SetState("MeleeAttackState");
        }
    }
    public void SetAtkRange(float range) //borrable, creo que esta al pedo
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

    public void SetContext(EnemyController contextenemyController)
    {
        context = contextenemyController;
    }
}
