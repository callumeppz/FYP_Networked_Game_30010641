using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Gamemanager : MonoBehaviourPunCallbacks
{
    // Inspector references for each of these objects
    public GameObject PlayerPrefab;
    public GameObject GameCanvas;
    public GameObject DisconnectUI;
    public GameObject PlayerFeed;
    public GameObject FeedGrid;
    public Text PingText;
    private bool off = false;

    private void Awake()
    {
        GameCanvas.SetActive(true); // ensure game canvas is active on game start
        PhotonNetwork.AutomaticallySyncScene = true; // syncs scenes
    }

    // method to check whether escape pressed, pulls up pause screen
    private void CheckInput()
    {
        if (off && Input.GetKeyDown(KeyCode.Escape))
        {
            DisconnectUI.SetActive(false);
            off = false;
        }
        else if (!off && Input.GetKeyDown(KeyCode.Escape))
        {
            DisconnectUI.SetActive(true);
            off = true;
        }
    }

    // method to illustrate ping
    private void Update()
    {
        CheckInput();
        PingText.text = "Ping: " + PhotonNetwork.GetPing();
    }

    // leave room method to go back to menu
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("MainMenu");
    }

    public override void OnPlayerEnteredRoom(Player player)
    {
        // Instantiate a player feed item when a new player enters the room
        GameObject obj = Instantiate(PlayerFeed, new Vector2(0, 0), Quaternion.identity);
        obj.transform.SetParent(FeedGrid.transform, false);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        // Instantiate a player feed item when a player leaves the room
        GameObject obj = Instantiate(PlayerFeed, new Vector2(0, 0), Quaternion.identity);
        obj.transform.SetParent(FeedGrid.transform, false);
    }
}
