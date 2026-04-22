using UnityEngine;

public class Verde : MonoBehaviour, IMinigame
{
    private MinigameManager minigameManager;

    public void Setup(MinigameManager manager)
    {
        minigameManager=manager;
    }

    public void MinigameVerde()
    {
        Debug.Log("verde");
        FinishMinigame();
    }

    public void FinishMinigame()
    {
        TaskManager.Instance.CompleteTask(TaskList.Verde);
        minigameManager.CloseMinigame();
    }
}
