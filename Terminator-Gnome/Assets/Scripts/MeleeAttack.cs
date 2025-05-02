using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    //private GameObject attackArea;
    private SpriteRenderer meleeSprite;
    private BoxCollider2D meleeBoxCollider;

    private Vector2 horizontalHit = new Vector2(0.16f, 0f);
    private Vector2 verticallHit = new Vector2(0f, 21f);
    private Vector2 lastHitArea;
    float duration = 2f;


    void Awake()
    {
        Vector2 lastHitArea = horizontalHit;
        meleeSprite = GetComponent<SpriteRenderer>();
        meleeBoxCollider = GetComponent<BoxCollider2D>();

    }
    //void SetPosition(Vector3 PlayerPosition, float xValue, float yValue, float zvalue)
    //{
    //    Vector3 playerPos = PlayerPosition;
    //    transform.position.Set(PlayerPosition.x + xValue, PlayerPosition.y + yValue, PlayerPosition.z + zvalue);
    //}
    void SetPosition(Vector2 PlayerDir, Transform playerTransform)
    {
        Vector2 playerPos = new Vector2(playerTransform.position.x, playerTransform.position.y);
        horizontalHit = playerPos + horizontalHit * PlayerDir;
        verticallHit = playerPos + verticallHit * PlayerDir;

        if (PlayerDir == Vector2.up || PlayerDir == Vector2.down)
        {
            transform.position = verticallHit;
            lastHitArea = verticallHit;
            transform.eulerAngles = new Vector3(0, 0, 90);

        }
        else if (PlayerDir == Vector2.right || PlayerDir == Vector2.left)
        {
            transform.position = horizontalHit;
            lastHitArea = horizontalHit;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else { transform.position = lastHitArea; }
           
        

        //if (PlayerDir == Vector2.up)
        //{
        //    //transform.position = VerticallHit;
        //    //transform.eulerAngles = new Vector3(0, 0, 90);
        //    //lastHitArea = VerticallHit;
        //}
        //else if (PlayerDir == Vector2.down)
        //{
        //    transform.position = -VerticallHit;
        //    transform.eulerAngles = new Vector3(0, 0, 90);
        //    lastHitArea = -VerticallHit;
        //}
        //else if (PlayerDir == Vector2.right)
        //{
        //    transform.position = horizontalHit;
        //    transform.eulerAngles = Vector3.zero;
        //    lastHitArea = -horizontalHit;    
        //}
        //else if (PlayerDir == Vector2.left)
        //{
        //    transform.position = horizontalHit;
        //    transform.eulerAngles = Vector3.zero;
        //    lastHitArea = -horizontalHit;
        //}
        //else { transform.position = lastHitArea; }

    }

    public void ActivateAttack(Vector2 playerDir, Transform playerTransform)
    {
        SetPosition(playerDir, playerTransform);
        StartCoroutine(DoAttack(duration));
    }

    private System.Collections.IEnumerator DoAttack(float duration)
    {

        //attackArea.SetActive(true);
        //yield return new WaitForSeconds(duration);
        //attackArea.SetActive(false);
        meleeSprite.color = Color.red;
        yield return new WaitForSeconds(duration);
        meleeSprite.color = Color.white;
    }

    //analizar despues
    //void CreateMeleeHitbox()
    //{
    //    attackArea = new GameObject("AttackArea");
    //    attackArea.transform.parent = transform; // que siga al jugador
    //    attackArea.transform.localPosition = new Vector2(1f, 0f); // adelante del jugador

    //    BoxCollider2D collider = attackArea.AddComponent<BoxCollider2D>();
    //    collider.isTrigger = true;
    //    collider.size = new Vector2(1f, 0.5f);

    //    attackArea.tag = "PlayerAttack";
    //    attackArea.SetActive(false); // para activar solo cuando atacás
    //}
}
