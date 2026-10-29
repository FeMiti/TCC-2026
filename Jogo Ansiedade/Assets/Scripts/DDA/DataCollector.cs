using System.Collections.Generic;
using UnityEngine;

public class DataCollector : MonoBehaviour
{
    public static DataCollector Instance;
    public List<GameData> gameDatas = new List<GameData>();
    public int currentData=0;
    public EndingList ending;
    public List<RelaxData> relaxDatas = new List<RelaxData>();

    private void Awake()
    {
        Instance=this;
    }
}
