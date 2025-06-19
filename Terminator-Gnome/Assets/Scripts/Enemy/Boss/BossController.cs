using System;
using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour, IDamageable
{
    
    [SerializeField] private float timeBetweenAttacks = 3f;
    [SerializeField] private float idleTime = 3f;
    [SerializeField] private float lifePoints = 100f;
    [SerializeField] private BossMeleeAttack bossMeleeAttack;
    [SerializeField] private BossRangeAttack bossRangeAttack;
    private bool isAttacking = false;
    private bool isAlive = true;
    private HealthSystem healthSystem;
    private Coroutine attackCoroutine;
    private Coroutine idleCoroutine;
    private Coroutine damageCoroutine;
    private bool isTrakingDamage;
    [SerializeField] private float damageCooldown = 1f;
    private bool started = false;

    //public Action OnDeath;
    

    void Start()
    {
        //bossMeleeAttack = GetComponent<BossMeleeAttack>();
        bossMeleeAttack.OnFinishedAttack += StopAttack;
        bossRangeAttack.OnFinishedAttack += StopAttack;
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDeath += HandleDeath;
        healthSystem.SetLifePoints(lifePoints);
        //Init();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !started)
        {
            started = true;
            Init();
        }
    }
    void Init()
    {
        Debug.Log("iniciando BOSS");
        if(idleCoroutine == null)
        {
            idleCoroutine = StartCoroutine(Idle(0));
        }
    }

    IEnumerator Idle(int option)
    {
        Debug.Log("Boss Idle");
        yield return new WaitForSeconds(idleTime);
        if (!isAlive) yield break;
        idleCoroutine = null;
        if (attackCoroutine == null && option == 0) { attackCoroutine = StartCoroutine(MeleeAttack()); }
        if(attackCoroutine == null && option == 1) {  attackCoroutine = StartCoroutine(RangeAttack());}
    }
    IEnumerator MeleeAttack()
    {
        Debug.Log("Boss Melee Atk");
        isAttacking = true;
        bossMeleeAttack.ExecuteAttack();
        yield return new WaitUntil(() => isAttacking == false || !isAlive);
        if (!isAlive) yield break;
        attackCoroutine = null;
        idleCoroutine = StartCoroutine(Idle(1));
        //idleCoroutine = StartCoroutine(Idle(0));
    }
    void StopAttack()
    {
        isAttacking = false;
    }
    IEnumerator RangeAttack()
    {
        Debug.Log("Boss Range Atk");
        isAttacking = true;
        bossRangeAttack.ExecuteAttack();
        yield return new WaitUntil(() => isAttacking == false || !isAlive);
        if (!isAlive) yield break;
        attackCoroutine = null;
        idleCoroutine = StartCoroutine(Idle(0));
    }
    public void HandleDamage(float amount)
    {
        if(damageCoroutine == null) 
        {
            Debug.Log($"Daño recibido: {amount}");
            Debug.Log($"Vida antes del daño: {healthSystem._actualHealth}");
            healthSystem.TakeDamage(amount);
            damageCoroutine = StartCoroutine(DamageCoroutine());
            Debug.Log($"Vida despues del daño: {healthSystem._actualHealth}");
        }
        
    }
    IEnumerator DamageCoroutine()
    {
        yield return new WaitForSeconds(damageCooldown);
        damageCoroutine = null;
    }
    public IEnumerator ChangeColour(float seconds)
    {
        //isTakingDamage = true;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(seconds);
        spriteRenderer.color = originalColor;
        //isTakingDamage = false;
        //damageCoroutine = null;
    }
    void HandleDeath()
    {
        Debug.Log("MATASTE AL BOSS; GANASTE :)");
        bossMeleeAttack.EndCoroutine();
        isAlive = false;
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
