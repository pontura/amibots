using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersManager : MonoBehaviour
{

    public Character character_to_initialize;
    public Transform container;

    void Start()
    {
        Events.AddCharacter += AddCharacter;
    }
    void AddCharacter(int id)
    {
        Character character = Instantiate(character_to_initialize);
        character.transform.SetParent(container);
    }
}
