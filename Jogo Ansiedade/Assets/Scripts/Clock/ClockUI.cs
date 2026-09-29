using TMPro;
using UnityEngine;

public class ClockUI : MonoBehaviour
{

    [SerializeField] private TMP_Text hourText;
    [SerializeField] private TMP_Text twoDotsText;
    [SerializeField] private TMP_Text minutesText;
    [SerializeField] private TMP_Text finalText;

    public void UpdateClock()
    {
        hourText.text=ClockManager.Instance.currentHour.ToString();
        if (ClockManager.Instance.currentMinute == 0)
        {
            minutesText.text="00";
        }
        else
        {            
            minutesText.text=ClockManager.Instance.currentMinute.ToString();
        }
    }

    public void EndClock(EndingList ending)
    {
        hourText.text="";
        minutesText.text="";
        twoDotsText.text="";
        switch (ending)
        {
            case EndingList.TasksCompleted:
                finalText.text="All tasks completed!";
                break;
            case EndingList.OutOfTime:
                finalText.text="Time's UP!";
                break;
            case EndingList.PanicAttack:
                finalText.text="...";
                break;
        }
    }
}
