using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    [SerializeField] private Transform container;

    private GameObject currentMinigame;

    public bool onMinigame=false;

    public void OpenMinigame(GameObject prefab)
    {
        onMinigame=true;

        if (currentMinigame != null)
        {
            Destroy(currentMinigame);
        }

        currentMinigame=Instantiate(prefab,container);

        var minigame = currentMinigame.GetComponent<IMinigame>();
        if (minigame != null)
        {
            minigame.Setup(this);
        }
        else
        {
            Debug.LogWarning("Sem IMinigame");
        }

        currentMinigame.transform.localPosition=Vector3.zero;
        currentMinigame.transform.localScale=Vector3.one;

        Time.timeScale=0f;
        Cursor.lockState=CursorLockMode.None;
        Cursor.visible=true;
    }

    public void CloseMinigame()
    {
        if (currentMinigame != null)
        {
            Destroy(currentMinigame);
        }

        Time.timeScale=1f;
        Cursor.lockState=CursorLockMode.Locked;
        Cursor.visible=false;
        onMinigame=false;
    }
}
