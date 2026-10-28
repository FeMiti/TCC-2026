using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;

public class TaskManager : MonoBehaviour
{

    public static TaskManager Instance;

    [SerializeField] private List<TaskList> allTasks;

    [SerializeField] private List<TaskList> allCompulsions;

    [SerializeField] private List<TaskList> allEndings;

    private List<TaskList> remainingTasks = new List<TaskList>();

    public TaskList currentTask{get; private set;}

    private TaskUI taskUI;
    private ClockUI clockUI;

    private bool lastWasCompulsion=false;

    private void Awake()
    {
        Instance = this;

        remainingTasks = new List<TaskList>(allTasks);

        taskUI = GetComponentInChildren<TaskUI>();
        clockUI = GetComponentInChildren<ClockUI>();
    }

    private void Start()
    {
        PickNextTask();
    }

    public void CompleteTask(TaskList completed)
    {
        if(completed != currentTask) return;

        remainingTasks.Remove(completed);

        if (!lastWasCompulsion)
        {
            ClockManager.Instance.timesSinceLastTask=0;
        }

        DataCollector.Instance.gameDatas[DataCollector.Instance.currentData].anxietyAfter=AnxietyManager.Instance.currentAnxiety;
        DataCollector.Instance.gameDatas[DataCollector.Instance.currentData].timeAtConclusion=Time.time;

        DataCollector.Instance.currentData++;

        PickNextTask();
    }

    public void PickNextTask()
    {
        if (remainingTasks.Count == 0)
        {
            currentTask = TaskList.None;
            EndingManager.Instance.EndGame(EndingList.TasksCompleted);
            return;
        }

        GameData gameData = new GameData
        {
            timeStarted = Time.time,
            anxietyBefore=AnxietyManager.Instance.currentAnxiety,
            numberOfMistakes=0
        };

        if(!lastWasCompulsion)
        {
            int roll = Random.Range(0,2);
            int anxietyLevel = (int)AnxietyManager.Instance.currentState;
            Debug.Log(roll + " " + anxietyLevel);
            if(roll+anxietyLevel>=3)
            {
                PickNextCompulsion();

                gameData.taskName=currentTask.ToString();
                DataCollector.Instance.gameDatas.Add(gameData);

                return;
            }
        }

        int rand = Random.Range(0,remainingTasks.Count);
        currentTask=remainingTasks[rand];

        taskUI.UpdateTaskText(remainingTasks.Count);
        HighlightManager.Instance.HighlightTask(currentTask);
        lastWasCompulsion=false;

        gameData.taskName=currentTask.ToString();
        DataCollector.Instance.gameDatas.Add(gameData);
    }

    private void PickNextCompulsion()
    {
        int rand = Random.Range(0,allCompulsions.Count);
        currentTask=allCompulsions[rand];

        taskUI.UpdateTaskText(remainingTasks.Count);
        HighlightManager.Instance.HighlightTask(currentTask);
        lastWasCompulsion=true;
    }

    public void PickEndingTask()
    {
        currentTask=allEndings[(int)EndingManager.Instance.currentEnding];

        taskUI.UpdateTaskText(0);
        HighlightManager.Instance.HighlightTask(currentTask);
    }
}
