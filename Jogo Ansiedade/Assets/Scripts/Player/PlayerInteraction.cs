using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    [Header("Raycast Settings")]
    [SerializeField] private float interactDistance=3f;
    [SerializeField] private Transform cameraTransform;

    [SerializeField] private MinigameManager manager;

    private IInteract currentInterectable;

    // Update is called once per frame
    void Update()
    {
        CheckMinigameInteraction();

        if (currentInterectable!=null && Input.GetKeyDown(KeyCode.E))
        {
            currentInterectable.Interaction();
        }    
    }

    private void CheckMinigameInteraction()
    {
        Ray ray = new Ray(cameraTransform.position,cameraTransform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, interactDistance))
        {
            IInteract interact = hit.collider.GetComponent<IInteract>();

            if (interact!=null)
            {
                if (currentInterectable != interact)
                {
                    currentInterectable=interact;
                    Debug.Log("Olhando para minigame!");
                }
                return;
            }
        }

        currentInterectable=null;
    }
}
