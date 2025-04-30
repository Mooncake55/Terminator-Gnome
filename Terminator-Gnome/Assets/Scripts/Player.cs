using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerDash playerDash;
    [SerializeField] bool isDashing;
    private Vector2 lastDirection;
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerDash = GetComponent<PlayerDash>();
        lastDirection = Vector2.zero;
        InputController.Instance.OnMoveInput += HandleMoveInput;
        InputController.Instance.OnShiftPressed += HandleDashInput;
    }

    void HandleMoveInput(Vector2 direction)
    {
        lastDirection = direction;
        if (!isDashing) { playerMovement.MovePlayer(direction); }  
    }
    void HandleDashInput(bool isDashing) 
    {
        isDashing = true;
        playerDash.Dash(lastDirection, false);
    }
    void PlayerMove()
    {

    }
    private void OnDestroy()
    {
        InputController.Instance.OnMoveInput -= HandleMoveInput;
        InputController.Instance.OnShiftPressed -= HandleDashInput;
    }

}
