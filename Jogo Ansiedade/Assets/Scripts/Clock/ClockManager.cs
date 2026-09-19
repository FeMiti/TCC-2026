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

                yield return new WaitForSecondsRealtime(timeUntilChange);

                currentMinute+=10;
            }

            currentMinute=initialMinute;
            currentHour++;
        }

        clockUI.EndClock();
    }
}

