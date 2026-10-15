using UnityEngine;

public class PlacedFurnitureUI : MonoBehaviour
{
    public string FurnitureId { get; private set; }

    public void SetFurnitureId(string id)
    {
        FurnitureId = id;
    }
}