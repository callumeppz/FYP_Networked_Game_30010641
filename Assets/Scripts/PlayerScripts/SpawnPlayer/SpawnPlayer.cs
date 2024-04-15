using UnityEngine;
using Photon.Pun;

public class PlayerSpawnController : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;  // Reference to the player prefab to be spawned

    private void Start()
    {
        // when game starts spawn method called
        SpawnPlayer();
    }

    public void SpawnPlayer() 
    {
        // Instantiate the player prefab
        PhotonNetwork.Instantiate(playerPrefab.name, transform.position, Quaternion.identity);
    }
}
