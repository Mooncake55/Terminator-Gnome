using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    private PlayerMovement playerMovement;
    private PlayerDash playerDash; 
    private MeleeAttack meleeAttack;
    //private IAttack attack; //cambiar!


    private HealthSystem healthSystem;
    bool isTakingDamage = false; //for eventual checks and corroborations
    public PlayerData data;
    SpriteRenderer spriteRenderer;
    private Vector2 lastDirection;
    private AttackController attackController;
    private bool isAttacking;

    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform joint;
    [SerializeField] bool isDashing = false; //Serialized in order to do tests easily

    Coroutine damageCoroutine;
    Coroutine dashCoroutine;
    Coroutine attackCoroutine;
   
    public void Init()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.SetLifePoints(data.lifePoints); 
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.SetPlayer(this);
        playerDash = GetComponent<PlayerDash>();
        playerDash.SetPlayer(this);
        lastDirection = Vector2.zero;
        attackController = GetComponent<AttackController>();
        attackController.SetPlayer(this);

        healthSystem.OnDeath += HandleDeath;
        InputController.instance.OnMoveInput += HandleMoveInput;
        InputController.instance.OnShiftPressed += HandleDashInput;
        InputController.instance.OnLeftClickPressed += HandleMeleeAttack;
        InputController.instance.OnRightClickPressed += HandleRangeAttack;
    }
    public PlayerData GetPlayerData() { return data; }
    void HandleMoveInput(Vector2 direction)
    {
        FlipRender(direction);
        if (direction != Vector2.zero) { lastDirection = direction; }
        if (!isDashing) { playerMovement.MovePlayer(direction); }  
    }
    void HandleDashInput() 
    {
        if(dashCoroutine != null) { return; }
        FlipRender(lastDirection);
        dashCoroutine = StartCoroutine(DashCoroutine());
        isDashing = false;
    }
    public void DashTo(Vector2 direction, bool changeDirection) 
    {
        playerDash.Dash(direction, changeDirection);
    }
    IEnumerator DashCoroutine()
    {
        isDashing = true;
        playerDash.Dash(lastDirection, false);
        yield return new WaitForSeconds(data.dashCoolDown);    
        dashCoroutine = null;
    }

    void HandleMeleeAttack() //CAMBIAR
    {
        //if (isAttacking) { return; }
        //isAttacking = true;
        if (attackCoroutine != null) { return; }
        Debug.Log("HandleMELEatk");
        attackController.SetPlayer(this);
        //attackController.ExecuteMeleeAttack();
        //isAttacking = false;
        attackCoroutine = StartCoroutine(AttackCoroutine(0));

    }
    void HandleRangeAttack()
    {
        if (attackCoroutine != null) { return; }
        Debug.Log("HandleRANGEatk");
        attackController.SetPlayer(this);
        //attackController.ExecuteRangeAttack();
        attackCoroutine = StartCoroutine(AttackCoroutine(1));
    }
    IEnumerator AttackCoroutine(int option)
    {
        float cooldDown = data.meleeAtkDuration;
        //isAttacking = true;
        if(option == 0) { attackController.ExecuteMeleeAttack(); }
        else 
        { 
            attackController.ExecuteRangeAttack();
            cooldDown = data.rangeAtkCoolDown;
        }
        yield return new WaitForSeconds(cooldDown);
        attackCoroutine = null;
    }
    public Vector2 GetFacingTo(){
        return lastDirection;
    }

    private void OnDestroy()
    {
        InputController.instance.OnMoveInput -= HandleMoveInput;
        InputController.instance.OnShiftPressed -= HandleDashInput;
        InputController.instance.OnLeftClickPressed -= HandleMeleeAttack;
    }

    //temporary, so the sprite faces in the movement direction
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
    public IEnumerator WaitSeconds(float duration)
    {
        yield return new WaitForSeconds(duration);
    }

    public void HandleDamage(float amount)
    {
        healthSystem.TakeDamage(amount);
        if(damageCoroutine == null) { damageCoroutine = StartCoroutine(ChangeColour(1f)); }     
    }

    //moves the player to the last spawnpoint, restores health and waits for the GameManaer to reactivate
    void HandleDeath()
    {
        //transform.position = spawnPoint.position;
        healthSystem.Heal(data.lifePoints);
        //gameObject.SetActive(false);
        GameManager.instance.ScheduleReactivation(gameObject, 2f);
        //Destroy(gameObject);
    }

    //temporary, to indicate that the player has take damege
    public IEnumerator ChangeColour(float seconds)
    {
        isTakingDamage = true;
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(seconds);
        spriteRenderer.color = originalColor;
        isTakingDamage = false;
        damageCoroutine = null;
    }
}
