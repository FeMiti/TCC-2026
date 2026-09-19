using UnityEngine;

public class Highlightable : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;

    private Renderer objectRenderer;
    private Material[] originalMaterials;

    private void Awake()
    {
        objectRenderer = GetComponent<Renderer>();

        originalMaterials = objectRenderer.sharedMaterials;
    }

    public void EnableHighlight()
    {
        Material[] materials = new Material[originalMaterials.Length+1];

        for(int i = 0; i < originalMaterials.Length; i++)
        {
            materials[i]=originalMaterials[i];
        }

        materials[materials.Length-1]=outlineMaterial;

        objectRenderer.sharedMaterials=materials;
    }

    public void DisableHighlight()
    {
        objectRenderer.sharedMaterials=originalMaterials;
    }
}
