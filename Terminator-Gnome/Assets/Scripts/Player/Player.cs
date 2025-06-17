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
    Coroutine damageCoroutine;
    public PlayerData data;
    SpriteRenderer spriteRenderer;
    private Vector2 lastDirection;
    private AttackController attackController;

    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform joint;
    [SerializeField] bool isDashing; //Serialized in order to do tests easily
   
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
    void HandleDashInput(bool isDashing) 
    {
        FlipRender(lastDirection);
        Debug.Log("Deberia dashear");
        isDashing = true;
        playerDash.Dash(lastDirection, false);
        isDashing = false;
    }
    void HandleMeleeAttack() //CAMBIAR
    {
        // float atkDuration = data.meleeAtkDuration;
        // SearchMeleeAttack();
        // meleeAttack.SetDamage(data.meleeAtkDamage);
        // meleeAttack.ActivateAttack(lastDirection, atkDuration, joint);
        // StartCoroutine(WaitSeconds(atkDuration));
        Debug.Log("HandleMELEatk");
        attackController.SetPlayer(this);
        attackController.ExecuteMeleeAttack();
    }
    void HandleRangeAttack(){
        Debug.Log("HandleRANGEatk");
        attackController.SetPlayer(this);
        attackController.ExecuteRangeAttack();
    }

    public Vector2 GetFacingTo(){
        return lastDirection;
    }


    //gets both the hitbox and the meleeAttack of that hitbox
    // void  SearchMeleeAttack() //cambiar
    // {
    //     if (joint != null)
    //     {
    //         Transform meleeAttackObj = joint.Find("MeleeAtk");
    //         if (meleeAttackObj != null)
    //         {
    //             meleeAttack = meleeAttackObj.GetComponent<MeleeAttack>();
    //         }
    //     }
    // }
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
