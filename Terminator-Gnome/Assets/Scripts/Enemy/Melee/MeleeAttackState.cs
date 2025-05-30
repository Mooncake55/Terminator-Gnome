using System;
using System.Collections;
using UnityEngine;

public class MeleeAttackState : IEnemyState
{
    private EnemyData meleeEnemy;
    private float atkDuration;
    private Transform joint;
    private MeleeAttack meleeAttack;
    private Vector2 direction;
    EnemyController context;
    private bool isAttacking;
    IEnemyState nextState;

    public MeleeAttackState(EnemyController enemy)
    {
        Debug.Log("Entrando e ATTACK");
        context = enemy;
        meleeEnemy = context.GetEnemyData();
        joint = context.GetJoint();
        Enter();
        GetMeleeAttack();
    }
    //Sets the enemyData values
    public void Enter()
    {
        atkDuration = meleeEnemy.attackkDuration;
    }
     //gets the meleeAttack hitbox and its component
    private void GetMeleeAttack()
    {
        if (joint == null)
        {
            Debug.LogWarning("Joint no asignado.");
            return;
        }

        Transform meleeAttackObj = joint.Find("EnemyMeleeAtk");
        if (meleeAttackObj != null)
        {
            meleeAttack = meleeAttackObj.GetComponent<MeleeAttack>();
            if (meleeAttack != null) 
            { 
                Debug.Log("MeleeAttack asignado correctamente: " + meleeAttack.name);
                meleeAttack.SetDamage(meleeEnemy.attackDamage);
            }
            else
                Debug.LogWarning("No se encontró el componente MeleeAttack en " + meleeAttackObj.name);
        }
        else
        {
            Debug.LogWarning("No se encontró el objeto hijo EnemyMeleeAtk en " + joint.name);
        }
    }

    public void UpdateAction()
    {
        direction = context.GetFaceTo();
        if (!isAttacking) { context.StartStateCoroutine(Attack()); }
    }
    //controls to know which state to exit
    public void Exit()
    {
        if (isAttacking == false)
        {
            nextState = new MeleeMovementState(context);

        }
        if (nextState != null)
        {
            context.SetState(nextState);
        }
    }
    public IEnumerator Attack()
    {
        isAttacking = true;
        Debug.Log("Debería atacar");

        if (meleeAttack == null)
        {
            Debug.LogWarning("meleeAttack es null en Attack()");
        }
        else
        {
            Debug.Log("Llamando a ActivateAttack");
            meleeAttack.ActivateAttack(direction, 1f, joint);
        }
        yield return new WaitForSeconds(atkDuration);
        isAttacking = false;
        Exit();
    }
}

