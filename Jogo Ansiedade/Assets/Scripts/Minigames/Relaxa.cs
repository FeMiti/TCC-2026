using UnityEngine;

public class Relaxa : MonoBehaviour, IMinigame
{

    private MinigameManager minigameManager;

    public void Setup(MinigameManager manager)
    {
        minigameManager=manager;
    }

    public void Relaxar()
    {
        Debug.Log("Relaxei.");
        AnxietyManager.Instance.DecreaseAnxiety(10);
    }

    public void FinishMinigame()
    {
        minigameManager.CloseMinigame();
    }
}
