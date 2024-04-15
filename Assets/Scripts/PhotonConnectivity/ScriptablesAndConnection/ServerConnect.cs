using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class ServerConnet : MonoBehaviourPunCallbacks // Pun callback for sucessful connection to PUN
{
    void Start()
    {
        print("Connecting. . .");
        PhotonNetwork.NickName = MasterManager.GameSettings.NickName; // set nickname to htat specified in gamesettings script
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion; // Set gameversion to that specified within gamesettings script
        PhotonNetwork.ConnectUsingSettings(); // Connect to Photon Network using the specified settings
    }

    // Callback function invoked when connected to Photon Master Server
    public override void OnConnectedToMaster()
    {
        print("Test: Connected!"); // Test output to console, assesing connection to photon server
        print(PhotonNetwork.LocalPlayer.NickName);

        //PhotonNetwork.JoinLobby(); // may be unused
    }

    // callback function invoked when joined to a lobby
    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("MainMenu"); // loads mainmenu scene

    }
    // callback function invoked when disconnected from the Photon server
    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Test Failed: Disconnected from server due to " + cause.ToString()); // prints reason for disconnection to dev console
    }
}
