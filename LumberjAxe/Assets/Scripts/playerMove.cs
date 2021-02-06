using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMove : MonoBehaviour
{
    public float speed = 10f;
    public float jumpheight = 10f;
    public GameObject axe;

    [SerializeField] private LayerMask platformLayerMask;

    public Rigidbody2D rb;
    public CapsuleCollider2D playerCollider;
    private Vector2 moveVelocity;
    
    //throw stuff
    public Vector2 axePos;
    public Vector2 playerPos;
    public Rigidbody2D axeForce;
    public float upThrow = 10f;
    public float forwardThrow = 10f;
    
    float timeSinceButtonPressed = 0;
    float timeSinceGrounded = 0;
    public float buttonCusion = 0.2f;
    public float XDampening;
    public Animator animator;

    private void Start()
    {
        playerCollider = GetComponent<CapsuleCollider2D>();
        //rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        timeSinceButtonPressed += Time.deltaTime;
        timeSinceGrounded += Time.deltaTime;
        axePos = axe.transform.position;
        playerPos = rb.transform.position;

        if (Input.GetButtonDown("Jump") || Input.GetKey(KeyCode.UpArrow))
        {
            timeSinceButtonPressed = 0;
            //animator.setBool("Throw", true);
        }

        if(IsGrounded())
        {
            timeSinceGrounded = 0;
        }

        if (timeSinceGrounded < buttonCusion && timeSinceButtonPressed < buttonCusion)
        {
            Debug.Log(timeSinceButtonPressed);
            //rb.AddForce(transform.up * jumpheight);
            rb.velocity = new Vector2(rb.velocity.x, jumpheight);
            timeSinceGrounded = buttonCusion;
            timeSinceButtonPressed = buttonCusion + 0.5f;
        }

        //Axe throwing

        if (Input.GetButtonDown("Fire1"))
        {
            axe.transform.position = playerPos;
            axeForce.AddForce(transform.up * upThrow);
            axeForce.AddForce(transform.right * forwardThrow);
        }
    }

    private void FixedUpdate()
    {
        float Xvel = rb.velocity.x;
        Xvel += Input.GetAxisRaw("Horizontal");
        Xvel *= Mathf.Pow(1f - XDampening, Time.deltaTime * speed);
        rb.velocity = new Vector2(Xvel, rb.velocity.y);


        //movementToAdd = movementToAdd * Input.GetAxis("Horizontal") * speed;
        //rb.velocity += movementToAdd;
        //rb.velocity = (Input.GetAxis("Horizontal") * transform.right * speed * Time.fixedDeltaTime) + rb.velocity;
        
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
        //Debug.DrawRay(playerCollider.bounds.center, Vector2.down * (playerCollider.bounds.extents.y + 0.1f), rayColor, 1);
        //Debug.Log(hit.collider);

        //returns value
        return hit.collider != null;
    }

    

}
