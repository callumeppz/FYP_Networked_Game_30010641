using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform content;
    [SerializeField] private PlayerListing _playerListingPrefab; 
    private List<PlayerListing> _listings = new List<PlayerListing>();

    public override void OnJoinedRoom()
    {
        GetCurrentRoomPlayers(); 
    }

    private void GetCurrentRoomPlayers()
    {
        foreach (KeyValuePair<int, Player> playerinfo in PhotonNetwork.CurrentRoom.Players) 
      {
            AddPlayerListing(playerinfo.Value);
        }

    }

    public void OnClick_Button()
    {
        foreach (KeyValuePair<int, Player> playerinfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerinfo.Value);
        }

    }

    private void AddPlayerListing (Player player)
    {
        PlayerListing listing = Instantiate(_playerListingPrefab, content);
        if (listing != null)
        {
            listing.SetPlayerInfo(player);
            _listings.Add(listing);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _listings.FindIndex( x=> x.Player == otherPlayer );
        if (index != -1 )
        {
            Destroy(_listings[index].gameObject );
            _listings.RemoveAt(index);
        }
    }
}
