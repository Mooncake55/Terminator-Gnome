using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class MeleeAttack : MonoBehaviour
{
    //private GameObject attackArea;
    private SpriteRenderer meleeSprite;
    private BoxCollider2D meleeBoxCollider;

    //private Vector2 horizontalHit = new Vector2(0.16f, 0f);
    //private Vector2 verticallHit = new Vector2(0f, 21f);
    //private Vector2 lastHitArea;
    //float duration = 2f;

    float posX;
    float posY;

    private void Start()
    {
        gameObject.SetActive(false);
    }
    void Awake()
    {
        meleeSprite = GetComponent<SpriteRenderer>();
        meleeBoxCollider = GetComponent<BoxCollider2D>();
        posX = transform.localPosition.x;
        posY = transform.localPosition.y;

    }
    void SetPosition(Vector2 playerDir)
    {
        if (playerDir == Vector2.up)
        {
            posY = Mathf.Abs(posY);
        }
        else if (playerDir == Vector2.down)
        {
            posY = -Mathf.Abs(posY);
        }
        else if (playerDir == Vector2.left)
        {
            posX = -Mathf.Abs(posX);
        }
        else if (playerDir == Vector2.right) { posX = Mathf.Abs(posX); }

        transform.localPosition = new Vector3(posX, posY, 0);
    }
    //void SetPosition(Vector2 PlayerDir, Transform playerTransform)
    //{
    //    //Vector2 playerPos = new Vector2(playerTransform.position.x, playerTransform.position.y);
    //    //Vector2 horizontal = playerPos + horizontalHit * PlayerDir;
    //    //Vector2 verticall = playerPos + verticallHit * PlayerDir;

    //    //if (PlayerDir == Vector2.up || PlayerDir == Vector2.down)
    //    //{
    //    //    transform.position = verticall;
    //    //    lastHitArea = verticall;
    //    //    transform.eulerAngles = new Vector3(0, 0, 90);

    //    //}
    //    //else if (PlayerDir == Vector2.right || PlayerDir == Vector2.left)
    //    //{
    //    //    transform.position = horizontal;
    //    //    lastHitArea = horizontal;
    //    //    transform.eulerAngles = new Vector3(0, 0, 0);
    //    //}
    //    //else { transform.position = lastHitArea; }



    //    //if (PlayerDir == Vector2.up)
    //    //{
            
    //    //}
    //    //else if (PlayerDir == Vector2.down)
    //    //{
    //    //    transform.position = -VerticallHit;
    //    //    transform.eulerAngles = new Vector3(0, 0, 90);
    //    //    lastHitArea = -VerticallHit;
    //    //}
    //    //else if (PlayerDir == Vector2.right)
    //    //{
    //    //    transform.position = horizontalHit;
    //    //    transform.eulerAngles = Vector3.zero;
    //    //    lastHitArea = -horizontalHit;
    //    //}
    //    //else if (PlayerDir == Vector2.left)
    //    //{
    //    //    transform.position = horizontalHit;
    //    //    transform.eulerAngles = Vector3.zero;
    //    //    lastHitArea = -horizontalHit;
    //    //}
    //    //else { transform.position = lastHitArea; }

    //}

    public void ActivateAttack(Vector2 playerDir, float duration)
    {
        gameObject.SetActive(true);
        SetPosition(playerDir);
        StartCoroutine(DoAttack(duration));
        
    }

    private System.Collections.IEnumerator DoAttack(float duration)
    {
        Debug.Log(transform.localPosition);
        meleeSprite.color = Color.red;
        yield return new WaitForSeconds(duration);
        //meleeSprite.color = Color.white;
        gameObject.SetActive(false);
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
