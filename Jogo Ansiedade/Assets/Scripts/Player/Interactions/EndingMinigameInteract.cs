using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class EndingMinigameInteract : MonoBehaviour, IInteract
{

    [Header("Properties")]
    private bool playerNear=false;

    [Header("Minigame")]
    [SerializeField] private GameObject minigamePrefab;
    [SerializeField] private TaskList taskList;

    [SerializeField] private CanvasGroup gameUI;

    public void Interaction()
    {
        if (!playerNear) return;

        if (TaskManager.Instance.currentTask != taskList)
        {
            Debug.Log("Não é a tarefa.");
            return;
        }

        if (minigamePrefab != null)
        {
            HighlightManager.Instance.ClearHighlight();
            PauseMenu.Instance.onEnding=true;
            PauseMenu.Instance.SetCanvas(gameUI,false);
            MinigameManager.Instance.OpenMinigame(minigamePrefab);
        }
        else
        {
            Debug.LogWarning("Sem minigame atribuido.");
        }
    }

    public void OnTriggerEnter()
    {
        playerNear=true;
    }

    public void OnTriggerExit()
    {
        playerNear=false;
    }
}
