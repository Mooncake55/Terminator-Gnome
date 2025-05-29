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
    //private string nextStateTag = "MeleeEnemyMovementState";

    //Coroutine attackCoroutine;

    public MeleeAttackState(EnemyController enemy)
    {
        Debug.Log("Entrando e ATTACK");
        context = enemy;
        meleeEnemy = context.GetEnemyData();
        joint = context.GetJoint();
        Enter();
        GetMeleeAttack();
    }

    public void Enter()
    {
        atkDuration = meleeEnemy.attackkDuration;
    }
    //void GetTarget()
    //{
    //    if (target == null)
    //    {
    //        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
    //        if (playerObj != null)
    //        {
    //            target = playerObj.transform;
    //            lastTargetPosition = target.position;
    //        }
    //    }       
    //}
    public void PrepareMeleeAttack(Vector2 direction)
    {
        direction = direction.normalized;
    }
    private void GetMeleeAttack()
    {
        if (joint == null)
        {
            Debug.LogWarning("Joint no asignado.");
            return;
        }

        Transform meleeAttackObj = joint.Find("EnemyMeleeAtk"); // Usá el nombre actualizado
        if (meleeAttackObj != null)
        {
            meleeAttack = meleeAttackObj.GetComponent<MeleeAttack>();
            if (meleeAttack != null)
                Debug.Log("MeleeAttack asignado correctamente: " + meleeAttack.name);
            else
                Debug.LogWarning("No se encontró el componente MeleeAttack en " + meleeAttackObj.name);
        }
        else
        {
            Debug.LogWarning("No se encontró el objeto hijo EnemyMeleeAtk en " + joint.name);
        }
    }

    public void Exit()
    {
        //context.SetState(nextStateTag);
        if(isAttacking == false)
        {
            nextState = new MeleeMovementState(context);
            
        }
        if(nextState != null)
        {
            context.SetState(nextState);
        }
    }

    //public void SetContext(EnemyController enemyController)
    //{
    //    if (context == null) { context = enemyController; }
    //}

    public void UpdateAction()
    {
        direction = context.GetFaceTo();
        if (!isAttacking) { context.StartStateCoroutine(Attack()); }

    }

    //public void UpdateAction()
    //{
        
    //    if (target == null) { return; }
    //    //direction = _agent.velocity.normalized;
    //    direction = ((Vector2)(target.position - transform.position)).normalized;
    //    //CheckForAttack();
    //    if (attackCoroutine == null) { attackCoroutine = StartCoroutine(Attack()); }
    //}

    public IEnumerator Attack()
    {
        isAttacking = true;
        Debug.Log("Debería atacar");

        if (meleeAttack == null)
        {
            Debug.LogWarning("meleeAttack es null en Attack()");
            //GetMeleeAttack(); // Intenta recuperar la referencia en tiempo de ejecución
        }

        if (meleeAttack != null)
        {
            Debug.Log("Llamando a ActivateAttack");
            meleeAttack.ActivateAttack(direction, 1f, joint);
        }
        else
        {
            Debug.LogError("No se pudo ejecutar el ataque porque meleeAttack sigue siendo null");
        }

        yield return new WaitForSeconds(atkDuration);
        //attackCoroutine = null;
        isAttacking = false;
        Exit();
    }
}
//    //TAG: MeleeAttackState
//    private Transform target;
//    [SerializeField] Transform joint;
//[SerializeField] private float atkDuration;
//private MeleeAttack meleeAttack;
//private Vector2 direction;
//EnemyController context;
//private string nextStateTag = "MeleeEnemyMovementState";

//Coroutine attackCoroutine;
//public void Enter()
//{
//    target = context.GetTarget();
//    meleeAttack = GetComponent<MeleeAttack>();
//    //GetTarget();
//    GetMeleeAttack();

//}
//void GetTarget()
//{
//    if (target == null)
//    {
//        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
//        if (playerObj != null)
//        {
//            target = playerObj.transform;
//            lastTargetPosition = target.position;
//        }
//    }       
//}
//public void PrepareMeleeAttack(Vector2 direction)
//{
//    direction = direction.normalized;
//}

//public void GetMeleeAttack()
//{
//    if (joint == null)
//    {
//        Debug.LogWarning("Joint no asignado.");
//        return;
//    }

//    Transform meleeAttackObj = joint.Find("EnemyMeleeAtk"); // Usá el nombre actualizado
//    if (meleeAttackObj != null)
//    {
//        meleeAttack = meleeAttackObj.GetComponent<MeleeAttack>();
//        if (meleeAttack != null)
//            Debug.Log("MeleeAttack asignado correctamente: " + meleeAttack.name);
//        else
//            Debug.LogWarning("No se encontró el componente MeleeAttack en " + meleeAttackObj.name);
//    }
//    else
//    {
//        Debug.LogWarning("No se encontró el objeto hijo EnemyMeleeAtk en " + joint.name);
//    }
//}

//public void Exit()
//{
//    context.SetState(nextStateTag);
//}

//public void SetContext(EnemyController enemyController)
//{
//    if (context == null) { context = enemyController; }
//}

//public void UpdateAction()
//{
//    if (target == null) { return; }
//    //direction = _agent.velocity.normalized;
//    direction = ((Vector2)(target.position - transform.position)).normalized;
//    //CheckForAttack();
//    if (attackCoroutine == null) { attackCoroutine = StartCoroutine(Attack()); }


//}
//public IEnumerator Attack()
//{
//    Debug.Log("Debería atacar");

//    if (meleeAttack == null)
//    {
//        Debug.LogWarning("meleeAttack era null en Attack(), intentando buscar nuevamente...");
//        GetMeleeAttack(); // Intenta recuperar la referencia en tiempo de ejecución
//    }

//    if (meleeAttack != null)
//    {
//        Debug.Log("Llamando a ActivateAttack");
//        meleeAttack.ActivateAttack(direction, 1f, joint);
//    }
//    else
//    {
//        Debug.LogError("No se pudo ejecutar el ataque porque meleeAttack sigue siendo null");
//    }

//    yield return new WaitForSeconds(atkDuration);
//    attackCoroutine = null;
//    Exit();
//}
