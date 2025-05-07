using System.Collections;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Player : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerDash playerDash;
    private MeleeAttack horizontalPlayerMeleeAttack;
    private MeleeAttack verticallPlayerMeleeAttack;
    private MeleeAttack activeMeleeAttack;
    [SerializeField] float meleeAtkDuration = 1f;
    private Health_System healthSystem;
    bool isTakingDamage = false;
    Coroutine damageCoroutine;


    [SerializeField] bool isDashing;
    private Vector2 lastDirection;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        healthSystem = GetComponent<Health_System>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
        horizontalPlayerMeleeAttack = transform.Find("MeleeAtk(Horizontal)").GetComponent<MeleeAttack>();
        verticallPlayerMeleeAttack = transform.Find("MeleeAtk(Vertical)").GetComponent<MeleeAttack>();

        playerDash = GetComponent<PlayerDash>();
        lastDirection = Vector2.zero;
        InputController.Instance.OnMoveInput += HandleMoveInput;
        InputController.Instance.OnShiftPressed += HandleDashInput;
        InputController.Instance.OnRightClickPressed += HandleMeleeAttack;
    }

    void HandleMoveInput(Vector2 direction)
    {
        FlipRender(direction);
        if (direction != Vector2.zero) { lastDirection = direction; }
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
        activeMeleeAttack = horizontalPlayerMeleeAttack;
        
        if (lastDirection == Vector2.up || lastDirection == Vector2.down)
        {
            activeMeleeAttack = verticallPlayerMeleeAttack;
        }
        else if (lastDirection == Vector2.right || lastDirection == Vector2.left)
        {
            activeMeleeAttack = horizontalPlayerMeleeAttack;
        }
        //activeMeleeAttack.transform.localPosition.Set(activeMeleeAttack.transform.localPosition.x * lastDirection.x, activeMeleeAttack.transform.localPosition.y * lastDirection.y, 0);//orienta arriba o abajo segun la direccion
        activeMeleeAttack.ActivateAttack(lastDirection, meleeAtkDuration);
        StartCoroutine(WaitSeconds(meleeAtkDuration));
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
    IEnumerator WaitSeconds(float duration)
    {
        yield return new WaitForSeconds(duration);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isTakingDamage)
        {
            healthSystem.TakeDamage(3);
            damageCoroutine = StartCoroutine(ChangeColour(2f));
        }
        
    }
    public IEnumerator ChangeColour(float seconds)
    {
        isTakingDamage = true;
        Color originColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(seconds);
        spriteRenderer.color = originColor;
        isTakingDamage = true;
        damageCoroutine = null;

    }
}
