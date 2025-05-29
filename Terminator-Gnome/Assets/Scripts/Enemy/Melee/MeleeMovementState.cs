
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

    //[SerializeField] private float attackRange = 0.016f; //valor por defecto, se puede cambiar
    private float attackRange = 0.16f;

    public MeleeMovementState(EnemyController enemy)
    {
        Debug.Log("Entradndo a MOVEMENT");
        context = enemy;
        _agent = context.GetComponent<NavMeshAgent>();
        target = context.GetTarget();
        _agent.SetDestination(target.position);
    }

    public void Enter()
    {       
        //_circleCollider2D = GetComponent<CircleCollider2D>();
    }

    public void UpdateAction()
    {
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
        
        //CheckForAttack();
    }

    public void CheckForAttack()
    {
        if (target == null) { return; }
        if (distanceToTarget <= attackRange)
        {
            Debug.Log("¡En rango para atacar!");
            Debug.Log("¡En rango para atacar! Distancia actual: " + distanceToTarget);
            isAttacking = true;
            //attackState = new MeleeAttackState(); //o lo creo antes y uso uno solo?
            //context.SetState("MeleeAttackState");
            //CREAR ESTADO DE SALIDA
            //nextState = new MeleeAttackState(context);
            Exit();
        }
    }
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
    public void SetAttackRange(float newAtkRange)
    { 
        attackRange = newAtkRange;
    }

    //public void SetContext(EnemyController contextenemyController)
    //{
    //    if (context == null) { context = contextenemyController; }
    //}
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
//TAG: MeleeEnemyMovementState
//private CircleCollider2D _circleCollider2D;

//Transform parent;
//Transform AttackStateComponent;
//MeleeAttackState attackState;

//EnemyController context;
//private string nextStateTag = "MeleeEnemyAttackState";
//private Transform target;

//private NavMeshAgent _agent;
//private float distanceToTarget;
//Vector2 direction;

//[SerializeField] private float attackRange = 0.016f; //valor por defecto, se puede cambiar
//                                                     //[SerializeField] private LayerMask playerLayer;

//public void Enter()
//{
//    parent = transform.parent;
//    if (AttackStateComponent == null) { FindStateByTag(nextStateTag); }
//    Debug.Log("ENTRE AL ESTADO MOVEMENT");
//    target = context.GetTarget();

//    _agent = context.GetComponent<NavMeshAgent>();
//    _agent.SetDestination(target.position);
//    //_circleCollider2D = GetComponent<CircleCollider2D>();
//}

//public void FindStateByTag(string tag)
//{
//    foreach (Transform child in parent)
//    {
//        if (child != transform && child.CompareTag(nextStateTag))
//        {
//            Debug.Log("Hermano encontrado: " + child.name);
//            AttackStateComponent = child;
//            break;
//        }
//    }
//}

//public void UpdateAction()
//{
//    if (target == null) { return; }
//    //distanceToTarget = Vector2.Distance(lastTargetPosition, target.position);
//    distanceToTarget = Vector2.Distance(context.transform.position, target.position);
//    if (distanceToTarget > attackRange)
//    {
//        _agent.SetDestination(target.position);
//    }
//    direction = _agent.velocity.normalized;
//    CheckForAttack();
//}

//public void CheckForAttack()

//{
//    if (target == null) { return; }
//    if (distanceToTarget <= attackRange)
//    {
//        Debug.Log("¡En rango para atacar!");
//        Debug.Log("¡En rango para atacar! Distancia actual: " + distanceToTarget);
//        //attackState = new MeleeAttackState(); //o lo creo antes y uso uno solo?
//        //context.SetState("MeleeAttackState");
//        if (attackState == null) { attackState = AttackStateComponent.GetComponent<MeleeAttackState>(); }
//        attackState.PrepareMeleeAttack(direction);
//        Exit();
//    }
//}
//public void SetAtkRange(float range) //borrable, creo que esta al pedo
//{
//    attackRange = range;
//}
//public void Exit()
//{
//    context.SetState(nextStateTag);
//}

//public void SetContext(EnemyController contextenemyController)
//{
//    if (context == null) { context = contextenemyController; }
//}
////public void OnTriggerEnter2D(Collider2D other)
//// {
////    //if (!other.CompareTag("Player")) return;
////    if (other.CompareTag("Player"))
////    {
////        Vector3 playerDirection = other.transform.position - transform.position;
////        RaycastHit2D rayCast = Physics2D.Raycast(transform.position, playerDirection, _circleCollider2D.radius);

////        if (rayCast.collider == null) return;

////        if (rayCast.collider.CompareTag("Player"))
////        {
////            if (target == null)
////            {
////                _agent.SetDestination(other.transform.position);
////            }
////            Debug.Log("Player Detected!");
////        }

////    }
////}