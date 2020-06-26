using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum CharacterType
    {
        Player,
        AI
    };

    [SerializeField] CharacterType characterTypes = CharacterType.AI;


    public CharacterType characterType => characterTypes;
}
