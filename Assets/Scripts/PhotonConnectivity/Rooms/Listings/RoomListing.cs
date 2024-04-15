using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class RoomListing : MonoBehaviour
{

    [SerializeField]
    private Text text; // Reference to the text component

    public RoomInfo ROOMiNFO { get; private set; } // Player object associated with this listing
    private GameObject currentRoom;
    private CreateRoom createAndJoin;
    private PlayerMenu playerListing;


    private void Start()
    {
        currentRoom = GameObject.Find("CurrentLobby");
    }



    // method to set room info
    public void SetRoomInfo(RoomInfo roominfo)

    {
        ROOMiNFO = roominfo; // setting room object
        text.text = roominfo.PlayerCount + "/" + roominfo.MaxPlayers + ", " + roominfo.Name; // displaying room name and max player to screen
    }

    public void OnClick_Button()
    {
        PhotonNetwork.JoinRoom(ROOMiNFO.Name);
        if (currentRoom != null)
        {
            currentRoom.SetActive(true);
            createAndJoin.gameObject.SetActive(false);
        }
    }



}
