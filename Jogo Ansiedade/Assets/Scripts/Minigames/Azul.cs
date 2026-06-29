using UnityEngine;

public class Azul : MonoBehaviour, IMinigame
{
    private MinigameManager minigameManager;

    public void Setup(MinigameManager manager)
    {
        minigameManager=manager;
    }

    public void MinigameAzul()
    {
        FinishMinigame();
    }

    public void Vermelho()
    {
        AnxietyManager.Instance.IncreaseAnxiety(10);
    }

    public void Roxo()
    {
        AnxietyManager.Instance.DecreaseAnxiety(10);
    }

    public void FinishMinigame()
    {
        TaskManager.Instance.CompleteTask(TaskList.Azul);
        minigameManager.CloseMinigame();
    }
}
