using System.Collections;
using UnityEngine;

public class RangeAttackState : IEnemyState
{
    private Transform target;
    private EnemyController context;
    private float attackRange;
    ProjectileFactory factory;
    private bool isAttacking;
    Animator animator; 
    public RangeAttackState(EnemyController enemy)
    {
        Debug.Log("Entrando a RangeAttackState");
        context = enemy;
        factory = context.GetFactory();    
        Enter();
    }
    public void Enter()
    {
        if (animator == null)
        {
            animator = context.GetAnimator();
        }
        //animator = context.GetAnimator();
        target = context.GetTarget();
        attackRange = context.GetEnemyData().attackRange;
    }

    public void UpdateAction()
    {
        if(target == null) { target = context.GetTarget(); return; }
            float distanceToTarget = Vector2.Distance(context.transform.position, target.position);
        if (distanceToTarget > attackRange)
        {
            if (!isAttacking) { context.StartStateCoroutine(Attack()); }
        }
        else
        {
            if (!isAttacking) { context.StartStateCoroutine(Attack()); }
        }
    }
    public IEnumerator Attack()
    {       
        animator.SetBool("isAttacking", true);
        isAttacking = true;
        yield return new WaitForSeconds(context.GetEnemyData().attackCooldDown);
        Vector2 direction = ((Vector2)target.position - (Vector2)context.transform.position).normalized;
        factory.Fire(direction);
        isAttacking = false;
    }
    public void Exit()
    {
        animator.SetBool("isAttacking", false);
        throw new System.NotImplementedException();

    }
}
