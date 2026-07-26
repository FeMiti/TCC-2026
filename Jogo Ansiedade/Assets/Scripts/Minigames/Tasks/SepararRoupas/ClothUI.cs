using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClothUI : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private RawImage background;

    public ClothData Data {get; private set;}

    public void Setup(ClothData data)
    {
        Data=data;

        text.text=data.name;
    }

    public void TurnRed()
    {
        background.color=Color.red;
    }

    public void TurnWhite()
    {
        background.color=Color.white;
    }
}
