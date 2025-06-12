using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ChatUIManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_InputField userInputField;
    public Button sendButton;
    public ChatGPTManager chatGPTManager;
    public TextMeshProUGUI replyText;
    public ScrollRect replyScrollRect;

    void Start()
    {
        if (sendButton != null)
        {
            sendButton.onClick.AddListener(SendMessageToGPT);
        }
    }

    public void SendMessageToGPT()
    {
        string message = userInputField.text.Trim();

        if (!string.IsNullOrEmpty(message))
        {
            
            replyText.text += $"\n<color=#86C5FF>You:</color> {message}";
            userInputField.text = "";

           
            StartCoroutine(chatGPTManager.SendChat(message, OnReply));
        }
    }

    void OnReply(string response)
    {
        if (replyText != null)
        {
            replyText.text += $"\n<color=#FFD580>AI:</color> {response}";
        }

        StartCoroutine(ScrollToBottomSmoothly());
    }

    IEnumerator ScrollToBottomSmoothly()
    {
        yield return null; 
        Canvas.ForceUpdateCanvases();

        if (replyScrollRect != null)
        {
            replyScrollRect.verticalNormalizedPosition = 0f;
        }
    }
}
