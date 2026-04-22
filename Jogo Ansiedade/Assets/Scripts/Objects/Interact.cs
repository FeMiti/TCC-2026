using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interact : MonoBehaviour
{

    [Header("Properties")]
    private bool playerNear=false;

    [Header("Minigame")]
    [SerializeField] private GameObject minigamePrefab;
    [SerializeField] private TaskList taskList;

    public void Interaction(MinigameManager manager)
    {
        if (!playerNear) return;

        if (TaskManager.Instance.currentTask != taskList)
        {
            Debug.Log("Não é a tarefa.");
            return;
        }

        if (minigamePrefab != null)
        {
            manager.OpenMinigame(minigamePrefab);
        }
        else
        {
            Debug.LogWarning("Sem minigame atribuido.");
        }
    }

    private void OnTriggerEnter()
    {
        playerNear=true;
        Debug.Log("Player perto!");
    }

    private void OnTriggerExit()
    {
        playerNear=false;
        Debug.Log("Player longe!");
    }
}
