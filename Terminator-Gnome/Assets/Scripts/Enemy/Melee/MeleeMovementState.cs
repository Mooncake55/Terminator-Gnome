
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

    [Header("Animation")]
    private Animator animator;

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
        _agent.speed = context.GetEnemyData().moveSpeed;
        attackRange = context.GetEnemyData().attackRange;
        animator = context.GetAnimator();
        Debug.Log(attackRange);
    }

    public void UpdateAction()
    {
        if (target == null) { target = context.GetTarget(); return; }
        direction = _agent.velocity.normalized;
        animator.SetBool("isMoving", true);
        context.SetFaceTo(direction);
        if (target == null) { return; }
        distanceToTarget = Vector2.Distance(context.transform.position, target.position);
        if (distanceToTarget > attackRange)
        {
            _agent.SetDestination(target.position);
            if (direction != Vector2.zero)
            {
                animator.SetFloat("moveX", Mathf.Abs(direction.x));
                animator.SetFloat("moveY", direction.y);

                // Flip visual si vas a la izquierda (solo si usás sprites mirando a la derecha)
                if (direction.x != 0)
                {
                    context.GetComponent<SpriteRenderer>().flipX = direction.x < 0;
                }
            }

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
        animator.SetBool("isMoving", false);
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
