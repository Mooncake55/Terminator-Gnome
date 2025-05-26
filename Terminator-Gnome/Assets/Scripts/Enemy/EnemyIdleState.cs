using System.Collections;
using UnityEngine;

public class MeleeEnemyIdleState : MonoBehaviour, IEnemyState //si no es monoBehav
{
    EnemyController context;
    IEnemyState enemyMovevement;
    Coroutine animationCoroutine;
    
    public void Enter()
    {
        animationCoroutine = StartCoroutine(IdleAnimation());

    }

    public void Exit()
    {
        if (animationCoroutine != null) ;
        enemyMovevement = new MeleeMovementState(context);
        context.SetState(enemyMovevement);
        Destroy(this);
    }

    public void Update()
    {
        
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

