using System;
using System.Collections;
using UnityEngine;

public class EnemyIdleState : MonoBehaviour, IEnemyState //si no es monoBehav
{
    //public event Action OnStateChange;
    EnemyController context;
    IEnemyState enemyMovevement;
    Coroutine animationCoroutine;

    public void Enter()
    {
        if (animationCoroutine == null) { animationCoroutine = StartCoroutine(IdleAnimation()); }
        return;

    }

    public void Exit()
    {
        if (animationCoroutine != null) ;
        //enemyMovevement = new MeleeMovementState();
        //enemyMovevement.SetContext(context);
        //context.SetState(enemyMovevement);
        context.SetState("MeleeEnemyMovementState");
        //Destroy(this);
    }

    public void UpdateAction()
    {
        return;
    }
    public IEnumerator IdleAnimation()
    {
        //aca deberia ir alguna animacion de inicio o idle
        yield return new WaitForSeconds(1);
        animationCoroutine = null;
        Exit();
    }

    public void SetContext(EnemyController enemyController)
    {
        if (context != null) { context = enemyController; }
    }

}

