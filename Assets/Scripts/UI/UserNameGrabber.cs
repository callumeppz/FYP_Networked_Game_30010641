using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class UserNameGrabber : MonoBehaviourPunCallbacks
{
    // variable initialised
    public Text PlayerNameText;
    public Text ServerNameText;

    private void Start()
    {
        if (PhotonNetwork.IsConnected) // ensuring connceton to server
        {
            PlayerNameText.text = PhotonNetwork.NickName; // setting text to nickname
        }
    }

    public override void OnJoinedRoom() // method for displaying when a player joined a room
    {
        base.OnJoinedRoom();
        if (PhotonNetwork.CurrentRoom != null && !string.IsNullOrEmpty(PhotonNetwork.CurrentRoom.Name))
        {
            ServerNameText.text = PhotonNetwork.CurrentRoom.Name;
            Debug.Log("Joined room: " + PhotonNetwork.CurrentRoom.Name);
        }
    }
}
