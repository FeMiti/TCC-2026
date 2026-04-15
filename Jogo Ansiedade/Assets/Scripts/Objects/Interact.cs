using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Interact : MonoBehaviour
{

    [Header("Properties")]
    private bool playerNear=false;

    public void Interaction()
    {
        if (playerNear)
        {
            Debug.Log("Interagiu com o objeto!");
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
