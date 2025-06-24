using System;
using System.Collections;
using UnityEngine;

public class MeleeEnemyAttack : MonoBehaviour
{
    private float damage;
    private SpriteRenderer hitBoxSprite;
    [SerializeField] private Animator animator;
    private BoxCollider2D hitBoxCollider;
    private void Start()
    {
        //gameObject.SetActive(false);
        hitBoxSprite = GetComponent<SpriteRenderer>();
        //animator = GetComponentInParent<Animator>();
        hitBoxCollider = GetComponent<BoxCollider2D>();
        hitBoxCollider.enabled = false;
    }
    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    //Uses the joint to pivot and position the hitbox in front
    void SetPosition(Vector2 dir, Transform joint)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        joint.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    public void ActivateAttack(Vector2 dir, float duration, Transform joint, string animationParam)
    {
        //gameObject.SetActive(true);
        //animator.SetBool(animationParam, true);
        hitBoxCollider.enabled = true;
        SetPosition(dir, joint);
        StartCoroutine(DoAttack(duration, animationParam));
    }

    private IEnumerator DoAttack(float duration, string animationParam)
    {
        //Debug.Log("ATACANDO DESDE MELEE ATTACK");
        hitBoxSprite.color = Color.red;
        yield return new WaitForSeconds(duration);
        //gameObject.SetActive(false);
        animator.SetBool(animationParam, false);
        hitBoxCollider.enabled = false;
    }
    //when the hitbox is set as active, triggers this event and tells the Damageable
    //GO how much damage takes
    void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable enemy = other.GetComponent<IDamageable>();
        if (enemy != null)
        {
            enemy.HandleDamage(damage);
        }
    }

    public void ExecuteAttack()
    {
        throw new NotImplementedException();
    }
}
