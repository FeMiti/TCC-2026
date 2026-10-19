using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HomeworkMinigame : MonoBehaviour, IMinigame
{
    private MinigameManager minigameManager;


    [Header("Homeworks")]
    [SerializeField] private List<HomeworkData> mathHomework;
    [SerializeField] private List<HomeworkData> biologyHomework;
    [SerializeField] private List<HomeworkData> historyHomework;
    [SerializeField] private List<HomeworkData> geographyHomework;
    [SerializeField] private List<HomeworkData> scienceHomework;

    [SerializeField] private TMP_InputField answerInput;

    [SerializeField] private TMP_Text question;

    private HomeworkData currentHomework;

    public void Setup(MinigameManager manager)
    {
        minigameManager=manager;

        int rand = Random.Range(0,manager.tarefas.Count);

        switch (rand)
        {
            case 0:
                currentHomework=mathHomework[Random.Range(0,mathHomework.Count)];
                break;
            case 1:
                currentHomework=biologyHomework[Random.Range(0,biologyHomework.Count)];
                break;
            case 2:
                currentHomework=historyHomework[Random.Range(0,historyHomework.Count)];
                break;
            case 3:
                currentHomework=geographyHomework[Random.Range(0,geographyHomework.Count)];
                break;
            case 4:
                currentHomework=scienceHomework[Random.Range(0,scienceHomework.Count)];
                break;
        }

        manager.tarefas.RemoveAt(rand);

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
