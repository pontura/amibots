using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharacterSelector : MonoBehaviour
{
    public Button readyButton;
    public GameObject panel;
    public CharacterButton button;
    public Transform container;
    public Text title;
    bool isEditing;
    public CreatedCharactersManager createdCharactersManager;

    void Start()
    {
        Invoke("Init", 0.5f);
    }
    int id = 0;
    public void Init()
    {
        panel.SetActive(true);
        // Utils.RemoveAllChildsIn(container);
        foreach(CharacterCreated characterCreated in createdCharactersManager.all)
        {
            AddNewCharacterCreated(characterCreated);             
        }
        Refresh();
    }
    public void Open()
    {
        isEditing = true;
        panel.SetActive(true);
    }
    public void AddNewCharacterCreated(CharacterCreated characterCreated)
    {
        CharacterButton b = Instantiate(button);
        b.transform.SetParent(container);
        b.transform.localScale = Vector2.one;
        b.Init(this, characterCreated);
        id++;
        Refresh();
    }
    public void Add()
    {
        GetComponent<UiCustomizer>().CreateNew();
    }
    public void Done()
    {
        List<int> arr = new List<int>();
        foreach (CharacterButton b in container.GetComponentsInChildren<CharacterButton>())
        {
            if (b.isOn)
            {
                arr.Add(b.characterCreated.character.data.id);
            }
        }
        createdCharactersManager.SetSelectedIds(arr);

        panel.SetActive(false);

        if(!isEditing)
            GetComponent<UISceneSelector>().Open(true);
    }
    public void Edit(CharacterData data)
    {
        GetComponent<UiCustomizer>().Open(data);
    }
    public void Refresh()
    {
        int qty = 0;
        foreach(CharacterButton b in container.GetComponentsInChildren<CharacterButton>())
        {
            if (b.isOn)
                qty++;
        }
        readyButton.interactable = true;
        switch (qty)
        {
            case 0:
                title.text = "SELECT CHARACTERS";
                readyButton.interactable = false;
                break;
            case 1:
                title.text = qty + " CHARACTER";
                break;
            default:
                title.text = qty + " CHARACTERS";
                break;
        }
    }
}
