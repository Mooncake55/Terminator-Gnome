using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{ 
    private float speed;
    private Rigidbody2D rb;
    private Player player;

    [Header("Animation")]
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        animator = GetComponent<Animator>();
    }

    public void MovePlayer(Vector2 direction)
    {
        animator.SetBool("isMoving", direction != Vector2.zero);
        if (direction != Vector2.zero)
        {
            animator.SetFloat("moveX", direction.x);
            animator.SetFloat("moveY", direction.y);
        }
        speed = player.GetPlayerData().moveSpeed;
        rb.linearVelocity = direction * speed;
    }
    public void SetPlayer(Player p)
    {
        player = p;
    }
}

