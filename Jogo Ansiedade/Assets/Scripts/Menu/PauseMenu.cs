using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused=false;
    public bool onEnding=false;

    public static PauseMenu Instance;

    [SerializeField] private CanvasGroup gameUI;
    [SerializeField] private CanvasGroup minigameCanvas;

    [SerializeField] private GameObject pauseMenu;

    private void Awake()
    {
        Instance=this;
    }

    // Update is called once per frame
    void Update()
    {
        if(onEnding) return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        Time.timeScale=0f;
        SetCanvas(gameUI,false);
        SetCanvas(minigameCanvas,false);
        pauseMenu.SetActive(true);
        if (!MinigameManager.Instance.onMinigame)
        {  
            Cursor.visible=true;
            Cursor.lockState=CursorLockMode.None;
        }
        isPaused=true;
    }

    public void ResumeGame()
    {
        if (!MinigameManager.Instance.onMinigame)
        {
            Cursor.visible=false;
            Cursor.lockState=CursorLockMode.Locked;    
        }
        pauseMenu.SetActive(false);
        SetCanvas(minigameCanvas,true);
        SetCanvas(gameUI,true);
        Time.timeScale=1;
        isPaused=false;
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetCanvas(CanvasGroup canvas, bool state)
    {
        if (state)
        {
            canvas.alpha=1f;
        }
        else
        {
            canvas.alpha=0f;
        }

        canvas.interactable=state;
        canvas.blocksRaycasts=state;
    }
}
