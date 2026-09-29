using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighlightManager : MonoBehaviour
{
    [SerializeField] private List<TaskHighlightData> taskHighlights;

    [SerializeField] private Material outlineMaterial;

    public static HighlightManager Instance;

    private Highlightable currentHighlight;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if(currentHighlight==null || PauseMenu.Instance.isPaused) return;

        float alpha = Mathf.Lerp(0f,0.3f,(Mathf.Sin(Time.time*3f)+1f)/2f);

        Color cor = outlineMaterial.GetColor("_OutlineColor");
        cor.a=alpha;

        outlineMaterial.SetColor("_OutlineColor",cor);
    }

    public void HighlightTask(TaskList task)
    {
        if (currentHighlight != null)
        {
            currentHighlight.DisableHighlight();
            currentHighlight=null;
        }

        foreach(TaskHighlightData data in taskHighlights)
        {
            if (data.task == task)
            {
                currentHighlight=data.target;
                currentHighlight.EnableHighlight();
                return;
            }
        }
    }

    public void ClearHighlight()
    {
        if (currentHighlight != null)
        {
            currentHighlight.DisableHighlight();
            currentHighlight=null;
        }
    }

    public void ChangeHighlightColor(AnxietyState state)
    {
        Color cor=outlineMaterial.GetColor("_OutlineColor");

        switch (state)
        {
            case AnxietyState.Calm:
                cor=new Color(0,1,0,cor.a); //verde
                break;
            
            case AnxietyState.Anxious:
                cor=new Color(1,1,0,cor.a); //amarelo
                break;

            case AnxietyState.Panicking:
                cor=new Color(1,0,0,cor.a); //vermelho
                break;
        }

        outlineMaterial.SetColor("_OutlineColor",cor);
    }
}
