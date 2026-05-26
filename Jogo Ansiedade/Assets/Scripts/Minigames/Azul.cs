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
        Debug.Log("azul");
        FinishMinigame();
    }

    public void Vermelho()
    {
        Debug.Log("vermelho");
        AnxietyManager.Instance.IncreaseAnxiety(10);
    }

    public void Roxo()
    {
        Debug.Log("roxo");
        AnxietyManager.Instance.DecreaseAnxiety(10);
    }

    public void FinishMinigame()
    {
        TaskManager.Instance.CompleteTask(TaskList.Azul);
        minigameManager.CloseMinigame();
    }
}
