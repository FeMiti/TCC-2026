using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnxietyUI : MonoBehaviour
{

    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Image handleImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider.maxValue=AnxietyManager.maxAnxiety;
        slider.value=AnxietyManager.Instance.currentAnxiety;
        UpdateAnxietyBar();
    }
    
    public void UpdateAnxietyBar()
    {
        slider.value=AnxietyManager.Instance.currentAnxiety;
        text.text=AnxietyManager.Instance.currentAnxiety.ToString();

        switch (AnxietyManager.Instance.currentState)
        {
            case AnxietyState.Calm:
                handleImage.color=Color.green;
                break;
            
            case AnxietyState.Anxious:
                handleImage.color=Color.yellow;
                break;

            case AnxietyState.Panicking:
                handleImage.color=Color.red;
                break;
        }
    }
}
