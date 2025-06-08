using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System;

[System.Serializable]
public class ChatMessage
{
    public string role;
    public string content;
}

[System.Serializable]
public class ChatRequest
{
    public string model;
    public List<ChatMessage> messages;
}

[System.Serializable]
public class OpenAIConfig
{
    public string apiKey;
    public string apiUrl;
    public string model;
}

[System.Serializable]
public class GoogleTTSConfig
{
    public string apiKey;
}

[System.Serializable]
public class ChatChoice
{
    public ChatMessage message;
}

[System.Serializable]
public class ChatResponse
{
    public List<ChatChoice> choices;
}

[System.Serializable]
public class GoogleTTSResponse
{
    public string audioContent;
}

public class ChatGPTManager : MonoBehaviour
{
    public AudioSource audioSource;
    private string googleTTSApiKey;

    public IEnumerator SendChat(string userMessage, Action<string> onReply)
    {
        // Load OpenAI config
        TextAsset openAiConfigText = Resources.Load<TextAsset>("Config/openai_config");
        OpenAIConfig config = JsonUtility.FromJson<OpenAIConfig>(openAiConfigText.text);

        // Load Google TTS config
        TextAsset googleTTSConfigText = Resources.Load<TextAsset>("Config/google_tts_config");
        GoogleTTSConfig ttsConfig = JsonUtility.FromJson<GoogleTTSConfig>(googleTTSConfigText.text);
        googleTTSApiKey = ttsConfig.apiKey;

        string modifiedPrompt =
            "Please evaluate the candidate based on the following question. Limit the response to a maximum of 5 sentences. End the response with a new line and a percentage value of suitability for the position.\n"
            + userMessage;

        ChatRequest request = new ChatRequest
        {
            model = config.model,
            messages = new List<ChatMessage>
            {
                new ChatMessage { role = "user", content = modifiedPrompt }
            }
        };

        string body = JsonUtility.ToJson(request);

        using (UnityWebRequest webRequest = new UnityWebRequest(config.apiUrl, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(body);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + config.apiKey);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string json = webRequest.downloadHandler.text;
                ChatResponse response = JsonUtility.FromJson<ChatResponse>(json);
                if (response != null && response.choices != null && response.choices.Count > 0)
                {
                    string reply = response.choices[0].message.content;
                    onReply?.Invoke(reply);
                    StartCoroutine(PlayGoogleTTS(reply));
                }
                else
                {
                    onReply?.Invoke("[Error: Invalid response format]");
                }
            }
            else
            {
                Debug.LogError("ChatGPT error: " + webRequest.error);
                onReply?.Invoke("[Error: Network failure]");
            }
        }
    }

    private IEnumerator PlayGoogleTTS(string text)
    {
        string ttsUrl = "https://texttospeech.googleapis.com/v1/text:synthesize?key=" + googleTTSApiKey;

        string jsonPayload = "{\"input\":{\"text\":\"" + text.Replace("\"", "\\\"") + "\"}," +
                             "\"voice\":{\"languageCode\":\"en-US\",\"ssmlGender\":\"FEMALE\"}," +
                             "\"audioConfig\":{\"audioEncoding\":\"LINEAR16\"}}";

        UnityWebRequest ttsRequest = new UnityWebRequest(ttsUrl, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);
        ttsRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        ttsRequest.downloadHandler = new DownloadHandlerBuffer();
        ttsRequest.SetRequestHeader("Content-Type", "application/json");

        yield return ttsRequest.SendWebRequest();

        if (ttsRequest.result == UnityWebRequest.Result.Success)
        {
            string json = ttsRequest.downloadHandler.text;
            string base64Audio = JsonUtility.FromJson<GoogleTTSResponse>(json).audioContent;
            byte[] audioBytes = Convert.FromBase64String(base64Audio);

            WAV wav = new WAV(audioBytes);
            AudioClip audioClip = AudioClip.Create("TTS_Audio", wav.SampleCount, 1, wav.Frequency, false);
            audioClip.SetData(wav.LeftChannel, 0);

            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Google TTS error: " + ttsRequest.error);
        }
    }
}
