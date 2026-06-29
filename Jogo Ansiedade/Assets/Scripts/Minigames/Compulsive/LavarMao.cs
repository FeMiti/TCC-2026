using System.Collections.Generic;
using UnityEngine;

public class LavarMao : MonoBehaviour, IMinigame
{
    private MinigameManager minigameManager;

    private int numberOfCommands;

    private int[] commands;

    public void Setup(MinigameManager manager)
    {
        minigameManager=manager;
    }

    public void FinishMinigame()
    {
        minigameManager.CloseMinigame();
        TaskManager.Instance.PickNextTask();
    }
}
