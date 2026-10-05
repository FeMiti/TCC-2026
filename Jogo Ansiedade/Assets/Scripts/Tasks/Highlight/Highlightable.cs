using Unity.VisualScripting;
using UnityEngine;

public class Highlightable : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;

    private Renderer[] objectRenderer;
    private Material[][] originalMaterials;

    private void Awake()
    {
        objectRenderer = GetComponentsInChildren<Renderer>();

        originalMaterials = new Material[objectRenderer.Length][];

        for(int i=0;i<objectRenderer.Length;i++)
        {
            originalMaterials[i] = objectRenderer[i].sharedMaterials;
        }
    }

    public void EnableHighlight()
    {
        for(int j = 0; j < objectRenderer.Length; j++)
        {    
            Material[] materials = new Material[originalMaterials[j].Length+1];

            for(int i = 0; i < originalMaterials[j].Length; i++)
            {
                materials[i]=originalMaterials[j][i];
            }

            materials[materials.Length-1]=outlineMaterial;

            objectRenderer[j].sharedMaterials=materials;
        }
    }

    public void DisableHighlight()
    {
        for(int i = 0; i < objectRenderer.Length; i++)
        {
            objectRenderer[i].sharedMaterials=originalMaterials[i];
        }
    }
}
