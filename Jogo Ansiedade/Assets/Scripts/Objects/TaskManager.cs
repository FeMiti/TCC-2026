using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;

public class TaskManager : MonoBehaviour
{

    public static TaskManager Instance;

    [SerializeField] private List<TaskList> allTasks;

    private List<TaskList> remainingTasks = new List<TaskList>();

    public TaskList currentTask{get; private set;}

    private void Awake()
    {
        Instance = this;

        remainingTasks = new List<TaskList>(allTasks);

        PickNextTask();
    }

    public void CompleteTask(TaskList completed)
    {
        if(completed != currentTask) return;

        Debug.Log("Tarefa completada: " + completed);

        remainingTasks.Remove(completed);

        PickNextTask();
    }

    private void PickNextTask()
    {
        if (remainingTasks.Count == 0)
        {
            currentTask = TaskList.None;
            Debug.Log("Todas as tarefas completas!");
            return;
        }

        int rand = Random.Range(0,remainingTasks.Count);
        currentTask=remainingTasks[rand];

        Debug.Log("Nova tarefa: " + currentTask);
    }
}
