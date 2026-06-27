using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class TaskMinigameInteract : MonoBehaviour, IInteract
{

    [Header("Properties")]
    private bool playerNear=false;

    [Header("Minigame")]
    [SerializeField] private GameObject minigamePrefab;
    [SerializeField] private TaskList taskList;

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
        Debug.Log("Player perto!");
    }

    public void OnTriggerExit()
    {
        playerNear=false;
        Debug.Log("Player longe!");
    }
}
