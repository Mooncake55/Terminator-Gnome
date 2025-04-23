using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    int moveCount = 0;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float dashSpeed = 5f;
    [SerializeField] private float dashDuration = 0.2f;
    private Vector2 lastDirection = Vector2.zero;
    private Rigidbody2D rb;
    private bool isPaused = false;

    bool isDashing = false;
    
    void Start()
    {
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        rb = GetComponent<Rigidbody2D>();
        InputController.Instance.OnMoveInput += HandleMoveInput;
        InputController.Instance.OnShiftPressed += HandleDash;
    }

    void MovePlayer(Vector2 direction)
    {
        //Debug.Log($"Horizontal: " + moveInput.x);
        //Debug.Log($"Vertical: " + moveInput.y);
        //moveCount++;
        //Debug.Log($"move count" + moveCount);
        float originalSpeed = speed;
        if (isDashing)
        {
            Dash(direction, false);
            return;
        }
        rb.linearVelocity = direction * speed;
        speed = originalSpeed;
        isDashing = false;
        //moveInput = Vector2.zero;
    }
    void HandleMoveInput(Vector2 moveInput)
    {
        if (isPaused) return;
        lastDirection = moveInput;
        MovePlayer(lastDirection);
    }
    void HandleDash(bool dash)
    {
        if (dash)
        {
            Dash(lastDirection, false) ; return;
        }
        else 
        {
            Dash(lastDirection, true) ; return;
        }
    }

    System.Collections.IEnumerator Dash(Vector2 direction, bool changeDirection)
    {
        if (changeDirection) { direction = -direction; }
        rb.linearVelocity = direction * (speed + dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
    }

}
