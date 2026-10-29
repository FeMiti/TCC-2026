using System;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public static EndingManager Instance;

    public EndingList currentEnding=EndingList.None;

    private ClockUI clockUI;

    [SerializeField] private GameObject sister;

    private void Awake()
    {
        Instance=this;

        clockUI=GetComponentInChildren<ClockUI>();
    }

    public void EndGame(EndingList ending)
    {
        currentEnding=ending;

        ClockManager.Instance.StopAllCoroutines();
        clockUI.EndClock(ending);

        if (MinigameManager.Instance.onMinigame)
        {
            MinigameManager.Instance.CloseMinigame();
        }

        if (currentEnding == EndingList.OutOfTime)
        {
            sister.SetActive(true);
        }

        if (currentEnding != EndingList.TasksCompleted)
        {
            DataCollector.Instance.gameDatas[DataCollector.Instance.currentData].anxietyAfter=AnxietyManager.Instance.currentAnxiety;
            DataCollector.Instance.gameDatas[DataCollector.Instance.currentData].timeAtConclusion=Time.time;
        }
        
        DataCollector.Instance.ending=currentEnding;

        TaskManager.Instance.PickEndingTask();
    }
}
