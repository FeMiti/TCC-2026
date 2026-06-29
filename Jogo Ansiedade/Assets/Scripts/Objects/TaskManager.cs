using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;

public class TaskManager : MonoBehaviour
{

    public static TaskManager Instance;

    [SerializeField] private List<TaskList> allTasks;

    [SerializeField] private List<TaskList> allCompulsions;

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

        PickNextTask();
    }

    public void PickNextTask()
    {
        if (remainingTasks.Count == 0)
        {
            currentTask = TaskList.None;
            Debug.Log("Todas as tarefas completas!");
            taskUI.UpdateTaskText(remainingTasks.Count);
            ClockManager.Instance.StopAllCoroutines();
            clockUI.EndClock();
            return;
        }

        if(!lastWasCompulsion && (AnxietyManager.Instance.currentState==AnxietyState.Anxious || AnxietyManager.Instance.currentState == AnxietyState.Panicking))
        {
            int comp = Random.Range(0,2);
            Debug.Log(comp);
            if((comp>1 && AnxietyManager.Instance.currentState==AnxietyState.Anxious) || (comp>=1 && AnxietyManager.Instance.currentState == AnxietyState.Panicking))
            {
                PickNextCompulsion();
                return;
            }
        }

        int rand = Random.Range(0,remainingTasks.Count);
        currentTask=remainingTasks[rand];

        taskUI.UpdateTaskText(remainingTasks.Count);
        lastWasCompulsion=false;
    }

    private void PickNextCompulsion()
    {
        int rand = Random.Range(0,allCompulsions.Count);
        currentTask=allCompulsions[rand];

        taskUI.UpdateTaskText(remainingTasks.Count);
        lastWasCompulsion=true;
    }
}
