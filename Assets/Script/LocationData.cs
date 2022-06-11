using UnityEngine;

[CreateAssetMenu(fileName = "Location", menuName = "ScriptableObjects/Location", order = 1)]
public class LocationData : ScriptableObject
{
    public CollectibleData collectible;
    public string locationName;
    public Sprite background;
}