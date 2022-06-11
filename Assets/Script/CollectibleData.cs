using UnityEngine;

[CreateAssetMenu(fileName = "Collectible", menuName = "ScriptableObjects/Collectible", order = 2)]
public class CollectibleData : ScriptableObject
{
	public string collectibleName;
	public Sprite sprite;
	public Vector2 position;
}
