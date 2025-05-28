
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;

//using static UnityEngine.RuleTile.TilingRuleOutput;
//[RequireComponent(typeof(CircleCollider2D))]
public class MeleeMovementState : MonoBehaviour, IEnemyState
{
    //TAG: MeleeEnemyMovementState
    //private CircleCollider2D _circleCollider2D;

    Transform parent;
    Transform AttackStateComponent;
    MeleeAttackState attackState;

    EnemyController context;
    private string nextStateTag = "MeleeEnemyAttackState";
    private Transform target;

    private NavMeshAgent _agent;
    private float distanceToTarget;
    Vector2 direction;

    [SerializeField] private float attackRange = 0.016f; //valor por defecto, se puede cambiar
    //[SerializeField] private LayerMask playerLayer;

    public void Enter()
    {
        parent = transform.parent;
        if (AttackStateComponent == null) { FindStateByTag(nextStateTag); }
        Debug.Log("ENTRE AL ESTADO MOVEMENT");
        target = context.GetTarget();
        _agent = context.GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _agent.SetDestination(target.position);
        //_circleCollider2D = GetComponent<CircleCollider2D>();
    }

    public void FindStateByTag(string tag)
    {
        foreach (Transform child in parent)
        {
            if (child != transform && child.CompareTag(nextStateTag))
            {
                Debug.Log("Hermano encontrado: " + child.name);
                AttackStateComponent = child;
                break;
            }
        }
    }

    public void UpdateAction()
    {
        if (target == null) { return; }
        //distanceToTarget = Vector2.Distance(lastTargetPosition, target.position);
        distanceToTarget = Vector2.Distance(context.transform.position, target.position);
        if (distanceToTarget > attackRange)
        {
            _agent.SetDestination(target.position);
        }
        direction = _agent.velocity.normalized;
        CheckForAttack();
    }

    public void CheckForAttack()

    {
        if (target == null) { return; }
        if (distanceToTarget <= attackRange)
        {
            Debug.Log("¡En rango para atacar!");
            Debug.Log("¡En rango para atacar! Distancia actual: " + distanceToTarget);
            //attackState = new MeleeAttackState(); //o lo creo antes y uso uno solo?
            //context.SetState("MeleeAttackState");
            if(attackState == null) { attackState = AttackStateComponent.GetComponent<MeleeAttackState>(); }            
            attackState.PrepareMeleeAttack(direction);
            Exit();
        }
    }
    public void SetAtkRange(float range) //borrable, creo que esta al pedo
    {
        attackRange = range;
    }
    public void Exit()
    {     
        context.SetState(nextStateTag);
    }

    public void SetContext(EnemyController contextenemyController)
    {
        if (context == null) { context = contextenemyController; }
    }
    //public void OnTriggerEnter2D(Collider2D other)
    // {
    //    //if (!other.CompareTag("Player")) return;
    //    if (other.CompareTag("Player"))
    //    {
    //        Vector3 playerDirection = other.transform.position - transform.position;
    //        RaycastHit2D rayCast = Physics2D.Raycast(transform.position, playerDirection, _circleCollider2D.radius);

    //        if (rayCast.collider == null) return;

    //        if (rayCast.collider.CompareTag("Player"))
    //        {
    //            if (target == null)
    //            {
    //                _agent.SetDestination(other.transform.position);
    //            }
    //            Debug.Log("Player Detected!");
    //        }

    //    }
    //}
}
