using UnityEngine;

public class RelaxMinigameInteract : MonoBehaviour, IInteract
{

    [Header("Properties")]
    private bool playerNear=false;

    [Header("Minigame")]
    [SerializeField] private GameObject minigamePrefab;


    public void Interaction()
    {
        if(!playerNear) return;

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
