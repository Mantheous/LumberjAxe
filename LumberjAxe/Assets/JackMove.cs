using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JackMove : MonoBehaviour
{
    LumberjAxe controls;
    public Rigidbody2D rb;
    public float jumpForce;
    public bool grounded;
    Vector2 movement;

    private void Awake()
    {
        controls = new LumberjAxe();
        controls.Player.Jump.performed += ctx => Jump();
        controls.Player.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => movement = Vector2.zero;
    }

    private void FixedUpdate()
    {

        rb.velocity = rb.velocity + new Vector2(movement.x, 0);
    }

    //Called from input
    public void Jump()
    {
        //Sends message to consol
        Debug.Log("Jump");
        if(grounded)
        {
            //Adds Vector2 force to the players RidgidBody component
            rb.AddForce(Vector2.up * jumpForce);
        }
    }

    //Turns on Input
    private void OnEnable()
    {
        controls.Player.Enable();
    }

    //Turns of input
    private void OnDisable()
    {
        controls.Player.Disable();
    }

    //Receaves bool from ground check collider
    public void GroundSet(bool _grounded)
    {
        grounded = _grounded;
    }
}
