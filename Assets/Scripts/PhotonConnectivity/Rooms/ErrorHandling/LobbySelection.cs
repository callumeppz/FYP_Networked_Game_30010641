using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class LobbySelection : MonoBehaviourPunCallbacks
{
    [SerializeField] private RoomListingMenu roomListingMenu;

    // Connect to Photon Network if previous connection failed
    public void Start()
    {
        if (!PhotonNetwork.IsConnected)
            PhotonNetwork.ConnectUsingSettings();
    }

    //public void OnJoinLobby()
    //{
 
    //    if (PhotonNetwork.IsConnected)
    //    {
    //        PhotonNetwork.JoinLobby();
    //    }
    //}

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
