using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnxietyUI : MonoBehaviour
{

    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider.maxValue=AnxietyManager.maxAnxiety;
        slider.value=AnxietyManager.Instance.currentAnxiety;
        text.text=AnxietyManager.Instance.currentAnxiety.ToString();
    }
    
    public void UpdateAnxietyBar()
    {
        slider.value=AnxietyManager.Instance.currentAnxiety;
        text.text=AnxietyManager.Instance.currentAnxiety.ToString();
    }
}
