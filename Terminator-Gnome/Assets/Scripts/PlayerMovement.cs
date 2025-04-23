using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    int moveCount = 0;
    [SerializeField] private float speed = 5f;
    private Rigidbody2D rb;
    private bool isPaused = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InputController.Instance.OnMoveInput += HandleMoveInput;
    }

    void MovePlayer(Vector2 moveInput)
    {
        Debug.Log($"Horizontal: " + moveInput.x);
        Debug.Log($"Vertical: " + moveInput.y);
        moveCount++;
        Debug.Log($"move count" + moveCount);
        rb.velocity = moveInput * speed; //rb.volocity
        moveInput = Vector2.zero;
    }
    void HandleMoveInput(Vector2 moveInput)
    {
        if (isPaused) return;
        else 
        {
            MovePlayer(moveInput);
            //moveInput = Vector2.zero;
        }
    }

}
