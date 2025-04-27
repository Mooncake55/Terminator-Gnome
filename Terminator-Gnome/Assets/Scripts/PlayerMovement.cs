using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f; 
    [SerializeField] private float dashSpeed = 1f;//also dashDistance
    [SerializeField] private float dashDuration = 0.2f; //dash normal
    private int dashCount = 2;
    public LayerMask obstacleLayer;
    private bool isDashing = false;//dash normal

    private Vector2 lastDirection = Vector2.zero;
    private Rigidbody2D rb;
    private bool isPaused = false;
    
    void Start()
    {
        //GetComponent<Rigidbody2D>().gravityScale = 0f;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        InputController.Instance.OnMoveInput += HandleMoveInput;
        InputController.Instance.OnShiftPressed += HandleDash;
    }

    void MovePlayer(Vector2 direction)
    {
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
    void HandleDash(Vector2 direction, bool normalDash)
    {
        if (!isDashing && direction != Vector2.zero && dashCount > 0)
        {
            if (normalDash)
            {
                //Dash(lastDirection, false); 
                StartCoroutine(DashTp(direction, false));
                return;
            }
            else
            {
                //Dash(lastDirection, true);
                DashTp(direction, true);
                return;
            }
        }
        
    }
    //borrar
    void Dash(Vector2 direction, bool changeDirection)
    {
        Debug.Log("Deberia Dashear");
        if (changeDirection) { direction = -direction; }
        rb.linearVelocity = direction * (speed + dashSpeed);
        isDashing = false;
    }

    //void DashTp(Vector2 direction, bool changeDirection)
    //{
    //    isDashing = true;
    //    if (changeDirection) { direction = -direction; }
    //    Vector2 dashTarget = rb.position + direction * dashSpeed;
    //    Debug.Log($"DashTarget: " + dashTarget);

    //    // Opcional: chequeo de colisiones para no atravesar paredes
    //    Debug.DrawRay(rb.position, direction * dashSpeed, Color.red, dashSpeed);
    //    RaycastHit2D hit = Physics2D.Raycast(rb.position, direction, dashSpeed, obstacleLayer);
    //    if (hit.collider != null)
    //    {
    //        dashTarget = hit.point; // Frenar en la pared
    //        Debug.Log($"DashTarget: " + dashTarget);
    //    }

    //    rb.MovePosition(dashTarget);
    //    dashTarget = Vector2.zero;
    //    Debug.Log($"DashTarget Final: " + dashTarget);
    //    Debug.Log($"pos: " + rb.position);
    //    isDashing = false;
        
    //}
    IEnumerator DashTp(Vector2 direction, bool changeDirection)
    {
        isDashing = true;
        if (changeDirection) { direction = -direction; }
        Vector2 dashTarget = rb.position + direction * dashSpeed;
        Debug.Log($"DashTarget: " + dashTarget);

        // Opcional: chequeo de colisiones para no atravesar paredes
        Debug.DrawRay(rb.position, direction * dashSpeed, Color.red, dashSpeed);
        RaycastHit2D hit = Physics2D.Raycast(rb.position, direction, dashSpeed, obstacleLayer);
        if (hit.collider != null)
        {
            dashTarget = hit.point; // Frenar en la pared
            Debug.Log($"DashTarget: " + dashTarget);
        }
        yield return new WaitForSeconds(0.1f);
        rb.MovePosition(dashTarget);
        dashTarget = Vector2.zero;
        yield return new WaitForSeconds(dashDuration);
        Debug.Log($"DashTarget Final: " + dashTarget);
        Debug.Log($"pos: " + rb.position);
        isDashing = false;

    }
    //private IEnumerator DashWait()
    //{
    //    yield await for return 
    //}
}

