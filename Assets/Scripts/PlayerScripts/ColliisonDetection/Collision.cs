using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [Header("Layers")] // header to better seperate
    public LayerMask groundLayer; // initialising ground layer

    [Space]

    public bool onGround; // checking whether on ground
    public bool onWall; // checking whether on wall
    public bool onRightWall; // checking whether on right wall
    public bool onLeftWall; // checking whether on left wall
    public int wallSide; // checking whether wall sliding

    [Space]

    [Header("Collision")]

    public float collisionRadius = 0.25f; // radius of collision
    public Vector2 bottomOffset, rightOffset, leftOffset; // offset positions for collision deteciton
    private Color debugCollisionColor = Color.blue; // colour used for visualisation

    // Update is called once per frame
    void Update()
    {
        // check if collision with ground
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
        // check if collision with wall
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        // check if collision with right wall
        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
        // check if collision with left wall
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);
        // Determine which side of the wall
        wallSide = onRightWall ? -1 : 1;
    }


    // Visualise the collision detection in the Unity Editor
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };
        // Draw wire spheres to represent collision points
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }
}
