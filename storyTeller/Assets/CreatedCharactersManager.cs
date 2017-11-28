using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatedCharactersManager : MonoBehaviour {

    public Transform container;
    public CharacterCreated characterCreated_to_instantiate;
    public List<CharacterCreated> all;

    void Start () {

        foreach (CharacterData data in Data.Instance.charactersCreated.all)
            AddNewCharacter(data);

    }
    public CharacterCreated AddNewCharacter(CharacterData data)
    {
        CharacterCreated characterCreated = Instantiate(characterCreated_to_instantiate);
        characterCreated.transform.SetParent(container);
        characterCreated.transform.localPosition = new Vector3(data.id*10, 0, 0);
        characterCreated.Init(data);        
        all.Add(characterCreated);
        return characterCreated;
    }
}
