using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float dashSpeed = 1f;//also dashDistance
    [SerializeField] private float dashDuration = 0.2f; //dash normal
    private int dashCount = 2;
    public LayerMask obstacleLayer;
    //private bool isDashing = false;//dash normal

    //private Vector2 lastDirection = Vector2.zero;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        //InputController.Instance.OnMoveInput += HandleMoveInput;
        //InputController.Instance.OnShiftPressed += HandleDash;
        //InputController.Instance.OnLeftClickPressed += HandleDash;
    }

    public void Dash(Vector2 direction, bool changeDirection)
    {
        if (changeDirection) { direction = -direction; }
        Vector2 dashTarget = rb.position + direction * dashSpeed;
        Debug.Log($"DashTarget: " + dashTarget);

        // Opcional: chequeo de colisiones para no atravesar paredes
        //Debug.DrawRay(rb.position, direction * dashSpeed, Color.red, dashSpeed);
        //RaycastHit2D hit = Physics2D.Raycast(rb.position, direction, dashSpeed, obstacleLayer);
        //if (hit.collider != null)
        //{
        //    dashTarget = hit.point; // Frenar en la pared
        //    Debug.Log($"DashTarget: " + dashTarget);
        //}
        //yield return new WaitForSeconds(0.1f);
        rb.MovePosition(dashTarget);
        dashTarget = Vector2.zero;
        StartCoroutine(WaitForSeconds(dashDuration));
        Debug.Log($"DashTarget Final: " + dashTarget);
        Debug.Log($"pos: " + rb.position);
    }
    private IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
