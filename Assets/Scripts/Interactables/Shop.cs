using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    // initialise variables
    private PlayerMovement2D playerMovement;
    private KeyScript key;
    public GameObject KeyError;
    
    // Start is called before the first frame update
    public void OnClickEvent()
    {
        if (KeyScript.keyNumber >= 6) //method to allow for players to buy powerups
        {
            playerMovement.maxWallJumps = 5; // increasing player stats on click and if key is over or 6
            playerMovement.jumpingPower = 20;
        }
        else
        {
            Debug.Log("invalid amount of keys"); // else display key error and debug test log
            KeyError.SetActive(true);
        }
    }
}
