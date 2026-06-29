using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChecarPorta : MonoBehaviour, IMinigame
{
    private MinigameManager minigameManager;

    [SerializeField] private TMP_Text number;

    [SerializeField] private GameObject readyButton;

    [SerializeField] private GameObject[] checkingObjects;

    [SerializeField] private Slider timeBar;

    [SerializeField] private TMP_Text checksText;

    private float maxTime=5f;

    private float currentTime;

    private int numberOfChecks;

    private int timesChecked=0;

    private bool minigameStarted=false;

    private bool firstTime=true;

    private void Update()
    {
        if (minigameStarted)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                timesChecked++;
                checksText.text=timesChecked.ToString();
            }
        }
    }

    public void Setup(MinigameManager manager)
    {
        minigameManager=manager;
        
        numberOfChecks=Random.Range(3,8);
        number.text=numberOfChecks.ToString();
    }

    public void MinigameStart()
    {
        readyButton.SetActive(false);
        checksText.text=timesChecked.ToString();
        if (firstTime)
        {
            for(int i = 0; i < checkingObjects.Length; i++)
            {
                checkingObjects[i].SetActive(true);
            }
        }
        currentTime=maxTime;
        timeBar.maxValue=maxTime;
        timeBar.value=currentTime;
        minigameStarted=true;

        StartCoroutine(RunMinigame());
    }

    public IEnumerator RunMinigame()
    {
        Debug.Log("Corrotina startou");

        while (currentTime > 0)
        {
            yield return new WaitForSecondsRealtime(1f);

            currentTime--;

            timeBar.value=currentTime;
        }

        minigameStarted=false;

        if (timesChecked == numberOfChecks)
        {
            AnxietyManager.Instance.DecreaseAnxiety(5);
            FinishMinigame();
        }
        else
        {
            MinigameFail();
        }
    }

    private void MinigameFail()
    {
        AnxietyManager.Instance.IncreaseAnxiety(10);
        firstTime=false;
        timesChecked=0;
        readyButton.SetActive(true);
    }

    public void FinishMinigame()
    {
        minigameManager.CloseMinigame();
        TaskManager.Instance.PickNextTask();
    }
}
