using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCollectable : MonoBehaviourPun
{
    // initialise variables
    [SerializeField] private float healthValue;
    public static int healthNumber = 0;
    public PlayerHealth health;

    private void Start()
    {
        // get neccessary components
        health = GetComponent<PlayerHealth>();
    }

    /** Method to check collisions between active gameobejct and player, if so get player health value and increse by 1
     * also call the Invunrability co-routine. 
     */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().AddHealth(healthValue);
            gameObject.SetActive(false);
            healthNumber++; // increase by 1
            collision.GetComponent<PlayerHealth>().StartCoroutine("Invunrability"); // start co-routine
        }
    }
}
