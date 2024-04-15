using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviourPun
{

    /// <summary>
    /// This script took inspiration from the youtube video by MoreBBlakeyyy released in 2023. 
    /// https://www.youtube.com/watch?v=RuvfOl8HhhM&t=247s&ab_channel=MoreBBlakeyyy
    /// </summary>

    // variable initialising
    public float speed;
    [SerializeField] private float damage;

    // points in which enemies move
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D body;
    private Animator anim;
    private Transform currentPoint;

    [SerializeField] private float idle;
    private float idleTimer;

    void Start()
    {
        // Getting componenets such as rigidbody and animator
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        // setting initial movement point to point B
        currentPoint = pointB.transform;
        
    }

    void Update()
    {
        // calculating direction of movement based on current point
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform) 
        {
            // Moving towards point B
            body.velocity = new Vector2(speed, 0);
            idleTimer = 0;
        }
        else
        {
            // Moving towards point A
            body.velocity = new Vector2(-speed, 0);
            idleTimer = 0;
        }
        // Statements to check whether enemy has reached specific point, if so, flip
        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
                currentPoint = pointA.transform;
                flip();
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {

                currentPoint = pointB.transform;
                flip();
        }
    }
    // method to draw Gizmos for better visualisation in the Unity editor
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointA.transform.position);
    }

    // Method to flip sprite
    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}
