using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour, IDamageable
{
    private PlayerMovement playerMovement;
    private PlayerDash playerDash;
    [SerializeField] Transform joint; 
    private MeleeAttack meleeAttack;
    //[SerializeField] float meleeAtkDuration = 1f;
    float meleeAtkDuration;
    private HealthSystem healthSystem;
    [SerializeField] Transform spawnPoint;
    bool isTakingDamage = false;
    Coroutine damageCoroutine;
    public PlayerData data;

    [SerializeField] bool isDashing;
    private Vector2 lastDirection;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        healthSystem = GetComponent<HealthSystem>(); //vida
        healthSystem.SetLifePoints(data.lifePoints);
        healthSystem.OnDeath += HandleDeath;
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        playerMovement = GetComponent<PlayerMovement>(); //movimiento
        playerMovement.SetPlayer(this);
        playerDash = GetComponent<PlayerDash>(); //dash
        playerDash.SetPlayer(this);
        lastDirection = Vector2.zero;
        InputController.Instance.OnMoveInput += HandleMoveInput;
        InputController.Instance.OnShiftPressed += HandleDashInput;
        InputController.Instance.OnRightClickPressed += HandleMeleeAttack;
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
        //checkear validaciones 
        meleeAttack.ActivateAttack(lastDirection, atkDuration, joint);
        StartCoroutine(WaitSeconds(atkDuration));
    }
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

    public void HandleDamage(int amount)
    {
        //logica de da�o que falte 
        healthSystem.TakeDamage(10);
        ChangeColour(2f);
    }

    void HandleDeath()
    {
        //StartCoroutine(WaitSeconds(3f));
        transform.position = spawnPoint.position;
        healthSystem.Heal(20);
        gameObject.SetActive(false);
        GameManager.instance.ScheduleReactivation(gameObject, 3f);
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
