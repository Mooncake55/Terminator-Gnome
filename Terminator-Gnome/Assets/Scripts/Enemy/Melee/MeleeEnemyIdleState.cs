using System;
using System.Collections;
using UnityEngine;

public class EnemyIdleState : MonoBehaviour, IEnemyState //si no es monoBehav
{
    //TAG: InitialState
    EnemyController context;
    Coroutine animationCoroutine;

    public void Enter()
    {
        Debug.Log($"Entrando al estado Idle");
        if (animationCoroutine == null) { animationCoroutine = StartCoroutine(IdleAnimation()); }
        return;

    }

    public void Exit()
    {
        Debug.Log($"Saliendo del estado Idle");
        if (animationCoroutine == null) { context.SetState("MeleeEnemyMovementState"); }
    }

    public void UpdateAction()
    {
        return;
    }
    public IEnumerator IdleAnimation()
    {
        Debug.Log("IdleAnimation");
        //aca deberia ir alguna animacion de inicio o idle
        yield return new WaitForSeconds(1);
        animationCoroutine = null;
        Exit();
    }

    public void SetContext(EnemyController enemyController)
    {
        if (context == null) { context = enemyController; }
    }

}

