using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public static MinigameManager Instance;

    [SerializeField] private Transform container;

    [Header("Randomizador de Minigames")]
    public List<int> mensagens;
    public List<int> entregas;
    public List<int> tarefas;
    public List<int> loucas;

    [SerializeField] private int numberOfMensagens;
    [SerializeField] private int numberOfPortas;
    [SerializeField] private int numberOfTarefas;
    [SerializeField] private int numberOfLoucas;


    private GameObject currentMinigame;

    public bool onMinigame=false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //Definindo o tamanho
        mensagens=Enumerable.Range(0,numberOfMensagens).ToList();
        entregas=Enumerable.Range(0,numberOfPortas).ToList();
        tarefas=Enumerable.Range(0,numberOfTarefas).ToList();
        loucas=Enumerable.Range(0,numberOfLoucas).ToList();
    }

    public void OpenMinigame(GameObject prefab)
    {
        onMinigame=true;

        if (currentMinigame != null)
        {
            Destroy(currentMinigame);
        }

        currentMinigame=Instantiate(prefab,container);

        var minigame = currentMinigame.GetComponent<IMinigame>();
        if (minigame != null)
        {
            minigame.Setup(this);
        }
        else
        {
            Debug.LogWarning("Sem IMinigame");
        }

        currentMinigame.transform.localPosition=Vector3.zero;
        currentMinigame.transform.localScale=Vector3.one;

        Cursor.lockState=CursorLockMode.None;
        Cursor.visible=true;
    }

    public void CloseMinigame()
    {
        if (currentMinigame != null)
        {
            Destroy(currentMinigame);
        }

        Cursor.lockState=CursorLockMode.Locked;
        Cursor.visible=false;
        onMinigame=false;
    }
}
