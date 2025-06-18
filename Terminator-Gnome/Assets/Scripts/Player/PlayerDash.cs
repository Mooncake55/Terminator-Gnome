using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private float dashSpeed;//also dashDistance
    private float dashDuration; 
    private Rigidbody2D rb;
    private Player player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }    

    //changeDirection is for range attack (not implemented, but it moves the player backward)
    //is implemented as a POINT A to POINT B "teleportation"
    public void Dash(Vector2 direction, bool changeDirection)
    {     
        if (changeDirection) { direction = -direction; }
        Vector2 dashTarget = rb.position + direction.normalized * dashSpeed;
        //RaycastHit2D hit = Physics2D.Raycast(rb.position, direction, dashSpeed, LayerMask.GetMask("Wall"));
        RaycastHit2D hit = Physics2D.CircleCast(rb.position, 0.3f, direction, dashSpeed, LayerMask.GetMask("Wall"));
        if (hit.collider != null)
        {
            dashTarget = hit.point - direction.normalized * 0.1f;
        }
        Debug.DrawLine(rb.position, dashTarget, Color.red, 1f);
        rb.MovePosition(dashTarget);
        dashTarget = Vector2.zero;
        StartCoroutine(WaitForSeconds(dashDuration));
    }
    private IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
    public void SetPlayer(Player p)
    {
        player = p;
        dashSpeed = player.GetPlayerData().dashSpeed;
        dashDuration = player.GetPlayerData().dashDuration;
        Debug.Log($"dashSpeed: " + dashSpeed + " dashDuration: " + dashDuration);
    }
}
