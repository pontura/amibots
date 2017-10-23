using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICharactersManager : MonoBehaviour
{
	public UIButton characterButton;
	public GameObject panel;
    public UIButton uiButton_to_instantiate;
    public Transform container;

    void Start()
    {
		SetActive (true);
        Events.OnUIButtonClicked += OnUIButtonClicked;
    }
    void OnUIButtonClicked(UIButton uiButton)
	{
		if (uiButton.type == UIButton.types.CHARACTER_EDITOR) {
			SetActive (true);
		} else
		if (uiButton.type == UIButton.types.SCENE_EDITOR)
		{
				SetActive (false);
		} else
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
	void SetActive(bool isActive)
	{
		if (!isActive) {
			characterButton.gameObject.SetActive (true);
			panel.SetActive (false);
		} else {
			characterButton.gameObject.SetActive (false);
			panel.SetActive (true);
		}
	}
    int id = 1;
    UIButton uiButton;
    void AddNewButton()
    {
        uiButton = Instantiate(uiButton_to_instantiate);
        uiButton.transform.SetParent(container);
		uiButton.transform.localScale = Vector2.one;
		uiButton.GetComponent<RectTransform> ().sizeDelta = new Vector2 (uiButton.GetComponent<RectTransform> ().sizeDelta.x, 37.7f);
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
