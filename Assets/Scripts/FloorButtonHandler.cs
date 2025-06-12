using UnityEngine;
using TMPro;

public class FloorButtonHandler : MonoBehaviour
{
    public ChatGPTManager chatGPTManager;
    public FloorManager floorManager;
    public TextMeshProUGUI currentFloorText;
    public TMP_InputField questionInputField; // Otomatik soru yazılacak alan
    public TextMeshProUGUI replyTextUI;       // ChatGPT cevabı buraya yazılacak

    private void DisplayQuestion(string floorName, string question)
    {
        currentFloorText.text = "Floor: " + floorName;
        if (questionInputField != null)
        {
            questionInputField.text = question;
        }
    }

    private void DisplayAnswer(string reply)
    {
        replyTextUI.text = reply;
        Debug.Log("ChatGPT Response: " + reply);
    }

    private void AskQuestion(int floorIndex, string floorName, string question)
    {
        DisplayQuestion(floorName, question);
        floorManager.ActivateFloor(floorIndex);

        chatGPTManager.StartCoroutine(chatGPTManager.SendChat(
            question,
            DisplayAnswer
        ));
    }

    public void AskEducationQuestion()
    {
        AskQuestion(0, "Education Background", "How suitable is this candidate's education for the position?");
    }

    public void AskWorkBackgroundQuestion()
    {
        AskQuestion(1, "Work Background", "How well does this candidate’s previous work experience align with the role requirements?");
    }

    public void AskTechnicalSkillsQuestion()
    {
        AskQuestion(2, "Technical Skills", "How effective are the candidate's technical skills in fulfilling this job?");
    }

    public void AskFutureReadinessQuestion()
    {
        AskQuestion(3, "Future Readiness", "How well is this candidate prepared for future growth in this field?");
    }

    public void AskSoftSkillsQuestion()
    {
        AskQuestion(4, "Soft Skills", "To what extent does this candidate demonstrate strong soft skills such as communication and teamwork?");
    }

    public void AskCompanyCultureFitQuestion()
    {
        AskQuestion(5, "Company Culture Fit", "How well does this candidate fit into the company's culture and values?");
    }
}
