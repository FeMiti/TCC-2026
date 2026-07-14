using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class LavarMaos : MonoBehaviour, IMinigame
{
    private MinigameManager minigameManager;

    private int numberOfCommands;

    private List<ArrowData> arrows = new();

    [SerializeField] private Transform sequenceContainer;

    [SerializeField] private GameObject arrowPrefab;

    private int currentIndex;

    private ArrowDirections currentDirection;

    private bool minigameStarted=false;

    public void Setup(MinigameManager manager)
    {
        minigameManager=manager;

        numberOfCommands=Random.Range(3,7);
        Debug.Log("Numero de setas:" + numberOfCommands);
        SetArrows();
    }

    private void Start()
    {
        StartMinigame();
    }

    private void Update()
    {
        if (minigameStarted)
        {
            if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                currentDirection=ArrowDirections.Up;
                CheckDirections();
            }
            else if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                currentDirection=ArrowDirections.Left;
                CheckDirections();
            }
            else if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                currentDirection=ArrowDirections.Down;
                CheckDirections();
            }
            else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                currentDirection=ArrowDirections.Right;
                CheckDirections();
            }
        }
    }

    private void SetArrows()
    {
        for(int i = 0; i < numberOfCommands; i++)
        {
            GameObject arrowUI = Instantiate(arrowPrefab,sequenceContainer);
            ArrowDirections randomDirection=(ArrowDirections)Random.Range(0,4);

            arrowUI.transform.localRotation=Quaternion.Euler(0,0,90*(int)randomDirection);

            Debug.Log("Seta numero " + i + " criada");

            ArrowData arrow = new()
            {
                uiObject = arrowUI,
                direction = randomDirection,
                rawImage = arrowUI.GetComponent<RawImage>()
            };

            arrows.Add(arrow);

            Debug.Log("Seta numero " + i + " salva");
        }
    }

    private void StartMinigame()
    {
        Debug.Log("Minigame starting");
        minigameStarted=true;

        arrows[0].rawImage.color=Color.yellow;

        currentIndex=0;
        Debug.Log("Minigame started");
    }

    private void CheckDirections()
    {
        minigameStarted=false;
        if (currentDirection != arrows[currentIndex].direction)
        {
            StartCoroutine(FailMinigame());
        }
        else
        {
            NextDirection();
        }
    }

    private void NextDirection()
    {
        arrows[currentIndex].rawImage.color=Color.green;
        currentIndex++;
        if (currentIndex >= numberOfCommands)
        {
            FinishMinigame();
        }
        else
        {
            arrows[currentIndex].rawImage.color=Color.yellow;
            minigameStarted=true;
        }
    }

    public IEnumerator FailMinigame()
    {
        AnxietyManager.Instance.IncreaseAnxiety(10);

        yield return StartCoroutine(BlinkRed());

        currentIndex=0;
        arrows[0].rawImage.color=Color.yellow;
        minigameStarted=true;
    }

    public IEnumerator BlinkRed()
    {
        for(int i=0; i < numberOfCommands; i++)
        {
            arrows[i].rawImage.color=Color.red;
        }

        yield return new WaitForSecondsRealtime(1f);

        for(int i=0; i < numberOfCommands; i++)
        {
            arrows[i].rawImage.color=Color.white;
        }
    }

    public void FinishMinigame()
    {
        minigameManager.CloseMinigame();
        TaskManager.Instance.PickNextTask();
    }
}
