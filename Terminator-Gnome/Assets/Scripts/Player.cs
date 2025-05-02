using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerDash playerDash;
    private MeleeAttack playerMeleeAttack;

    [SerializeField] bool isDashing;
    private Vector2 lastDirection;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
        playerMeleeAttack = GetComponentInChildren<MeleeAttack>();
        playerDash = GetComponent<PlayerDash>();
        lastDirection = Vector2.zero;
        InputController.Instance.OnMoveInput += HandleMoveInput;
        InputController.Instance.OnShiftPressed += HandleDashInput;
        InputController.Instance.OnRightClickPressed += HandleMeleeAttack;
    }

    void HandleMoveInput(Vector2 direction)
    {
        FlipRender(direction);
        lastDirection = direction;
        if (!isDashing) { playerMovement.MovePlayer(direction); }  
    }
    void HandleDashInput(bool isDashing) 
    {
        FlipRender(lastDirection);
        Debug.Log("Deberia dashear");
        isDashing = true;
        playerDash.Dash(lastDirection, false);
        isDashing = false;
    }
    void HandleMeleeAttack()
    {
        playerMeleeAttack.ActivateAttack(lastDirection, transform);
    }
    private void OnDestroy()
    {
        InputController.Instance.OnMoveInput -= HandleMoveInput;
        InputController.Instance.OnShiftPressed -= HandleDashInput;
        InputController.Instance.OnRightClickPressed -= HandleMeleeAttack;
    }
    void FlipRender(Vector2 direction)
    {
        if (direction == Vector2.left)
        {
            spriteRenderer.flipX = false;
        }
        if (direction == Vector2.right)
        {
            spriteRenderer.flipX = true;
        }
    }
}
