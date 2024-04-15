using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEditor;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject cameraPrefab;
    public Text pingText;
    public GameObject DisconnectUI;
    public GameObject PlayerFeed;
    public GameObject FeedGrid;
        
    private bool Off = false;

    public float minX;
    public float minY;
    public float maxX;
    public float maxY;

    public float cameraOffsetX; 
    public float cameraOffsetY; 
    public float cameraOffsetZ; 

    private void Start()
    {
        Vector2 randomPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        Debug.Log("Player spawned");

        GameObject newPlayer = PhotonNetwork.Instantiate(playerPrefab.name, randomPos, Quaternion.identity);
        Debug.Log("Player instantiated: " + (newPlayer != null));

   
        Vector3 cameraPos = new Vector3(randomPos.x + cameraOffsetX, randomPos.y + cameraOffsetY, cameraOffsetZ);

        GameObject newCamera = Instantiate(cameraPrefab, cameraPos, Quaternion.identity);
        Debug.Log("Camera instantiated: " + (newCamera != null));

        if (newPlayer != null && newCamera != null)
        {
            CameraFollow cameraFollow = newCamera.GetComponent<CameraFollow>();
            if (cameraFollow != null)
            {
                cameraFollow.target = newPlayer.transform;
            }
            else
            {
                Debug.LogError("CameraFollow component not found on camera prefab!");
            }
        }
        else
        {
            CheckInput();
            Debug.LogError("Player or Camera is null!");
        }
    }

    private void Update()
    {
        pingText.text = "Ping" + PhotonNetwork.GetPing();
    }

    private void CheckInput()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Debug.Log("Escape key pressed.");
            if (Off)
            {
                Debug.Log("Turning off DisconnectUI.");
                DisconnectUI.SetActive(false);
                Off = false;
            }
            else
            {
                Debug.Log("Turning on DisconnectUI.");
                DisconnectUI.SetActive(true);
                Off = true;
            }
        }
    }


    public void leaveRoom()
    {
        PhotonNetwork.LeaveRoom(this);
        PhotonNetwork.LoadLevel("MainMenu");
    }

    private void OnPhotonPlayerConnected(Player player) {
        GameObject obj = Instantiate(PlayerFeed, new Vector2(0, 0), Quaternion.identity);
        obj.transform.SetParent(FeedGrid.transform, false);
        obj.GetComponent<Text>().text = player.NickName + "Joined the Game";
        obj.GetComponent<Text>().color = Color.green;
        }

    private void OnPhotonPlayerDisconnected(Player player)
    {
        GameObject obj = Instantiate(PlayerFeed, new Vector2(0, 0), Quaternion.identity);
        obj.transform.SetParent(FeedGrid.transform, false);
        obj.GetComponent<Text>().text = player.NickName + "Left the Game";
        obj.GetComponent<Text>().color = Color.red;
    }
}
