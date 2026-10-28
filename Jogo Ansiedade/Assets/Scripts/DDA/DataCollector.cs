using System.Collections.Generic;
using UnityEngine;

public class DataCollector : MonoBehaviour
{
    public static DataCollector Instance;
    public List<GameData> gameDatas = new List<GameData>();
    public int currentData=0;
    public EndingList ending;

    private void Awake()
    {
        Instance=this;
    }
}
