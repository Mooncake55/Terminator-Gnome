using System;
using System.Collections;
using UnityEngine;


public class MeleeAttack : MonoBehaviour
{
    private float damage;
    //private SpriteRenderer jointSprite;
    private BoxCollider2D hitBoxCollider;

    [Header("Animation")]
    private Animator animator;
    private bool isAttacking;
    private bool isRangeAttacking;
    [SerializeField] private SpriteRenderer weaponSpriteRenderer;
    [SerializeField] private SpriteRenderer jointSpriteRenderer;

    private void Start()
    {
        //gameObject.SetActive(false);
        hitBoxCollider = GetComponent<BoxCollider2D>();
        hitBoxCollider.enabled = false;
        animator = GetComponentInParent<Animator>();
        weaponSpriteRenderer.enabled = true;
    }
    void LateUpdate()
    {
        float zRotation = transform.eulerAngles.z;

        // Si rota más de 90° o menos de 270°, está "dada vuelta"
        bool shouldFlip = zRotation > 90f && zRotation < 270f;
        if(weaponSpriteRenderer == null) { weaponSpriteRenderer = GetComponent<SpriteRenderer>(); }
        if (weaponSpriteRenderer != null)
        {   
            weaponSpriteRenderer.flipY = shouldFlip;
        }
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
        jointSpriteRenderer.enabled = false;
        weaponSpriteRenderer.enabled = true;

        isAttacking = true;
        animator.SetBool(animationParam, isAttacking);
        //gameObject.SetActive(true);
        hitBoxCollider.enabled = true;
        SetPosition(dir, joint);
        StartCoroutine(DoAttack(duration, animationParam));       
    }
    //public void ActivateAttackRange(Vector2 dir, float duration, Transform joint)
    //{
    //    jointSprite.enabled = false;
    //    weaponSpriteRenderer.enabled = true;

    //    isRangeAttacking = true;
    //    animator.SetBool("isRangeAttacking", isRangeAttacking);
    //    //gameObject.SetActive(true);
    //    hitBoxCollider.enabled = true;
    //    SetPosition(dir, joint);
    //    StartCoroutine(DoAttack(duration));
    //}

    private IEnumerator DoAttack(float duration, string animationParam)
    {
        //Debug.Log("ATACANDO DESDE MELEE ATTACK");
        //hitBoxSprite.color = Color.red;
        yield return new WaitForSeconds(duration);
        //gameObject.SetActive(false);
        hitBoxCollider.enabled = false;
        weaponSpriteRenderer.enabled = false;
        jointSpriteRenderer.enabled = true;
        isRangeAttacking = false;
        isAttacking = false;
        animator.SetBool(animationParam, isAttacking);
        animator.SetBool(animationParam, isAttacking);

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
