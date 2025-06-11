using UnityEngine;
using TMPro;

public class FloorButtonHandler : MonoBehaviour
{
    public ChatGPTManager chatGPTManager;
    public FloorManager floorManager;
    public TextMeshProUGUI currentFloorText;
    public TextMeshProUGUI questionTextUI;
    public TextMeshProUGUI replyTextUI;

    public void AskEducationQuestion()
    {
        string floor = "Education Background";
        string question = "How suitable is this candidate's education for the position?";
        DisplayQuestion(floor, question);
        floorManager.ActivateFloor(0);
        chatGPTManager.StartCoroutine(chatGPTManager.SendChat(question, DisplayAnswer));
    }

    public void AskWorkBackgroundQuestion()
    {
        string floor = "Work Background";
        string question = "How well does this candidate’s previous work experience align with the role requirements?";
        DisplayQuestion(floor, question);
        floorManager.ActivateFloor(1);
        chatGPTManager.StartCoroutine(chatGPTManager.SendChat(question, DisplayAnswer));
    }

    public void AskTechnicalSkillsQuestion()
    {
        string floor = "Technical Skills";
        string question = "How effective are the candidate's technical skills in fulfilling this job?";
        DisplayQuestion(floor, question);
        floorManager.ActivateFloor(2);
        chatGPTManager.StartCoroutine(chatGPTManager.SendChat(question, DisplayAnswer));
    }

    public void AskFutureReadinessQuestion()
    {
        string floor = "Future Readiness";
        string question = "How well is this candidate prepared for future growth in this field?";
        DisplayQuestion(floor, question);
        floorManager.ActivateFloor(3);
        chatGPTManager.StartCoroutine(chatGPTManager.SendChat(question, DisplayAnswer));
    }

    public void AskSoftSkillsQuestion()
    {
        string floor = "Soft Skills";
        string question = "To what extent does this candidate demonstrate strong soft skills such as communication and teamwork?";
        DisplayQuestion(floor, question);
        floorManager.ActivateFloor(4);
        chatGPTManager.StartCoroutine(chatGPTManager.SendChat(question, DisplayAnswer));
    }

    public void AskCompanyCultureFitQuestion()
    {
        string floor = "Company Culture Fit";
        string question = "How well does this candidate fit into the company's culture and values?";
        DisplayQuestion(floor, question);
        floorManager.ActivateFloor(5);
        chatGPTManager.StartCoroutine(chatGPTManager.SendChat(question, DisplayAnswer));
    }

    private void DisplayQuestion(string floorName, string question)
    {
        currentFloorText.text = "Floor: " + floorName;
        questionTextUI.text = question;
    }

    private void DisplayAnswer(string reply)
    {
        replyTextUI.text = reply;
        Debug.Log("ChatGPT Response: " + reply);
    }
}
