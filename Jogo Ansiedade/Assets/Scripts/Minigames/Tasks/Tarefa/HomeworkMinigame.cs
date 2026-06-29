using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HomeworkMinigame : MonoBehaviour, IMinigame
{
    private MinigameManager minigameManager;

    [SerializeField] private List<HomeworkData> homeworks;

    [SerializeField] private TMP_InputField answerInput;

    [SerializeField] private TMP_Text question;

    private HomeworkData currentHomework;

    public void Setup(MinigameManager manager)
    {
        minigameManager=manager;

        currentHomework=homeworks[Random.Range(0,homeworks.Count)];
        question.text=currentHomework.question;
    }

    public void CheckAnswer()
    {
        bool isCorrect=false;
        for(int i = 0; i < currentHomework.answers.Count(); i++)
        {
            if (answerInput.text.Trim().ToLower() == currentHomework.answers[i].Trim().ToLower())
            {
                isCorrect=true;
                break;
            }
        }

        if (isCorrect)
        {
            RightAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }

    private void RightAnswer()
    {
        AnxietyManager.Instance.DecreaseAnxiety(10);
        FinishMinigame();
    }

    private void WrongAnswer()
    {
        AnxietyManager.Instance.IncreaseAnxiety(10);
        answerInput.text="";
    }

    public void FinishMinigame()
    {
        TaskManager.Instance.CompleteTask(TaskList.FazerTarefa);
        minigameManager.CloseMinigame();
    }
}
