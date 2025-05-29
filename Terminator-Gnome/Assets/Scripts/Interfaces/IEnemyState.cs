using UnityEngine;

public interface IEnemyState
{
    void Enter();
    void UpdateAction();
    void Exit();
    //DEBERIA TENER ALGO INCLUSO PREVIO A EXIT?
    //void SetContext(EnemyController enemyController);
}
