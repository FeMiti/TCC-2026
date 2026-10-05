using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DoorInteract : MonoBehaviour, IInteract
{

    [Header("Properties")]
    private bool playerNear=false;

    [Header("Door")]
    [SerializeField] private float openAngle=90f;
    private float duration=1f;
    [SerializeField] private bool positiveAngle=true;


    private bool isOpen=false;
    private bool isMoving=false;

    private Quaternion openRotation;
    private Quaternion closedRotation;

    private void Start()
    {
        closedRotation=transform.localRotation;

        float negpos=positiveAngle?1:-1;

        openRotation=closedRotation*Quaternion.Euler(0f,negpos*openAngle,0f);
    }

    public void Interaction()
    {
        if(isMoving) return;

        StartCoroutine(RotateDoor());
    }

    private IEnumerator RotateDoor()
    {
        isMoving=true;

        Quaternion startRotation=transform.localRotation;
        Quaternion targetRotation=isOpen?closedRotation:openRotation;

        float elapsed=0f;

        while (elapsed < duration)
        {
            elapsed+=Time.deltaTime;

            float t=elapsed/duration;

            transform.localRotation=Quaternion.Lerp(startRotation,targetRotation,t);

            yield return null;
        }

        transform.localRotation=targetRotation;

        isOpen=!isOpen;
        isMoving=false;
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
