using Photon.Pun;
using UnityEngine;

public class LobbyVerifier : MonoBehaviour
{
    public GameObject roomError; // Reference to the room error message object
    public GameObject currentRoom; // Reference to the current room object
    public GameObject ModeSelect; // Reference to the mode select object

    // Method to be invoked when checking the room
    public void OnClick_RoomChecker()
    {
        CheckRoom(); // Call the method to check the room
    }

    private void CheckRoom()
    {
        if (PhotonNetwork.InRoom)
        {
            currentRoom.SetActive(true);// Activate the current room object
            ModeSelect.SetActive(false);// Deactivate the modeselect room object
        }
        else
        {
            roomError.SetActive(true); // displau room error if not in room
        }
    }
}
