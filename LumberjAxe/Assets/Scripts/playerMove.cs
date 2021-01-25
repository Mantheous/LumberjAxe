using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    public float speed = 10f;
    public float jumpheight = 10f;

    [SerializeField] private LayerMask platformLayerMask;

    public Rigidbody2D player;
    public CapsuleCollider2D playerCollider;
    private Vector2 moveVelocity;
    

    void Update()
    {
        player = gameObject.GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();


    }

    private void FixedUpdate()
    {
        //player.MovePosition(player.position + moveVelocity * Time.fixedDeltaTime);

        if (IsGrounded() && Input.GetButtonDown("Jump"))
        {
            player.AddForce(transform.up * jumpheight);
            Debug.Log("jump");
        }

        Debug.Log(IsGrounded());

        if (Input.GetButton("Horizontal"))
        {
            transform.localPosition += Input.GetAxis("Horizontal") * transform.right * speed * Time.fixedDeltaTime;
        }
    }


    private bool IsGrounded()
    {
        //creates ray and stores it in the hit var
        RaycastHit2D hit = Physics2D.Raycast(playerCollider.bounds.center, Vector2.down, playerCollider.bounds.extents.y + 0.1f, platformLayerMask);
        //sets color for draw ray
        Color rayColor;
        if(hit.collider != null)
        {
            rayColor = Color.green;
        }else
        {
            rayColor = Color.red;
        }
        //draws ray in editor
        Debug.DrawRay(playerCollider.bounds.center, Vector2.down * (playerCollider.bounds.extents.y + 0.1f), rayColor, 1);
        Debug.Log(hit.collider);

        //returns value
        return hit.collider != null;
    }
}