using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    //[SerializeField] private float speed = 5f;
    [SerializeField] private float dashSpeed = 2f;//also dashDistance
    [SerializeField] private float dashDuration = 0.2f; //dash normal
    public LayerMask obstacleLayer;
    private Rigidbody2D rb;
    private Player player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    public void Dash(Vector2 direction, bool changeDirection)
    {
        dashSpeed = player.GetPlayerData().dashSpeed;
        dashDuration = player.GetPlayerData().dashDuration;
        if (changeDirection) { direction = -direction; }
        Vector2 dashTarget = rb.position + direction * dashSpeed;
        Debug.Log($"DashTarget: " + dashTarget);

        rb.MovePosition(dashTarget);
        dashTarget = Vector2.zero;
        StartCoroutine(WaitForSeconds(dashDuration));
        Debug.Log($"DashTarget Final: " + dashTarget);
        Debug.Log($"pos: " + rb.position);
    }
    private IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
    public void SetPlayer(Player p)
    {
        player = p;
    }
}
