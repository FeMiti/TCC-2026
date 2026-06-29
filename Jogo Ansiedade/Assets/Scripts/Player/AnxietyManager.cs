using UnityEngine;

public class AnxietyManager : MonoBehaviour
{

    private AnxietyUI anxietyUI;

    public static AnxietyManager Instance;

    public static int maxAnxiety=100;

    public int currentAnxiety=0;

    public static int lowAnxietyLimit=40, highAnxietyLimit=70;

    public AnxietyState currentState=AnxietyState.Calm;

    void Awake()
    {
        Instance=this;

        Debug.Log("Ansiedade acordou");

        anxietyUI=GetComponentInChildren<AnxietyUI>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentAnxiety=0;
        currentState=AnxietyState.Calm;
    }

    public void IncreaseAnxiety(int anx)
    {
        currentAnxiety+=anx;

        currentAnxiety=Mathf.Clamp(currentAnxiety,0,maxAnxiety);

        if(currentAnxiety>=highAnxietyLimit && currentState != AnxietyState.Panicking)
        {
            UpdateState(AnxietyState.Panicking);
        }
        else if(currentAnxiety>=lowAnxietyLimit && currentAnxiety<highAnxietyLimit && currentState != AnxietyState.Anxious)
        {
            UpdateState(AnxietyState.Anxious);
        }

        anxietyUI.UpdateAnxietyBar();
    }

    public void DecreaseAnxiety(int anx)
    {
        currentAnxiety-=anx;

        currentAnxiety=Mathf.Clamp(currentAnxiety,0,maxAnxiety);

        if(currentAnxiety<lowAnxietyLimit && currentState != AnxietyState.Calm)
        {
            UpdateState(AnxietyState.Calm);
        }
        else if(currentAnxiety<highAnxietyLimit && currentAnxiety>=lowAnxietyLimit && currentState != AnxietyState.Anxious)
        {
            UpdateState(AnxietyState.Anxious);
        }

        anxietyUI.UpdateAnxietyBar();
    }

    private void UpdateState(AnxietyState nextState)
    {
        currentState=nextState;
    }
}
