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

    public void EndClock()
    {
        hourText.text="";
        minutesText.text="";
        twoDotsText.text="";
        finalText.text="Time's UP!";
    }
}
