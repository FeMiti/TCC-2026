using UnityEngine;
using TMPro;

public class TaskUI : MonoBehaviour
{

    [SerializeField] private TMP_Text numberRemainingTasksText;

    [SerializeField] private TMP_Text currentTaskText; 

    public void UpdateTaskText(int remaining)
    {
        numberRemainingTasksText.text="Tarefas Restantes: " + remaining;

        if(remaining!=0)
        {
            currentTaskText.text=TaskManager.Instance.currentTask.ToString();
        }
        else
        {
            currentTaskText.text="Todas as tarefas finalizadas!";
        }
    }
}
