using System.Collections;
using UnityEngine;

public class BasicSwordAttackState : IPlayerAttackState
{
    private AttackData data;
    private Transform joint;
    private MeleeAttack meleeAttack;
    private Vector2 direction;
    AttackController context;
    private bool isAttacking;
    IPlayerAttackState nextState;
    Animator animator;
    float damage;
    float cooldown;
    float duration;
    string animation;


    public BasicSwordAttackState(AttackController controller, AttackData atkData, string animationParam, MeleeAttack attack) //CAMBIAR STATE POR STRATEGY
    {
        context = controller;
        joint = context.GetJoint();
        data = atkData;
        animation = animationParam;
        meleeAttack = attack;
        meleeAttack.SetAnimationParam(animation);
        Enter();
        //GetMeleeAttack();
    }
    //Sets the enemyData values
    public void Enter()
    {
        damage = data.damage;
        cooldown = data.cooldown;
        duration = data.duration;
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
                meleeAttack.SetDamage(damage);
            }
            else
                Debug.LogWarning("No se encontr? el componente MeleeAttack en " + meleeAttackObj.name);
        }
        else
        {
            Debug.LogWarning("No se encontr? el objeto hijo EnemyMeleeAtk en " + joint.name);
        }
    }
    public void Execute(Vector2 dir)
    {
        //direction = context.GetFaceTo();
        direction = dir;
        if (!isAttacking) { context.StartStateCoroutine(Attack()); }
    }
    //controls to know which state to exit
    public void Exit()
    {
        context.HasFinishAttack();
    }
    public IEnumerator Attack()
    {
        isAttacking = true;
        //Debug.Log("Deberia atacar");

        if (meleeAttack == null)
        {
            Debug.Log("meleeAttack es null en Attack()");
        }
        else
        {
            Debug.Log("Llamando a ActivateAttack");
            if (direction != Vector2.zero)
            {
                //animator.SetFloat("moveX", Mathf.Abs(direction.x));
                //animator.SetFloat("moveY", direction.y);

                // Flip visual si vas a la izquierda (solo si usás sprites mirando a la derecha)                
            }
            //meleeAttack.ActivateAttack(direction, meleeEnemy.attackDuration, joint, "isMeleeAttacking");
            meleeAttack.ActivateAttack(direction, duration, joint, animation);
        }
        //yield return new WaitForSeconds(atkDuration);
        yield return new WaitForSeconds(cooldown);
        isAttacking = false;
        Exit();
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }
}
