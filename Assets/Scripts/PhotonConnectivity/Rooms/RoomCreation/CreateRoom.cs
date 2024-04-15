using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField roomNameInputField;

    public GameObject Error; // Reference to the error message object
    public GameObject joinError; // Reference to the join error message object
    public GameObject HostOnlyButton; // Reference to the host only button

    // start methd  to syncrhonise scenes between clients

    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }


    // Method invoked when the create room button is clicked
    public void OnClick_CreateRoom()
    {
        // Check if Photon Network is connected
        if (!PhotonNetwork.IsConnected)
        {
            Debug.LogError("PhotonNetwork is not connected!");
            return;
        }

        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            Debug.LogError("Room name is empty!");
            Error.SetActive(true);
            return;
        }

        // Create room options
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 6;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;
        roomOptions.PublishUserId = true;
        // Activate host only button
        HostOnlyButton.SetActive(true);
        // Create the room
        PhotonNetwork.CreateRoom(roomNameInputField.text, roomOptions, TypedLobby.Default);
    }
    // Method to leave the current room
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom(true);
    }
    // Method invoked when the join random room button is clicked
    public void OnJoinRandomButtonClicked()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    // Callback invoked when joining a random room fails
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message); // display message on why failed
        //joinError.SetActive(true);
        string newRoomName = "Room" + Random.Range(100, 10000); // new room name randomly generated
        RoomOptions roomsOptions = new RoomOptions();
        roomsOptions.MaxPlayers = 6; // setting max players
        PhotonNetwork.CreateRoom(newRoomName, roomsOptions, TypedLobby.Default); // creating room and saving to Photon
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Test: Room created successfully!"); // integration testing
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"Room creation failed: {message}"); // integration testing and error messages
        base.OnCreateRoomFailed(returnCode, message);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room successfully!");
    }

    //    public override void OnPlayerEnteredRoom(Player newPlayer)
    //    {
    //        base.OnPlayerEnteredRoom(newPlayer);
    //        if (PhotonNetwork.IsMasterClient)
    //        {
    //            if (PhotonNetwork.CurrentRoom.MaxPlayers == PhotonNetwork.CurrentRoom.PlayerCount)
    //            {
    //                PhotonNetwork.LoadLevel("Main Level (hub)");
    //            }
    //        }
    //    }
    //
}
 