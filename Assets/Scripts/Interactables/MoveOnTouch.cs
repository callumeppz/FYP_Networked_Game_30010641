using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnTouch : MonoBehaviourPun
{
    /// <summary>
    /// This script was inspired by Jason Weimann with his YouTube tutorial released in 2017.
    /// https://www.youtube.com/watch?v=O6wlIqe2lTA&ab_channel=JasonWeimann
    /// </summary>
    /// <param name="col"></param>

    void OnCollisionEnter2D(Collision2D col) // when colliding
    {
        if (col.gameObject.CompareTag("Player")) // check if player
        {
            col.collider.transform.SetParent(transform); // set moving platforms as parent to ensure players sticks
        }
    }

    private void OnCollisionExit2D(Collision2D col) // when not colliding
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.collider.transform.SetParent(null); // once not colliding, remove player from child 
        }
    }

}
