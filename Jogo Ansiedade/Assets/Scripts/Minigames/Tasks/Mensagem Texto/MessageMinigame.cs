using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class MessageMinigame : MonoBehaviour, IMinigame
{

    private MinigameManager minigameManager;

    [SerializeField] private List<MessagesData> messages;

    [SerializeField] private MessageButtons[] buttons;

    [SerializeField] private TMP_Text questionText;

    private int correctButton;

    private MessagesData currentMessage;

    public void Setup(MinigameManager manager)
    {
        minigameManager=manager;

        int rand = Random.Range(0,manager.mensagens.Count);

        currentMessage=messages[manager.mensagens[rand]];

        manager.mensagens.RemoveAt(rand);

        questionText.text=currentMessage.question;
        currentMessage=ShuffleWrongMessages(currentMessage);

        correctButton=Random.Range(0,buttons.Length);
        ShuffleButtons();
    }

    private MessagesData ShuffleWrongMessages(MessagesData current)
    {
        for(int i=0; i < currentMessage.wrongAnswers.Count();i++)
        {
            int rand = Random.Range(i,currentMessage.wrongAnswers.Count());

            (current.wrongAnswers[i], current.wrongAnswers[rand]) = (current.wrongAnswers[rand], current.wrongAnswers[i]);
        }

        return current;
    }

    private void ShuffleButtons()
    {
        int j=0;
        for(int i=0; i < buttons.Length; i++)
        {
            if (i == correctButton)
            {
                buttons[i].Setup(currentMessage.correctAnswer, this, true);
            }

            else
            {
                buttons[i].Setup(currentMessage.wrongAnswers[j], this, false);
                j++;
            }
        }
    }

    public void RightAnswer()
    {
        AnxietyManager.Instance.DecreaseAnxiety(5);
        FinishMinigame();
    }

    public void WrongAnswer()
    {
        AnxietyManager.Instance.IncreaseAnxiety(10);
    }
    
    public void FinishMinigame()
    {
        TaskManager.Instance.CompleteTask(TaskList.ResponderMensagem);
        minigameManager.CloseMinigame();
    }
}
