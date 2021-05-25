using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{
    [SerializeField] float fallMultiplier = 2.5f;
    [SerializeField] float lowJumpMultiplier = 2f;

    KeyCode jumpKey;
    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        jumpKey = GetComponent<PlayerMovement>().JumpKey;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb2d.velocity.y < 0)
        {
            rb2d.gravityScale = fallMultiplier;
        }
        else if(rb2d.velocity.y > 0 && !Input.GetKey(jumpKey))
        {
            rb2d.gravityScale = lowJumpMultiplier;
        }
        else
        {
            rb2d.gravityScale = 1f;
        }
    }
}
