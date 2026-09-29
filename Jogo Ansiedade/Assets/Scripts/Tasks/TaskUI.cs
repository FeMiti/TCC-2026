using UnityEngine;
using TMPro;

public class TaskUI : MonoBehaviour
{

    [SerializeField] private TMP_Text numberRemainingTasksText;

    [SerializeField] private TMP_Text currentTaskText; 

    public void UpdateTaskText(int remaining)
    {
        numberRemainingTasksText.text="Tarefas Restantes: " + remaining;
        
        currentTaskText.text=TaskManager.Instance.currentTask.ToString();
    }
}
