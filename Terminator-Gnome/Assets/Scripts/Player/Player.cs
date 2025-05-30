using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    private PlayerMovement playerMovement;
    private PlayerDash playerDash; 
    private MeleeAttack meleeAttack;
    //float meleeAtkDuration;
    private HealthSystem healthSystem;
    bool isTakingDamage = false; //for eventual checks and corroborations
    Coroutine damageCoroutine;
    public PlayerData data;
    SpriteRenderer spriteRenderer;
    private Vector2 lastDirection;

    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform joint;
    [SerializeField] bool isDashing; //Serialized in order to do tests easily
   
    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.SetLifePoints(data.lifePoints); 
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.SetPlayer(this);
        playerDash = GetComponent<PlayerDash>();
        playerDash.SetPlayer(this);
        lastDirection = Vector2.zero;

        healthSystem.OnDeath += HandleDeath;
        InputController.instance.OnMoveInput += HandleMoveInput;
        InputController.instance.OnShiftPressed += HandleDashInput;
        InputController.instance.OnRightClickPressed += HandleMeleeAttack;
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
    void HandleMeleeAttack()
    {
        float atkDuration = data.meleeAtkDuration;
        SearchMeleeAttack();
        meleeAttack.SetDamage(data.meleeAtkDamage);
        meleeAttack.ActivateAttack(lastDirection, atkDuration, joint);
        StartCoroutine(WaitSeconds(atkDuration));
    }

    //gets both the hitbox and the meleeAttack of that hitbox
    void  SearchMeleeAttack()
    {
        if (joint != null)
        {
            Transform meleeAttackObj = joint.Find("MeleeAtk");
            if (meleeAttackObj != null)
            {
                meleeAttack = meleeAttackObj.GetComponent<MeleeAttack>();
            }
        }
    }
    private void OnDestroy()
    {
        InputController.instance.OnMoveInput -= HandleMoveInput;
        InputController.instance.OnShiftPressed -= HandleDashInput;
        InputController.instance.OnRightClickPressed -= HandleMeleeAttack;
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
        transform.position = spawnPoint.position;
        healthSystem.Heal(data.lifePoints);
        gameObject.SetActive(false);
        GameManager.instance.ScheduleReactivation(gameObject, 3f);
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
