using System;
using UnityEngine;

public class MeleeAttackState : IEnemyState
{
    [SerializeField] private Transform target;
    [SerializeField] Transform joint;
    [SerializeField] private float atkDuration;
    private MeleeAttack meleeAttack;

    Coroutine attackCoroutine;

    public MeleeAttackState()
    {

    }
    public void Enter()
    {
        target = context.GetTarget();
        meleeAttack = GetComponent<MeleeAttack>();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    public void SetContext(EnemyController enemyController)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateAction()
    {
        throw new System.NotImplementedException();
    }
    public void CheckForAttack()
    {
        ////SearchMeleeAttack();
        //if (direction != Vector2.zero)
        //{
        //    // Dibuja la línea de detección (solo visual)
        //    Debug.DrawRay(transform.position, direction * 0.16f, Color.red);

        //    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.16f, playerLayer);
        //    if (hit.collider != null && hit.collider.CompareTag("Player") && attackCoroutine == null)
        //    {
        //        attackCoroutine =  StartCoroutine(Attack());
        //    }
        //}
        // Dibuja la línea de detección (solo visual)
        direction = ((Vector2)(target.position - transform.position)).normalized;
        if (target == null) { return; }

        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget <= attackRange && attackCoroutine == null)
        {
            attackCoroutine = StartCoroutine(Attack());
        }
    }
}
