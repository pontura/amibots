using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatedCharactersManager : MonoBehaviour {

    public Transform container;
    public CharacterCreated characterCreated_to_instantiate;
    public List<CharacterCreated> all;
    public List<int> selectedIds;

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
    public void SetSelectedIds(List<int> arr)
    {
        selectedIds = arr;
    }
    public List<CharacterData> GetActiveCharacters()
    {
        List<CharacterData> arr = new List<CharacterData>();
        foreach (CharacterCreated cd in all)
        {
            foreach (int id in selectedIds)
            {
                print(id + "      id: " + cd.character.data.id + " a:: " + all.Count);
                if (id == cd.character.data.id)
                {
                    arr.Add(cd.character.data);
                }
            }
        }
        return arr;
    }
    public CharacterCreated GetCharacterCreatedByID(int id)
    {
        foreach (CharacterCreated cd in all)
        {
            if (id == cd.character.data.id)
            {
               return cd;
            }
        }
        return null;
    }
}
