using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Text;

// Represents a single message in the chat conversation
[System.Serializable]
public class ChatMessage
{
    public string role; // "user", "assistant", etc.
    public string content; // The message content
}

// Represents the request payload for the OpenAI Chat Completions API
[System.Serializable]
public class ChatRequest
{
    public string model; // The model name (e.g., "gpt-4")
    public List<ChatMessage> messages; // List of messages in the conversation
}

// Represents the configuration for OpenAI API
[System.Serializable]
public class OpenAIConfig
{
    public string apiKey; // API key for authentication
    public string apiUrl; // URL of the OpenAI API
    public string model; // Model name (e.g., "gpt-4")
}

// MonoBehaviour script to manage communication with OpenAI Chat Completions API
public class ChatGPTManager : MonoBehaviour
{
    // Coroutine to send a user message to the OpenAI API and handle the response
    public IEnumerator SendChat(string userMessage, System.Action<string> onReply)
    {
        // Load the OpenAI configuration from a JSON file in Resources
        TextAsset configText = Resources.Load<TextAsset>("Config/openai_config");
        OpenAIConfig config = JsonUtility.FromJson<OpenAIConfig>(configText.text);

        // Create the request payload
        ChatRequest request = new ChatRequest
        {
            model = config.model,
            messages = new List<ChatMessage>
            {
                new ChatMessage { role = "user", content = userMessage }
            }
        };

        // Serialize the request payload to JSON
        string body = JsonUtility.ToJson(request);

        // Create and configure the UnityWebRequest
        UnityWebRequest webRequest = new UnityWebRequest(config.apiUrl, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(body);
        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");
        webRequest.SetRequestHeader("Authorization", "Bearer " + config.apiKey);

        // Send the request and wait for the response
        yield return webRequest.SendWebRequest();

        // Handle the response
        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            string response = webRequest.downloadHandler.text;
            onReply?.Invoke(response); // Invoke the callback with the response
        }
        else
        {
            Debug.LogError("ChatGPT error: " + webRequest.error);
        }
    }
}