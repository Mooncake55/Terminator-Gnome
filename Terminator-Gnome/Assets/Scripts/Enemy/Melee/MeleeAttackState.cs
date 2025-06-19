using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MeleeAttackState : IEnemyState
{
    private EnemyData meleeEnemy;
    private float atkDuration;
    private Transform joint;
    private MeleeEnemyAttack meleeAttack;
    private Vector2 direction;
    EnemyController context;
    private bool isAttacking;
    IEnemyState nextState;
    Animator animator;

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
        atkDuration = meleeEnemy.attackDuration;
        animator = context.GetAnimator();
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
            meleeAttack = meleeAttackObj.GetComponent<MeleeEnemyAttack>();
            if (meleeAttack != null) 
            { 
                Debug.Log("MeleeAttack asignado correctamente: " + meleeAttack.name);
                meleeAttack.SetDamage(meleeEnemy.attackDamage);
            }
            else
                Debug.LogWarning("No se encontr� el componente MeleeAttack en " + meleeAttackObj.name);
        }
        else
        {
            Debug.LogWarning("No se encontr� el objeto hijo EnemyMeleeAtk en " + joint.name);
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
            animator.SetBool("isAttacking", false);
            context.SetState(nextState);
        }
    }
    public IEnumerator Attack()
    {
        isAttacking = true;
        Debug.Log("Deber�a atacar");

        if (meleeAttack == null)
        {
            Debug.LogWarning("meleeAttack es null en Attack()");
        }
        else
        {
            animator.SetBool("isAttacking", true);
            Debug.Log("Llamando a ActivateAttack");
            if (direction != Vector2.zero)
            {
                animator.SetFloat("moveX", Mathf.Abs(direction.x));
                animator.SetFloat("moveY", direction.y);

                // Flip visual si vas a la izquierda (solo si usás sprites mirando a la derecha)
                if (direction.x != 0)
                {
                    context.GetComponent<SpriteRenderer>().flipX = direction.x > 0;
                }
            }
            //meleeAttack.ActivateAttack(direction, meleeEnemy.attackDuration, joint, "isMeleeAttacking");
            meleeAttack.ActivateAttack(direction, meleeEnemy.attackDuration, joint, "isAttacking");
        }
        //yield return new WaitForSeconds(atkDuration);
        yield return new WaitForSeconds(meleeEnemy.attackCooldDown);
        isAttacking = false;
        Exit();
    }

}

