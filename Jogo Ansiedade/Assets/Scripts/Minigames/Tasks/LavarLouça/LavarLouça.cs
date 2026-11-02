using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LavarLouça : MonoBehaviour, IMinigame
{
    private MinigameManager minigameManager;

    [SerializeField] private Transform dishPlace;

    [SerializeField] private List<DishData> dishes;
    private DishData currentDish;

    private Button[] dirtButtons;

    private int numberOfDirt=0;
    private int cleanedDirt=0;

    [SerializeField] private Slider timeBar;
    private float maxTime=4f;

    private float timeBarInterval=0.4f;

    private float currentTime;

    private bool canStart=false;

    public void Setup(MinigameManager manager)
    {
        minigameManager=manager;

        Debug.Log("=== SETUP LAVAR LOUÇA ===");
        Debug.Log($"Instância do Manager: {manager.GetInstanceID()}");
        Debug.Log($"Quantidade de louças disponíveis: {manager.loucas.Count}");
        Debug.Log($"Conteúdo de loucas: {string.Join(", ", manager.loucas)}");
        Debug.Log($"Quantidade de dishes: {dishes.Count}");

        if (manager.loucas.Count == 0)
        {
            Debug.LogError("A lista loucas está vazia!");
            return;
        }

        int rand = Random.Range(0,manager.loucas.Count);
        int dishIndex = manager.loucas[rand];

        Debug.Log($"Posição aleatória: {rand}");
        Debug.Log($"Índice da louça selecionada: {dishIndex}");

        if (dishIndex < 0 || dishIndex >= dishes.Count)
        {
            Debug.LogError($"Índice inválido para dishes: {dishIndex}");
            return;
        }

        currentDish = dishes[dishIndex];

        manager.loucas.RemoveAt(rand);
    }

    private void Start()
    {
        GameObject obj = Instantiate(currentDish.dishPrefab,dishPlace);
        dirtButtons = obj.GetComponentsInChildren<Button>();

        foreach(Button dirt in dirtButtons)
        {
            dirt.onClick.RemoveAllListeners();
            dirt.onClick.AddListener(() => CleanDirt(dirt));    
        }


        canStart=true;
    }

    public void StartMinigame()
    {
        if(!canStart) return;

        canStart=false;
        numberOfDirt=Random.Range(5,7);
        cleanedDirt=0;
        ShuffleDirt();
        for(int i = 0; i < numberOfDirt; i++)
        {
            dirtButtons[i].image.enabled=true;
            dirtButtons[i].interactable=true;
        }

        currentTime=maxTime;
        timeBar.maxValue=maxTime;
        timeBar.value=currentTime;
    
        StartCoroutine(RunMinigame());
    }

    public IEnumerator RunMinigame()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(timeBarInterval);

            currentTime-=timeBarInterval;

            timeBar.value=currentTime;
        }

        if (cleanedDirt==numberOfDirt)
        {
            FinishMinigame();
        }
        else
        {
            FailMinigame();
        }
    }

    private void ShuffleDirt()
    {
        for(int i = dirtButtons.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0,i+1);
            (dirtButtons[i],dirtButtons[randomIndex])=(dirtButtons[randomIndex],dirtButtons[i]);
        }
    }

    public void CleanDirt(Button dirt)
    {
        dirt.interactable=false;
        dirt.image.enabled=false;

        cleanedDirt++;
    }

    private void FailMinigame()
    {
        AnxietyManager.Instance.IncreaseAnxiety(15);

        DataCollector.Instance.gameDatas[DataCollector.Instance.currentData].numberOfMistakes++;

        for(int i = 0; i < numberOfDirt; i++)
        {
            if (dirtButtons[i].interactable)
            {
                dirtButtons[i].interactable=false;
                dirtButtons[i].image.enabled=false;
            }
        }

        canStart=true;
    }

    public void FinishMinigame()
    {
        AnxietyManager.Instance.DecreaseAnxiety(0);
        TaskManager.Instance.CompleteTask(TaskList.LavarLouça);
        minigameManager.CloseMinigame();
    }
}
