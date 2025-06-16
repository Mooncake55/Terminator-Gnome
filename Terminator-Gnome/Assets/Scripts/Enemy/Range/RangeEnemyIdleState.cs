using UnityEngine;
using System.Collections;
public class RangeEnemyIdleState : IEnemyState
{
    EnemyController context;
    //Coroutine animationCoroutine;
    IEnemyState nextState;
    private bool didCoroutine;

    public RangeEnemyIdleState(EnemyController enemy)
    {
        context = enemy;
        Debug.Log($"Entrando al estado RangeIdle");
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
        yield return new WaitForSeconds(1.5f);
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
            nextState = new RangeAttackState(context);
        }
        if (nextState != null)
        {
            context.SetState(nextState);
        }
    }
}
