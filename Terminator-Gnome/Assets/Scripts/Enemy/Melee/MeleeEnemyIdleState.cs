using System;
using System.Collections;
using UnityEngine;

public class MeleeEnemyIdleState : IEnemyState
{
    //TAG: InitialState
    EnemyController context;
    Coroutine animationCoroutine;
    IEnemyState nextState;
    private bool didCoroutine;


    public MeleeEnemyIdleState(EnemyController enemy)
    {
        context = enemy;
        Debug.Log($"Entrando al estado Idle");
    }

    //public void Enter()
    //{
    //    Debug.Log($"Entrando al estado Idle");
    //    if (animationCoroutine == null) { animationCoroutine = StartCoroutine(IdleAnimation()); }
    //    return;

    //}

    public void UpdateAction()
    {      
        if (didCoroutine == false) { context.StartStateCoroutine(IdleAnimation()); }
    }
    public IEnumerator IdleAnimation()
    {
        didCoroutine = true;
        Debug.Log("IdleAnimation");
        //aca deberia ir alguna animacion de inicio o idle
        yield return new WaitForSeconds(3);
        //animationCoroutine = null;
        context.StopCurrentCoroutine();       
        Exit();
        didCoroutine = false;
    }

    public void SetContext(EnemyController enemyController)
    {
        if (context == null) { context = enemyController; }
    }
    public void Exit()
    {
        Debug.Log($"SALIENDO DE IDLE");

        if (didCoroutine)
        {
            nextState = new MeleeMovementState(context);
        }
        if (nextState != null)
        {
            context.SetState(nextState);
        }

    }
}

////TAG: InitialState
//EnemyController context;
//Coroutine animationCoroutine;

//public void Enter()
//{
//    Debug.Log($"Entrando al estado Idle");
//    if (animationCoroutine == null) { animationCoroutine = StartCoroutine(IdleAnimation()); }
//    return;

//}

//public void Exit()
//{
//    Debug.Log($"Saliendo del estado Idle");
//    if (animationCoroutine == null) { context.SetState("MeleeEnemyMovementState"); }
//}

//public void UpdateAction()
//{
//    return;
//}
//public IEnumerator IdleAnimation()
//{
//    Debug.Log("IdleAnimation");
//    //aca deberia ir alguna animacion de inicio o idle
//    yield return new WaitForSeconds(1);
//    animationCoroutine = null;
//    Exit();
//}

//public void SetContext(EnemyController enemyController)
//{
//    if (context == null) { context = enemyController; }
//}