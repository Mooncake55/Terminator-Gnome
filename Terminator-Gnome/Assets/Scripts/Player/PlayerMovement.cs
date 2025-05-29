using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //[SerializeField] private float speed = 5f; 
    private float speed;
    private Rigidbody2D rb;
    private Player player;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    public void MovePlayer(Vector2 direction)
    {
        speed = player.GetPlayerData().moveSpeed;
        rb.linearVelocity = direction * speed;
    }
    public void SetPlayer(Player p)
    {
        player = p;
    }
}

