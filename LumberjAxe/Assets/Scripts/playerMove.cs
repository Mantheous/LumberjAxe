using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    public float speed = 10f;
    public float jumpheight = 10f;

    [SerializeField] private LayerMask platformLayerMask;

    private Rigidbody2D player;
    private CapsuleCollider2D playerCollider;
    private Vector2 moveVelocity;

    void Update()
    {
        player = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();

        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), 0);
        moveVelocity = moveInput.normalized * speed;
    }

    private void FixedUpdate()
    {
        player.MovePosition(player.position + moveVelocity * Time.fixedDeltaTime);

        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            player.AddForce(transform.up * jumpheight);
        }
    }
    private bool IsGrounded()
    {
        Physics2D.Raycast(playerCollider.bounds.center, Vector2.down, playerCollider.bounds.extents.y, + 1);
        return true;
    }
}