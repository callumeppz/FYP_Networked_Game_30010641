using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListing : MonoBehaviour
{
   
    [SerializeField]
    private Text text; // Reference to the text component

    public Player Player { get; private set; }  // Player object associated with this listing

    // method that sets player info
    public void SetPlayerInfo(Player player)
    {
        Player = player; // set player object
        text.text = player.NickName; // display players nickname in UI
    }

}
