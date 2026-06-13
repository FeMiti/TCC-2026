using TMPro;
using UnityEngine;

public class MessageButtons : MonoBehaviour
{

    [SerializeField] private TMP_Text answer;

    private MessageMinigame manager;

    private bool correctAnswer;
    
    public void Setup(string answerText, MessageMinigame minigame, bool correct)
    {
        answer.text=answerText;
        manager=minigame;
        correctAnswer=correct;
    }

    public void OnClick()
    {
        if (correctAnswer)
        {
            manager.RightAnswer();
        }
        else
        {
            manager.WrongAnswer();
        }
    }
}