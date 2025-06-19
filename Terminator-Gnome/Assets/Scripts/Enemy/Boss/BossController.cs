using System;
using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour, IDamageable
{
    private BossMeleeAttack bossMeleeAttack;
    private BossRangeAttack bossRangeAttack;
    [SerializeField] private float timeBetweenAttacks = 3f;
    [SerializeField] private float idleTime = 3f;
    private bool isAttacking = false;
    private bool isAlive = true;
    private HealthSystem healthSystem;
    private Coroutine attackCoroutine;
    private Coroutine idleCoroutine;

    //public Action OnDeath;
    

    void Start()
    {
        bossMeleeAttack = GetComponent<BossMeleeAttack>();
        healthSystem = GetComponent<HealthSystem>();
    }

    void Init()
    {
        if(idleCoroutine == null)
        {
            idleCoroutine = StartCoroutine(Idle(0));
        }
    }

    IEnumerator Idle(int option)
    {
        yield return new WaitForSeconds(idleTime);
        if (!isAlive) yield break;
        idleCoroutine = null;
        if (attackCoroutine == null && option == 0) { attackCoroutine = StartCoroutine(MeleeAttack()); }
        if(attackCoroutine == null && option == 1) {  attackCoroutine = StartCoroutine(RangeAttack());}
    }
    IEnumerator MeleeAttack()
    {
        isAttacking = true;
        bossMeleeAttack.ExecuteAttack();
        yield return new WaitUntil(() => isAttacking == false || !isAlive);
        if (!isAlive) yield break;
        attackCoroutine = null;
        //idleCoroutine = StartCoroutine(Idle(1));
        idleCoroutine = StartCoroutine(Idle(0));
    }
    void StopAttack()
    {
        isAttacking = false;
    }
    IEnumerator RangeAttack()
    {
        isAttacking = true;
        //bossRangeAttack.ExecuteAttack();

        yield return new WaitUntil(() => isAttacking == false || !isAlive);
        if (!isAlive) yield break;
        attackCoroutine = null;
        idleCoroutine = StartCoroutine(Idle(0));
    }
    public void HandleDamage(float amount)
    {
        OnDeath?.Invoke();
        isAlive = false;
        StopAllCoroutines(); 
    }
}
