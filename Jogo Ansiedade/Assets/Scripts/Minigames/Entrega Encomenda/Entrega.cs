using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Entrega : MonoBehaviour, IMinigame
{
    private MinigameManager minigameManager;

    [SerializeField] private List<string> targetWords;

    [SerializeField] private TMP_Text targetWordText;

    [SerializeField] private TMP_Text answerInput;

    private string currentWord;

    private int currentIndex=0;

    public void Setup(MinigameManager manager)
    {
        minigameManager=manager;

        currentWord=targetWords[Random.Range(0,targetWords.Count)];
        targetWordText.text=currentWord;
        UpdateDisplay();
    }

    private void Update()
    {
        if (Input.inputString.Length > 0)
        {
            char typedChar=Input.inputString[0];

            CheckCharacter(typedChar);
        }
    }

    public void CheckCharacter(char currentChar)
    {
        if (currentChar == currentWord[currentIndex])
        {
            currentIndex++;

            UpdateDisplay();

            if (currentIndex >= currentWord.Length)
            {
                AnxietyManager.Instance.DecreaseAnxiety(10);
                FinishMinigame();
            }
        }
        else
        {
            AnxietyManager.Instance.IncreaseAnxiety(5);
        }
    }

    private void UpdateDisplay()
    {
        string typed=currentWord.Substring(0,currentIndex);

        string current=currentIndex<currentWord.Length?currentWord[currentIndex].ToString():"";

        string remaining=currentIndex<currentWord.Length-1?currentWord.Substring(currentIndex+1):"";

        answerInput.text=
            $"<color=black>{typed}</color>"+
            $"<color=yellow>{current}</color>"+
            $"<color=white>{remaining}</color>";
    }

    public void FinishMinigame()
    {
       TaskManager.Instance.CompleteTask(TaskList.ReceberEntrega);
       minigameManager.CloseMinigame(); 
    }
}
