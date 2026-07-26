using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SepararRoupas : MonoBehaviour, IMinigame
{
    private MinigameManager minigameManager;

    [SerializeField] private GameObject clothPrefab;

    private int numberOfErrors;

    private bool canSort=false;
    private bool canCheck=false;

    [Header("Clothes List")]
    [SerializeField] private List<ClothData> allClothes;
    private List<ClothData> remainingClothes = new();
    private List<ClothData> brotherPile = new();
    private List<ClothData> sisterPile = new();
    private ClothData currentCloth;

    [Header("Remaining Pile")]
    [SerializeField] private TMP_Text clothName;

    [SerializeField] private Transform brotherContainer;
    [SerializeField] private Transform sisterContainer;

    public void Setup(MinigameManager manager)
    {
        minigameManager=manager;

        remainingClothes=allClothes;
    }

    private void ShuffleClothes(List<ClothData> clothes)
    {
        for(int i = clothes.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0,i+1);

            (clothes[i], clothes[randomIndex]) = (clothes[randomIndex], clothes[i]);
        }
    }

    private void Start()
    {
        StartMinigame();
    }

    private void StartMinigame()
    {
        ShuffleClothes(remainingClothes);
        canSort=true;
        PickNextCloth();
    }

    private void PickNextCloth()
    {
        if (remainingClothes.Count > 0)
        {
            currentCloth=remainingClothes[0];
            clothName.text=currentCloth.name;
        }
        else
        {
            clothName.text="Todas as roupas separadas";
        }
    }

    public void PlaceOnBrotherPile()
    {
        if (!canSort)
        {
            return;
        }

        remainingClothes.RemoveAt(0);
        brotherPile.Add(currentCloth);
        currentCloth.currentPile=ClothPile.Brother;

        GameObject obj = Instantiate(clothPrefab, brotherContainer);
        obj.GetComponent<ClothUI>().Setup(currentCloth);

        if (remainingClothes.Count <= 0)
        {
            canSort=false;
            canCheck=true;
        }

        PickNextCloth();
    }

    public void PlaceOnSisterPile()
    {
        if (!canSort)
        {
            return;
        }

        remainingClothes.RemoveAt(0);
        sisterPile.Add(currentCloth);
        currentCloth.currentPile=ClothPile.Sister;

        GameObject obj = Instantiate(clothPrefab, sisterContainer);
        obj.GetComponent<ClothUI>().Setup(currentCloth);

        if (remainingClothes.Count <= 0)
        {
            canSort=false;
            canCheck=true;
        }

        PickNextCloth();
    }

    public void CheckClothes()
    {
        if (!canCheck)
        {
            return;
        }

        canCheck=false;

        numberOfErrors=0;

        CheckPile(brotherPile);
        CheckPile(sisterPile);

        if (numberOfErrors <= 0)
        {
            AnxietyManager.Instance.DecreaseAnxiety(10);
            FinishMinigame();
        }
        else
        {
            StartCoroutine(FailMinigame());
        }
    }

    private void CheckPile(List<ClothData> pile)
    {
        for(int i = pile.Count - 1; i >= 0; i--)
        {
            ClothData cloth = pile[i];

            if (cloth.correctPile != cloth.currentPile)
            {
                cloth.currentPile=ClothPile.None;
                remainingClothes.Add(cloth);
                pile.RemoveAt(i);
                numberOfErrors++;
            }
        }
    }

    public IEnumerator FailMinigame()
    {
        AnxietyManager.Instance.IncreaseAnxiety(5);

        yield return StartCoroutine(BlinkRed());

        foreach(ClothUI ui in brotherContainer.GetComponentsInChildren<ClothUI>())
        {
            if (ui.Data.currentPile != ui.Data.correctPile)
            {
                Destroy(ui.gameObject);
            }
        }
        foreach(ClothUI ui in sisterContainer.GetComponentsInChildren<ClothUI>())
        {
            if (ui.Data.currentPile != ui.Data.correctPile)
            {
                Destroy(ui.gameObject);
            }
        }

        StartMinigame();
    }

    public IEnumerator BlinkRed()
    {
        foreach(ClothUI ui in brotherContainer.GetComponentsInChildren<ClothUI>())
        {
            if (ui.Data.currentPile != ui.Data.correctPile)
            {
                ui.TurnRed();
            }
        }
        foreach(ClothUI ui in sisterContainer.GetComponentsInChildren<ClothUI>())
        {
            if (ui.Data.currentPile != ui.Data.correctPile)
            {
                ui.TurnRed();
            }
        }

        yield return new WaitForSecondsRealtime(1f);

        foreach(ClothUI ui in brotherContainer.GetComponentsInChildren<ClothUI>())
        {
            if (ui.Data.currentPile != ui.Data.correctPile)
            {
                ui.TurnWhite();
            }
        }
        foreach(ClothUI ui in sisterContainer.GetComponentsInChildren<ClothUI>())
        {
            if (ui.Data.currentPile != ui.Data.correctPile)
            {
                ui.TurnWhite();
            }
        }
    }

    public void FinishMinigame()
    {
        TaskManager.Instance.CompleteTask(TaskList.SepararRoupas);
        minigameManager.CloseMinigame();
    }
}
