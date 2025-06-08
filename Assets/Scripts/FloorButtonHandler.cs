using UnityEngine;
using UnityEngine.UI;

public class FloorButtonHandler : MonoBehaviour
{
    public ChatGPTManager chatGPTManager;

    public void AskEducationQuestion()
    {
        string question = "How suitable is this candidate's education for the position?";
        chatGPTManager.StartCoroutine(chatGPTManager.SendChat(question, DisplayAnswer));
    }

    public void AskWorkBackgroundQuestion()
    {
        string question = "How well does this candidate’s previous work experience align with the role requirements?";
        chatGPTManager.StartCoroutine(chatGPTManager.SendChat(question, DisplayAnswer));
    }

    public void AskTechnicalSkillsQuestion()
    {
        string question = "How effective are the candidate's technical skills in fulfilling this job?";
        chatGPTManager.StartCoroutine(chatGPTManager.SendChat(question, DisplayAnswer));
    }

    public void AskFutureReadinessQuestion()
    {
        string question = "How well is this candidate prepared for future growth in this field?";
        chatGPTManager.StartCoroutine(chatGPTManager.SendChat(question, DisplayAnswer));
    }

    public void AskSoftSkillsQuestion()
    {
        string question = "To what extent does this candidate demonstrate strong soft skills such as communication and teamwork?";
        chatGPTManager.StartCoroutine(chatGPTManager.SendChat(question, DisplayAnswer));
    }

    public void AskCompanyCultureFitQuestion()
    {
        string question = "How well does this candidate fit into the company's culture and values?";
        chatGPTManager.StartCoroutine(chatGPTManager.SendChat(question, DisplayAnswer));
    }

    private void DisplayAnswer(string reply)
    {
        Debug.Log("ChatGPT Response: " + reply);
        // You can connect this to a UI Text element or any in-game feedback system
    }
}
