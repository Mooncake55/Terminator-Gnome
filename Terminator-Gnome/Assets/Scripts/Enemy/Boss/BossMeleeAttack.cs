using System;
using System.Collections;
using UnityEngine;

public class BossMeleeAttack : MonoBehaviour
{
    [SerializeField] private Transform rightAttackZone;
    [SerializeField] private Transform leftAttackZone;
    [SerializeField] private float attackDuration = 1.5f;
    [SerializeField] private float maxScale = 1f;
    [SerializeField] private float cooldown = 1f;

    bool hasFinished;

    public void EndCoroutine()
    {
        StopAllCoroutines();
    }
    //public Action OnStartAttack;
    public Action OnFinishedAttack;
    public void ExecuteAttack()
    {
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        yield return StartCoroutine(GrowHitbox(rightAttackZone));
        yield return new WaitForSeconds(cooldown);
        yield return StartCoroutine(GrowHitbox(leftAttackZone));
        Debug.Log("Termiando Melee Atk");
        OnFinishedAttack?.Invoke();
    }
    IEnumerator GrowHitbox(Transform attackZone)
    {
        BoxCollider2D collider = attackZone.GetComponent<BoxCollider2D>();
        collider.enabled = true;

        Vector3 startScale = Vector3.zero;
        Vector3 endScale = new Vector3(1, 1, 1) * maxScale;

        float timer = 0f;
        while (timer < attackDuration)
        {
            timer += Time.deltaTime;
            float t = timer / attackDuration;
            attackZone.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }

        yield return new WaitForSeconds(0.3f); // Tiempo para mantener la hitbox activa
        collider.enabled = false;
        attackZone.localScale = Vector3.zero; // Reset
    }
}
