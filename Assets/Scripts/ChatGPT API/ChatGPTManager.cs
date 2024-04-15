using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using UnityEngine.UIElements;
using UnityEngine.Events;
using UnityEngine.UI;
using Unity.VisualScripting;
using Photon.Pun;

public class ChatGPTManager : MonoBehaviour
{

    /// <summary>
    /// This script took inspiration from the youtube video by Valem Tutorials released in 2023. Alongside documentation provided by Unity and OpenAI.
    /// https://www.youtube.com/watch?v=lYckk570Tqw&ab_channel=ValemTutorials
    /// </summary>



    // variable declaration 
    private OpenAIApi openAi = new OpenAIApi();
    public class OnResponseEvent : UnityEvent<string> { }
    public Text outputText;
    private Animator anim;
    public GameObject buttonToDisplay;
    public GameObject outputImage;
    public OnResponseEvent onResponse = new OnResponseEvent();
    private List<ChatMessage> messages = new List<ChatMessage>();
    [SerializeField] private string promptText;


    // start method used to initialise OPENAI API using provided API Key
    private void Start()
    {

#if OPENAI_API_KEY
    string apiKey = OPENAI_API_KEY;
    openAi = new OpenAIApi(apiKey);
#endif
    }


    // Method to interact with GPT
    public async void AskChatGPT(string newText)
    {
        // creation of new chat prompt 
        ChatMessage newMessage = new ChatMessage();
        newMessage.Content = newText;
        newMessage.Role = "user";
        messages.Add(newMessage);

        // preparing request for generation, which model etc
        CreateChatCompletionRequest request = new CreateChatCompletionRequest();
        request.Messages = messages;
        request.Model = "gpt-3.5-turbo";


        // retreival of response from OpenAI GPT
        var response = await openAi.CreateChatCompletion(request);
        if (response.Choices != null && response.Choices.Count > 0)
        {
            var chatResponse = response.Choices[0].Message;
            messages.Add(chatResponse);
            // Test 1
            Debug.Log("GPT Test 1 Passed");
            Debug.Log(chatResponse.Content);
            // Display response
            outputText.text = chatResponse.Content;
        }
    }

    // Triggers when collision occurs with player
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // display start game button if masterclient on Photon Servers
            outputImage.SetActive(true);
            if (PhotonNetwork.IsMasterClient) { buttonToDisplay.SetActive(true); }
            else { buttonToDisplay.SetActive(false); }
            Debug.Log("Player entered trigger collider. Initiating quest dialogue...");
            AskChatGPT(promptText); // initiate prompt for response
        }
    }

    // Triggers when player leaves collision area
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {  
            // deactiviating start button
            outputImage.SetActive(false);
            if (PhotonNetwork.IsMasterClient) { buttonToDisplay.SetActive(false); }


        }
    }
}
