using UnityEngine;

public class Amarelo : MonoBehaviour, IMinigame
{
    private MinigameManager minigameManager;

    public void Setup(MinigameManager manager)
    {
        minigameManager=manager;
    }

    public void MinigameAmarelo()
    {
        Debug.Log("amarelo");
        FinishMinigame();
    }

    public void FinishMinigame()
    {
        TaskManager.Instance.CompleteTask(TaskList.Amarelo);
        minigameManager.CloseMinigame();
    }
}
