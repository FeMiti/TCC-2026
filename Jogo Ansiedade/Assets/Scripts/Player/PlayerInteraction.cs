using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    [Header("Raycast Settings")]
    [SerializeField] private float interactDistance=3f;
    [SerializeField] private Transform cameraTransform;

    private Interact currentInterectable;

    // Update is called once per frame
    void Update()
    {
        CheckInteraction();

        if (currentInterectable!=null && Input.GetKeyDown(KeyCode.E))
        {
            currentInterectable.Interaction();
        }    
    }

    private void CheckInteraction()
    {
        Ray ray = new Ray(cameraTransform.position,cameraTransform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, interactDistance))
        {
            Interact interact = hit.collider.GetComponent<Interact>();

            if (interact!=null)
            {
                if (currentInterectable != interact)
                {
                    currentInterectable=interact;
                    Debug.Log("Olhando para objeto!");
                }
                return;
            }
        }

        currentInterectable=null;
    }
}
