using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AdaptivePerformance;

public class OrdenarLivros : MonoBehaviour, IMinigame
{
    private MinigameManager minigameManager;

    [SerializeField] private GameObject bookPrefab;
    [SerializeField] private Transform bookshelf;

    private List<BookUI> booksUIs=new();

    [SerializeField] private List<BookData> allBooks;
    private List<BookData> books;
    private BookData firstSelected=null;
    private BookData secondSelected=null;

    private SortCriteria currentCriteria;
    private SortDirection currentDirection;

    [SerializeField] private TMP_Text criteriaText;
    [SerializeField] private TMP_Text directionText;

    private bool canSelectBooks=false;
    private bool canCheck=false;

    public void Setup(MinigameManager manager)
    {
        minigameManager=manager;
        books=allBooks;

        ShuffleBooks();
    }

    private void ShuffleBooks()
    {   
        for(int i = books.Count - 1; i > 0; i--)
        {
            int randomIndex=Random.Range(0,i+1);

            (books[i], books[randomIndex]) = (books[randomIndex], books[i]);
        }
    }

    private void Start()
    {
        CreateBooks();

        currentCriteria=(SortCriteria)Random.Range(0,System.Enum.GetValues(typeof(SortCriteria)).Length);
        currentDirection=(SortDirection)Random.Range(0,2);

        SetInstructions();

        canSelectBooks=true;
        canCheck=true;
    }

    private void CreateBooks()
    {
        for(int i = 0; i < books.Count; i++)
        {   
            GameObject obj = Instantiate(bookPrefab,bookshelf);
            booksUIs.Add(obj.GetComponent<BookUI>());
            booksUIs[i].Setup(books[i], this);
        }
    }

    private void SetInstructions()
    {
        switch (currentCriteria)
        {
            case SortCriteria.Name:
                criteriaText.text="alfabeticamente";
                break;
            
            case SortCriteria.Color:
                criteriaText.text="pela ordem das cores do espectro";
                break;

            case SortCriteria.Height:
                criteriaText.text="por altura";
                break;

            case SortCriteria.Width:
                criteriaText.text="por largura";
                break;
        }

        directionText.text = currentDirection == SortDirection.Ascending ? "do menor para o maior." : "do maior para o menor.";
    }

    private void UpdateBooks()
    {
        for(int i = 0; i < books.Count; i++)
        {   
            booksUIs[i].Setup(books[i], this);
        }
    }

    public void SelectBook(BookData selectedBook)
    {
        if (!canSelectBooks)
        {
            return;
        }

        if (firstSelected == null)
        {
            firstSelected=selectedBook;
        }
        else
        {
            secondSelected=selectedBook;

            canSelectBooks=false;
            canCheck=false;

            SwapBooks();

            firstSelected=null;
            secondSelected=null;
            canCheck=true;
            canSelectBooks=true;
        }
    }

    private void SwapBooks()
    {
        int indexA=books.IndexOf(firstSelected);
        int indexB=books.IndexOf(secondSelected);

        (books[indexA], books[indexB]) = (books[indexB], books[indexA]);

        UpdateBooks();
    }

    public void CheckBooks()
    {
        if (!canCheck)
        {
            return;
        }

        canCheck=false;
        canSelectBooks=false;

        for(int i = 0; i < books.Count - 1; i++)
        {
            if (!IsCorrectOrder(books[i], books[i + 1]))
            {
                FailMinigame();
                canCheck=true;
                canSelectBooks=true;
                return;
            }
        }

        FinishMinigame();
    }

    private bool IsCorrectOrder(BookData a, BookData b)
    {
        int comparission=CompareBooks(a,b);

        if (currentDirection == SortDirection.Ascending)
        {
            return comparission<=0;
        }
        else
        {
            return comparission>=0;
        }
    }

    private int CompareBooks(BookData a, BookData b)
    {
        switch (currentCriteria)
        {
            case SortCriteria.Name:
                return string.Compare(a.name,b.name);
            
            case SortCriteria.Color:
                return a.color.CompareTo(b.color);

            case SortCriteria.Height:
                return a.height.CompareTo(b.height);

            case SortCriteria.Width:
                return a.width.CompareTo(b.width);
        }

        return 0;
    }

    private void FailMinigame()
    {
        AnxietyManager.Instance.IncreaseAnxiety(10);
    }

    public void FinishMinigame()
    {
        minigameManager.CloseMinigame();
        TaskManager.Instance.PickNextTask();
    }
}
