using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class TestStartScreen : MonoBehaviourPunCallbacks
{
    [SerializeField] private RoomListingMenu roomListingMenu; // Reference to the RoomListingMenu script

    public void Start()
    {
        // Connect to Photon Network if previous connection failed
        if (!PhotonNetwork.IsConnected)
            PhotonNetwork.ConnectUsingSettings();
    }
    // Callback function invoked when attempting to join lobby
   //public void OnJoinLobby()
   // {
 
   //     if (PhotonNetwork.IsConnected)
   //     {
   //         PhotonNetwork.JoinLobby();
   //     }
   // }

    public override void OnJoinedLobby()
    {
        roomListingMenu.gameObject.SetActive(true);
    }
    // Callback function invoked when room list is updated
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // Call OnRoomListUpdate function in RoomListingMenu script
        roomListingMenu.OnRoomListUpdate(roomList);
    }
}
