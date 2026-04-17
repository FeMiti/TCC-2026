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
        minigameManager.CloseMinigame();
    }
}
