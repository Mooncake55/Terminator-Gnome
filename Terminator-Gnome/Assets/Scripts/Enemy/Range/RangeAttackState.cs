using System.Collections;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class RangeAttackState : IEnemyState
{
    private Transform target;
    private EnemyController context;
    private float attackRange;
    ProjectileFactory factory;
    private bool isAttacking;
    public RangeAttackState(EnemyController enemy)
    {
        Debug.Log("Entrando a RangeAttackState");
        context = enemy;
        factory = context.GetFactory();
        Enter();
    }
    public void Enter()
    {
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
        isAttacking = true;
        yield return new WaitForSeconds(2f);
        Vector2 direction = ((Vector2)target.position - (Vector2)context.transform.position).normalized;
        factory.Fire(direction);
        isAttacking = false;
    }
    public void Exit()
    {
        throw new System.NotImplementedException();

    }
}
