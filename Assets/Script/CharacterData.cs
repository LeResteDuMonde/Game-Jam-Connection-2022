using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character", order = 1)]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public RuntimeAnimatorController animatorController;
}

