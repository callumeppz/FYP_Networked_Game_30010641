using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJumping : MonoBehaviour
{
    private Rigidbody2D body; // get component
    public float fallMult = 2.5f; // faster falling multiplier
    public float lowJumpMult = 2.0f; // lower jump multiplier

    // Start is called before the first frame update
    void Start()
    {
      body = GetComponent<Rigidbody2D>(); // fetch component
    }

    // Update is called once per frame
    void Update()
    {
        // If the player is falling
        if (body.velocity.y < 0)
        {
            // apply a higher downward velocity to therefore faster falling
            body.velocity += Vector2.up * Physics2D.gravity.y * (fallMult - 1) * Time.deltaTime;
        }
        // if the player is jumping but the jump button is not pressed
        else if (body.velocity.y > 0 && !Input.GetButton("Jump"))
            {
            // Apply a lower upward velocity therefore lower jumps
            body.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMult - 1) * Time.deltaTime;
        }
    }
}
