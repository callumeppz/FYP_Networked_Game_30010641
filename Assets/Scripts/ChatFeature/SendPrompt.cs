using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviourPunCallbacks, IPunObservable
{

    /// <summary>
    /// This script took inspiration from the youtube video by Diving_Squid released in 2021. 
    /// https://www.youtube.com/watch?v=7cOtVzjy-6o&t=192s&ab_channel=diving_squid
    /// </summary>

    // initialise variables
    public GameObject bubblespeechobject;
    public Text updatetext;
    private InputField ChatInputField;
    private bool DisableSend;

    private void Start()
    {
        // finding input field in hierachy
        ChatInputField = GameObject.Find("ChatInputField").GetComponent<InputField>();

        if (ChatInputField != null)
        {
            Debug.Log("Found Input Field Test Passed"); // test debug
            ChatInputField.onEndEdit.AddListener(SubmitChatMessage); // if found, call submitchatmessage method
        }
        else
        {
            Debug.Log("Not Found Input Field Test Failed"); // test debug
        }
    }

    // method to submit chat message
    private void SubmitChatMessage(string message)
    {
        if (!DisableSend && !string.IsNullOrEmpty(message))
        {
            // send message to all players and cache messages for new players
            photonView.RPC("SendChatMessage", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.NickName, message);
            Debug.Log("Sent Chat");
            bubblespeechobject.SetActive(true);
            DisableSend = true;
            ChatInputField.text = "";
        }
    }

    // RPC call to show message for all players 
    [PunRPC]
    public void SendChatMessage(string sender, string message)
    {
        // Display Message and player in the feed
        updatetext.text = sender + ": " + message;
        StartCoroutine(Remove());
    }

    // Coroutine to remove chat after 5 seconds, allow for more chats
    IEnumerator Remove()
    {
        yield return new WaitForSeconds(5f);
        bubblespeechobject.SetActive(false);
        DisableSend = false;
    }

    // Serialize through Photon
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(bubblespeechobject.activeSelf);
        }
        else if (stream.IsReading)
        {
            bubblespeechobject.SetActive((bool)stream.ReceiveNext());
        }
    }
}
