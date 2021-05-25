using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    [SerializeField] GameObject groundCheck = null;
    [SerializeField] float groundTolerance = 0.05f;
    [SerializeField] LayerMask groundLayerMask = 0;
    bool isGrounded = false;

    [SerializeField] float jumpPower = 5f;
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] bool canDoubleJump = false;
    [SerializeField] JumpType jumpType = JumpType.Instant;
    bool jumpRequest = false;
    bool hasDoubleJump = true;
    Vector3 eulerAngle = new Vector3();
    Quaternion rotation = new Quaternion();


    Rigidbody2D rb2d;
    float xAxis;
    BoxCollider2D myCollider;


    Vector2 groundCheckStart = new Vector2();
    Vector3 moveVector = new Vector3();

    public KeyCode JumpKey { get => jumpKey; set => jumpKey = value; }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        myCollider = GetComponent<BoxCollider2D>();
        groundCheckStart.Set(-myCollider.bounds.extents.x, -myCollider.bounds.extents.y - groundTolerance);
        groundCheck.transform.localPosition = groundCheckStart;
    }

    // Update is called once per frame
    void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(jumpKey))
            jumpRequest = true;

        UpdateLookingDir();
        GroundCheck();
    }

    private void FixedUpdate()
    {
        moveVector.Set(xAxis * moveSpeed, rb2d.velocity.y, 0f);
        rb2d.velocity = moveVector;

        if(jumpRequest)
        {
            if (isGrounded)
            {
                if(jumpType == JumpType.Instant)
                    rb2d.velocity = new Vector3(rb2d.velocity.x, jumpPower, 0f);
                else
                    rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }
            else if(hasDoubleJump && canDoubleJump)
            {
                hasDoubleJump = false;
                if (jumpType == JumpType.Instant)
                    rb2d.velocity = new Vector3(rb2d.velocity.x, jumpPower, 0f);
                else
                    rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }
            jumpRequest = false;
        }
    }

    void UpdateLookingDir()
    {
        if (xAxis == -1)
            eulerAngle = 180 * Vector3.up;
        else if (xAxis == 1)
            eulerAngle = Vector3.zero;

        rotation.eulerAngles = eulerAngle;
        transform.rotation = rotation;
    }

    void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.transform.position, transform.right, myCollider.bounds.extents.x * 2, groundLayerMask);
        if (hit.collider != null)
        {
            isGrounded = true;
            hasDoubleJump = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    enum JumpType
    {
        Instant,
        AddForce
    }
}
