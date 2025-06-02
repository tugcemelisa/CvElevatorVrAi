using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChatUIManager : MonoBehaviour
{
    public TMP_InputField userInputField; // Input field for user messages
    public Button sendButton;  // Button to send the message
    public ChatGPTManager chatGPTManager; // Reference to the ChatGPTManager script
    public TextMeshProUGUI replyText; // Text element to display the response

    void Start()
    {
        sendButton.onClick.AddListener(SendMessageToGPT);
    }

    void SendMessageToGPT()
    {
        // Get the user input and start the coroutine to send the message
        string message = userInputField.text;
        StartCoroutine(chatGPTManager.SendChat(message, OnReply));
    }

    void OnReply(string response)
    {
        replyText.text = response;
    }
}
