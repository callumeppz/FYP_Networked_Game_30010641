using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class HubSpawn : MonoBehaviour
{
    public GameObject playerPrefab; // Reference to the player prefab to be spawned

    public float minX; // max X, min Y for spawning
    public float minY;
    public float maxX; // max Y, min X for spawning
    public float maxY;

    private void Start()
    {
        // random position within the specified range
        Vector2 randomPos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        Debug.Log("Player spawned");
        // spawn the player prefab at the random position
        GameObject newPlayer = PhotonNetwork.Instantiate(playerPrefab.name, randomPos, Quaternion.identity);
        Debug.Log("Player instantiated: " + (newPlayer != null));
    }
}
