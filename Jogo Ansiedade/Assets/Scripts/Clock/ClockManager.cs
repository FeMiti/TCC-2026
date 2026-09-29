using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ClockManager : MonoBehaviour
{

    public static ClockManager Instance;
    private ClockUI clockUI;

    public static int initialHour=14;
    public static int finalHour=18;
    public static int initialMinute=0;
    public static int finalMinute=60;
    public int currentHour;
    public int currentMinute;

    private float timeUntilChange=10f;

    private int timesUntilProcrastination=2;
    private int timesUntilAnxiety=1;
    public int timesSinceLastTask=0;
    public int currentProcrastination;

    private int anxietyPerProcrastination=10;

    void Awake()
    {
        Instance = this;

        clockUI=GetComponentInChildren<ClockUI>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHour=initialHour;
        currentMinute=initialMinute;
        clockUI.UpdateClock();
        StartCoroutine(RunClock());
    }

    public IEnumerator RunClock()
    {
        while (currentHour < finalHour)
        {
            while (currentMinute < finalMinute)
            {
                clockUI.UpdateClock();

                yield return new WaitForSeconds(timeUntilChange);

                currentMinute+=10;
                timesSinceLastTask++;

                if (timesSinceLastTask >= timesUntilProcrastination)
                {
                    if (timesSinceLastTask == timesUntilProcrastination)
                    {
                        currentProcrastination=timesUntilAnxiety;
                    }
                    
                    currentProcrastination++;

                    if (timesUntilAnxiety <= currentProcrastination)
                    {
                        AnxietyManager.Instance.IncreaseAnxiety(anxietyPerProcrastination);
                        currentProcrastination=0;
                    }
                }
            }

            currentMinute=initialMinute;
            currentHour++;
        }

        EndingManager.Instance.EndGame(EndingList.OutOfTime);
    }
}

