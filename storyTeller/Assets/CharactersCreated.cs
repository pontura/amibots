using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersCreated : MonoBehaviour {

    public List<CharacterData> all;

    public void CreateNew(CharacterData data)
    {
        all.Add(data);
    }
}
