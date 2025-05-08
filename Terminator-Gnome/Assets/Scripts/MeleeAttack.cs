using System;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class MeleeAttack : MonoBehaviour
{
    private SpriteRenderer meleeSprite;
    private BoxCollider2D meleeBoxCollider;

    float posX;//quitable
    float posY;//quitable

    private void Start()
    {
        gameObject.SetActive(false);
    }
    void Awake()
    {
        meleeSprite = GetComponent<SpriteRenderer>();
        meleeBoxCollider = GetComponent<BoxCollider2D>();
        posX = transform.localPosition.x; //quitable
        posY = transform.localPosition.y;//quitable

    }
    //void SetPosition(Vector2 Dir)
    //{
    //    if (Dir == Vector2.up)
    //    {
    //        posY = Mathf.Abs(posY);
    //    }
    //    else if (Dir == Vector2.down)
    //    {
    //        posY = -Mathf.Abs(posY);
    //    }
    //    else if (Dir == Vector2.left)
    //    {
    //        posX = -Mathf.Abs(posX);
    //    }
    //    else if (Dir == Vector2.right) { posX = Mathf.Abs(posX); }

    //    transform.localPosition = new Vector3(posX, posY, 0);
    //}

    void SetPosition(Vector2 dir, Transform joint)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        joint.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    public void ActivateAttack(Vector2 dir, float duration, Transform joint)
    {
        gameObject.SetActive(true);
        SetPosition(dir, joint);
        StartCoroutine(DoAttack(duration)); 
    }

    private System.Collections.IEnumerator DoAttack(float duration)
    {
        meleeSprite.color = Color.red;
        yield return new WaitForSeconds(duration);
        //meleeSprite.color = Color.white;
        gameObject.SetActive(false);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable enemy = other.GetComponent<IDamageable>();
        if (enemy != null)
        {
            enemy.HandleDamage(10);
        }
    }

}
