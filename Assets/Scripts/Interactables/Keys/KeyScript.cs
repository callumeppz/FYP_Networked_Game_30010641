using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyScript : MonoBehaviourPun
{
    // initialise variables 
    [SerializeField] private float healthValue;
    public static int keyNumber = 0; // public key variable due to being accessible by other scripts
    public Text KeyNumberText;

    /** Method to check whether player collides with key object 
     */

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // gets collision component
            collision.GetComponent<PlayerHealth>().AddHealth(healthValue);
            gameObject.SetActive(false);
            // increases key number and sets object to false (collected)
            keyNumber++;
            KeyNumberText.text = "Key Count: " + keyNumber.ToString();
        }
    }
}
