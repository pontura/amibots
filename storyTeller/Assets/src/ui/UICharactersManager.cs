using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharactersManager : MonoBehaviour
{

    public UIButton uiButton_to_instantiate;
    public Transform container;

    void Start()
    {
        Events.OnUIButtonClicked += OnUIButtonClicked;
    }
    void OnUIButtonClicked(UIButton uiButton)
    {
        if (uiButton.type == UIButton.types.CHARACTER)
        {
            if (uiButton.id == 0)
            {
                AddNewButton();
            }
            else
            {
                Select(uiButton);
            }
        }
    }
    int id = 1;
    UIButton uiButton;
    void AddNewButton()
    {
        uiButton = Instantiate(uiButton_to_instantiate);
        uiButton.transform.SetParent(container);
        uiButton.Init(id, "Char" + id);
        AddCharacter(id);
        id++;
    }
    void AddCharacter(int id)
    {
        Events.AddCharacter(id);
        Invoke("SelectDelayed", 0.2f);
    }
    void SelectDelayed()
    {
        Select(uiButton);
    }
    void Select(UIButton thisUIButton)
    {
        Events.OnSelectCharacterID(thisUIButton.id);
        foreach (UIButton uiButton in container.GetComponentsInChildren<UIButton>())
        {
            if (uiButton == thisUIButton)
                uiButton.Select(true);
            else
                uiButton.Select(false);
        }
    }
}
