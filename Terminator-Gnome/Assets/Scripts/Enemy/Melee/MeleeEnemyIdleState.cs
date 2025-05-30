using System;
using System.Collections;
using UnityEngine;

public class MeleeEnemyIdleState : IEnemyState
{
    EnemyController context;
    //Coroutine animationCoroutine;
    IEnemyState nextState;
    private bool didCoroutine;

    public MeleeEnemyIdleState(EnemyController enemy)
    {
        context = enemy;
        Debug.Log($"Entrando al estado Idle");
    }

    //Sets the enemiData values, in this case, none
    public void Enter()
    {
        return;

    }

    public void UpdateAction()
    {      
        if (didCoroutine == false) { context.StartStateCoroutine(IdleAnimation()); }
    }
    public IEnumerator IdleAnimation()
    {
        didCoroutine = true;
        Debug.Log("IdleAnimation");
        //here there should be a particle implementation
        yield return new WaitForSeconds(3);
        context.StopCurrentCoroutine();       
        Exit();
        didCoroutine = false;
    }
    public void SetContext(EnemyController enemyController)
    {
        if (context == null) { context = enemyController; }
    }
    //controls to know which state to exit
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
