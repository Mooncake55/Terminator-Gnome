using UnityEngine;

public interface IPlayerAttackState
{
    void Enter();
    void Update();
    void Exit();
    void Execute();
    void Execute(Vector2 dir);
   
}
