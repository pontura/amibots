using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharacterSelector : MonoBehaviour
{
    public GameObject panel;
    public CharacterButton button;
    public Transform container;
    bool isNewScene;
    public CreatedCharactersManager createdCharactersManager;

    void Start()
    {
        Invoke("Open", 0.5f);
    }
    int id = 0;
    public void Open()
    {
        panel.SetActive(true);
        // Utils.RemoveAllChildsIn(container);
        foreach(CharacterCreated characterCreated in createdCharactersManager.all)
        {
            AddNewCharacterCreated(characterCreated);             
        }
    }
    public void AddNewCharacterCreated(CharacterCreated characterCreated)
    {
        CharacterButton b = Instantiate(button);
        b.transform.SetParent(container);
        b.transform.localScale = Vector2.one;
        b.Init(this, characterCreated);
        id++;
    }
    public void Add()
    {
        GetComponent<UiCustomizer>().CreateNew();
    }
    public void SetSelected(int sceneID, int backgroundID)
    {
        print("isNewScene " + isNewScene + " id: " + sceneID);
        panel.SetActive(false);
    }
    public void Done()
    {
        panel.SetActive(false);
        GetComponent<UISceneSelector>().Open(true);
    }
}
