using System;
using System.Collections;
using UnityEngine;

public class BossMeleeAttack : MonoBehaviour
{
    [SerializeField] private Transform rightAttackZone;
    [SerializeField] private Transform leftAttackZone;
    [SerializeField] private float attackDuration = 1.5f;
    //[SerializeField] private float maxScale = 1f;
    [SerializeField] private float cooldown = 1f;
    [SerializeField] private SpriteRenderer rightHitbox;
    [SerializeField] private SpriteRenderer leftHitbox;
    private Vector3 maxScale;

    bool hasFinished;
    [SerializeField] Animator animator; 

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
    private void Start()
    {
        maxScale = rightAttackZone.localScale;
    }
    IEnumerator AttackCoroutine()
    {
        yield return StartCoroutine(GrowHitbox(rightAttackZone, "MeleeR", rightHitbox));
        yield return new WaitForSeconds(cooldown);
        yield return StartCoroutine(GrowHitbox(leftAttackZone, "MeleeL", leftHitbox));
        Debug.Log("Termiando Melee Atk");
        OnFinishedAttack?.Invoke();
    }
    IEnumerator GrowHitbox(Transform attackZone, string aniamtionParam, SpriteRenderer hitBox)
    {
        BoxCollider2D collider = attackZone.GetComponent<BoxCollider2D>();
        //collider.enabled = true;
        if (collider == null) yield break;

        Vector3 startScale = Vector3.zero;
        //Vector3 endScale = new Vector3(1, 1, 1) * maxScale;
        //Vector3 endScale = attackZone.localScale;
        Vector3 endScale = maxScale;
        animator.SetBool(aniamtionParam, true);
        float timer = 0f;
        hitBox.enabled = true;
        while (timer < attackDuration)
        {
            timer += Time.deltaTime;
            float t = timer / attackDuration;
            attackZone.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
            if (!collider.enabled && t > 0.2f)
            {
                collider.enabled = true;
            }
        }
        animator.SetBool(aniamtionParam, false);
        //yield return new WaitForSeconds(0.3f); // Tiempo para mantener la hitbox activa
        collider.enabled = false;
        attackZone.localScale = Vector3.zero; // Reset
        hitBox.enabled = false;
    }
}
