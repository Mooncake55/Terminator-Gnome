using System;
using System.Collections;
using UnityEngine;


public class MeleeAttack : MonoBehaviour
{
    private float damage;
    //private SpriteRenderer jointSprite;
    private BoxCollider2D hitBoxCollider;

    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer weaponSprite;
    private string animationParam;
    private bool isAttacking;
    private bool isRangeAttacking;
    //[SerializeField] private SpriteRenderer weaponSpriteRenderer;
    //[SerializeField] private SpriteRenderer jointSpriteRenderer;

    private void Start()
    {
        //gameObject.SetActive(false);
        hitBoxCollider = GetComponent<BoxCollider2D>();
        hitBoxCollider.enabled = false;
        //animator = GetComponentInParent<Animator>();
        //weaponSpriteRenderer.enabled = true;
    }
    public void SetDamage(float dmg)
    {
        damage = dmg;
    }
    public void SetAnimationParam(string param)
    {
        animationParam = param;
    }

    //Uses the joint to pivot and position the hitbox in front
    void SetPosition(Vector2 dir, Transform joint)
    {
        ////float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        ////joint.transform.rotation = Quaternion.Euler(0, 0, angle);
        ////float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //angle = Mathf.Repeat(angle, 360f);
        //if (angle > 90f && angle < 270f) { 
        //    weaponSprite.transform.localScale = new Vector3(-1, 1, 0);
        //    //weaponSprite.transform.rotation = Quaternion.Euler(180, 0, 0);
        //}
        //else {
        //    weaponSprite.transform.localScale = new Vector3(1, 1, 1);           
        //}
        //joint.transform.rotation = Quaternion.Euler(0, 0, angle);
        // 1. Obtener ángulo entre -180° y 180°
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // 2. Normalizar el ángulo al rango 0°–360°
        angle = (angle + 360f) % 360f;

        // 3. Rotar el joint siempre hacia esa dirección
        joint.rotation = Quaternion.Euler(0, 0, angle);

        // 4. Flip visual si apunta hacia la izquierda (90° a 270°)
        if (angle > 90f && angle < 270f)
        {
            //weaponSprite.transform.localScale = new Vector3(-1, 1, 1);
            weaponSprite.transform.rotation = Quaternion.Euler(0, 0, 180);
            weaponSprite.transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            //weaponSprite.transform.localScale = new Vector3(1, 1, 1);
            weaponSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
            weaponSprite.transform.localScale = new Vector3(1, 1, 1);
        }

    }
    public void ActivateAttack(Vector2 dir, float duration, Transform joint, string animationParam)
    {
        //if (dir.x != 0) FALTA
        //{
        //    weaponSprite.flipX = dir.x > 0;
        //}
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
        Debug.Log("ATACANDO DESDE MELEE ATTACK");
        //hitBoxSprite.color = Color.red;
        yield return new WaitForSeconds(duration);
        //gameObject.SetActive(false);
        hitBoxCollider.enabled = false;
        //weaponSpriteRenderer.enabled = false;
        //jointSpriteRenderer.enabled = true;
        isAttacking = false;
        animator.SetBool(animationParam, isAttacking);
    }
    void OnDrawGizmosSelected()
    {
        if (!isAttacking) return;

        Gizmos.color = new Color(1f, 0f, 0f, 0.4f);
        //Vector3 center = transform.position + (Vector3)hitboxOffset;
        Vector3 center = transform.position;
        Gizmos.DrawCube(center, hitBoxCollider.size);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, hitBoxCollider.size);
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
