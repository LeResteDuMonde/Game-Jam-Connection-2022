using UnityEngine;

[CreateAssetMenu(fileName = "Collectible", menuName = "ScriptableObjects/Collectible", order = 2)]
public class CollectibleData : ScriptableObject
{
	public string collectibleName;
	public Sprite sprite;
	public Sprite alternativeSprite;
	public Vector2 position;
	public Vector2 scale;
    public string transition;
}
