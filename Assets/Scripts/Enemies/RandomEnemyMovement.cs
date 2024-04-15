using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemyMovement : MonoBehaviour
    
{
    /// <summary>
    ///  This script is now unused within this application due to a more diverse enemy movement being created
    /// </summary>

    // initialise variables
    public Rigidbody2D rb;
    public float speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        // getting components
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // move enemy randomly 
        Vector2 Movement = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
        rb.AddForce(Movement);
        print(Movement);
    }
}
