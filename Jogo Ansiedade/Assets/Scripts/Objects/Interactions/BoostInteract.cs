using System.Collections;
using UnityEngine;

public class BoostInteract : MonoBehaviour, IInteract
{

    [Header("Properties")]
    private bool playerNear=false;
    private bool isBoosted=false;

    private int timesBoosted=1;

    public void Interaction()
    {
        if (!playerNear) return;

        if (isBoosted)
        {
            Debug.Log("Já está com boost.");
            return;
        }


        isBoosted=true;
        PlayerController.Instance.StartSprint();
        AnxietyManager.Instance.IncreaseAnxiety(5*timesBoosted);
        timesBoosted++;

        StartCoroutine(Booster(5f));
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

    private IEnumerator Booster(float time)
    {
        yield return new WaitForSecondsRealtime(time);

        PlayerController.Instance.StopSprint();
        isBoosted=false;
    }
}
