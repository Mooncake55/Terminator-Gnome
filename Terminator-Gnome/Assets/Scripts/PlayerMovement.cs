using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    int moveCount = 0;
    void Start()
    {
        InputController.Instance.OnMoveInput += MovePlayer;
    }

    void MovePlayer(Vector2 direction)
    {
        Debug.Log(direction.x);
        Debug.Log(direction.y);
        moveCount++;
    }
}
