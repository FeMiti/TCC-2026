using UnityEngine;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GameObject startMenu;
    [SerializeField] private CanvasGroup mainUI;

    private void Start()
    {
        Time.timeScale=0f;
        Cursor.visible=true;
        Cursor.lockState=CursorLockMode.None;
        PauseMenu.Instance.onEnding=true;
        PauseMenu.Instance.SetCanvas(mainUI,false);
    }

    public void StartGame()
    {
        Debug.Log("Clicou Start");
        Cursor.visible=false;
        Cursor.lockState=CursorLockMode.Locked;
        startMenu.SetActive(false);
        PauseMenu.Instance.SetCanvas(mainUI,true);
        PauseMenu.Instance.onEnding=false;
        Time.timeScale=1f;
    }

    public void QuitGame()
    {
        Debug.Log("Clicou Quit");
        Application.Quit();
    }
}
