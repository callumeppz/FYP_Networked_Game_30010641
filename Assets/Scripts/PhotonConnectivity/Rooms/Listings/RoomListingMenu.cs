using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class RoomListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private Transform content; // Reference to the content transform where room listings will be instantiated
    [SerializeField] private RoomListing _roomListingPrefab; // Reference to the prefab used for room listings
    private List<RoomListing> _listings = new List<RoomListing>(); // List to store instantiated room listings

    // callback function invoked when the room list is updated
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // clear existing rooms
        foreach (var listing in _listings)
        {
            Destroy(listing.gameObject);
        }
        _listings.Clear();

        // iteratre through updated listings and create a new room listing
        foreach (RoomInfo info in roomList)
        {
            // skip invisble (private) rooms
            if (!info.IsVisible) continue;
            // instantiate room listing 
            RoomListing listing = Instantiate(_roomListingPrefab, content);
            if (listing != null)
            {
                // set room info for new listing and add to listings list
                listing.SetRoomInfo(info);
                _listings.Add(listing);
            }
        }
    }
}
