using UnityEngine;

public class Compulsivo : MonoBehaviour, IMinigame
{

    private MinigameManager minigameManager;

    public void Setup(MinigameManager manager)
    {
        minigameManager=manager;
    }

    public void Botao()
    {
        Debug.Log("apertou");
        AnxietyManager.Instance.DecreaseAnxiety(5);
        FinishMinigame();
    }

    public void FinishMinigame()
    {
        minigameManager.CloseMinigame();
        TaskManager.Instance.PickNextTask();
    }
}
