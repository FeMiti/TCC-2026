using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public static EndingManager Instance;

    public EndingList currentEnding=EndingList.None;

    private ClockUI clockUI;

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

        TaskManager.Instance.PickEndingTask();
    }
}
