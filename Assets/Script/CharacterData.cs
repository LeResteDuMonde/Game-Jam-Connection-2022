using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character", order = 1)]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public RuntimeAnimatorController animatorController;
    public string stateMachine;
    public List<CharacterData> connections;
    public List<ConnectionType> connectionsType;

}
