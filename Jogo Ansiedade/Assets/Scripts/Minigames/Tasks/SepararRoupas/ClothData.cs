using UnityEngine;

[System.Serializable]
public class ClothData
{
    public string name;
    public ClothPile correctPile;
    public ClothPile currentPile=ClothPile.None;
}
