using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BookUI : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private Image bookCover;
    [SerializeField] private RectTransform rectTransform;
    
    private OrdenarLivros minigameLivros;
    public BookData Data {get; private set;}

    public void Setup(BookData data, OrdenarLivros minigame)
    {
        minigameLivros=minigame;

        Data=data;

        title.text=data.name;

        float bookHeight = data.height*50f+350f;
        float bookWidth = data.width*10f+100f;
        rectTransform.sizeDelta = new Vector2(bookWidth,bookHeight);

        switch (data.color)
        {
            case BookColors.Red:
                bookCover.color=Color.red;
                break;
            case BookColors.Orange:
                bookCover.color=Color.orange;
                break;
            case BookColors.Yellow:
                bookCover.color=Color.yellow;
                break;
            case BookColors.Green:
                bookCover.color=Color.green;
                break;
            case BookColors.Blue:
                bookCover.color=Color.blue;
                break;
            case BookColors.Indigo:
                bookCover.color=Color.indigo;
                break;
            case BookColors.Violet:
                bookCover.color=Color.violet;
                break;
        }
    }

    public void OnBookClicked()
    {
        minigameLivros.SelectBook(Data);
    }
}
