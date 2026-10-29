using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RespiracaoCaixa : MonoBehaviour, IMinigame
{

    private MinigameManager minigameManager;

    [SerializeField] private GameObject mainButtons;
    [SerializeField] private TMP_Text startText;
    [SerializeField] private List<GameObject> instructions;
    [SerializeField] private List<Slider> breathingSliders;

    private int currentInstruction=-1;

    private float holdStartTime=0f;
    private bool isHolding=false;
    private float holdDuration=2f;

    private bool minigameOn=false;
    private bool firstTime=true;

    private RelaxData relaxData;

    public void Setup(MinigameManager manager)
    {
        minigameManager=manager;
    }

    private void Update()
    {
        if(!minigameOn)
            return;

        KeyCode requiredKey = currentInstruction switch
        {
          0 => KeyCode.W,
          1 => KeyCode.D,
          2 => KeyCode.S,
          3 => KeyCode.A,
          _ => KeyCode.None  
        };

        if (Input.GetKeyDown(requiredKey))
        {
            holdStartTime=Time.time;
            isHolding=true;
        }

        if(Input.GetKey(requiredKey) && isHolding)
        {
            float elapsed = Time.time - holdStartTime;
            float progress = Mathf.Clamp01(elapsed/holdDuration);

            if (progress < 1f)
            {
                breathingSliders[currentInstruction].value=progress;
            }

            else if (progress >= 1f)
            {
                breathingSliders[currentInstruction].value=1f;

                instructions[currentInstruction].SetActive(false);
                currentInstruction++;
                isHolding=false;
                if (currentInstruction < 4)  
                    instructions[currentInstruction].SetActive(true);
                else
                    EndMinigame();
            }
        }

        if (Input.GetKeyUp(requiredKey) && isHolding)
        {
            relaxData.numberOfMistakes++;
            breathingSliders[currentInstruction].value=0f;
            isHolding=false;
        }
    }

    public void StartMinigame()
    {

        relaxData = new RelaxData
        {
            taskNumber=DataCollector.Instance.currentData,
            timeStarted=Time.time,
            anxiety = AnxietyManager.Instance.currentAnxiety,
            numberOfMistakes=0
        };

        mainButtons.SetActive(false);
        if (!firstTime)
        {
            for(int i = 0; i < 4; i++)
            {
                breathingSliders[i].value=0f;
            }
        }
        currentInstruction=0;
        instructions[currentInstruction].SetActive(true);
        minigameOn=true;
    }

    private void EndMinigame()
    {
        relaxData.timeAtConclusion=Time.time;
        DataCollector.Instance.relaxDatas.Add(relaxData);

        minigameOn=false;
        AnxietyManager.Instance.DecreaseAnxiety(25);
        if (firstTime)
        {
            startText.text="REINICIAR";
            firstTime=false;
        }
        mainButtons.SetActive(true);
    }

    public void FinishMinigame()
    {
        minigameManager.CloseMinigame();
    }
}
