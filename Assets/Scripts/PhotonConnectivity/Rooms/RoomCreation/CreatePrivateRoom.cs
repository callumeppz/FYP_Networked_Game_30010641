using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class CreatePrivateRoom : MonoBehaviourPunCallbacks
{
    // input field
    [SerializeField] private InputField roomNameInputField;
    [SerializeField] private InputField roomNameInputField2;
    public GameObject Error; // Reference to the error message object
    public GameObject joinError; // Reference to the join error message object
    public GameObject HostOnlyButton; // Reference to the host only button
    public GameObject CurrentRoom;
    public GameObject myGameObject;


    public void OnClick_CreatePrivateRoom()
    {
        // Check if Photon Network is connected
        if (!PhotonNetwork.IsConnected)
        {
            Debug.LogError("Error: PhotonNetwork is not connected!"); // error messages

            return;
        }

        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            Debug.LogError("Error: Room name is empty!");
            Error.SetActive(true);
            return;
        }
        if (PhotonNetwork.IsConnected)
        {
            CurrentRoom.SetActive(true);
        }

        // Create room options
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 6;
        roomOptions.IsVisible = false;
        roomOptions.IsOpen = true;
        roomOptions.PublishUserId = true;
        // Activate host only button
        HostOnlyButton.SetActive(true);
        // Create the room
        PhotonNetwork.CreateRoom(roomNameInputField.text, roomOptions, TypedLobby.Default);
    }

    public void OnClick_JoinPrivateRoom()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.LogError("PhotonNetwork is not connected!");
            HostOnlyButton.SetActive(false);
            return;
        }

        if (string.IsNullOrEmpty(roomNameInputField2.text))
        {
            Debug.LogError("Error: Room name is empty!"); // error message displayed if room name null
            Error.SetActive(true); // set error popup to active
            return;
        }
        if (PhotonNetwork.IsConnected)
        {
            CurrentRoom.SetActive(true);
        }
        
        PhotonNetwork.JoinRoom(roomNameInputField2.text);

    }
}