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
		Events.NewSceneActive += NewSceneActive;
		Events.AddNewScene += AddNewScene;
    }
	void ResetButtons()
	{
		Utils.RemoveAllChildsIn (container);
	}
	void AddNewScene(int sceneID, int backgroundID)
	{
		ResetButtons ();
	}
	void NewSceneActive(int id)
	{		
		
		ResetButtons ();
		foreach(Character ch in World.Instance.scenesManager.sceneActive.characters)
			AddNewButton(ch.id);

		UIButton uiButton = container.GetComponentInChildren<UIButton> ();
		Select (uiButton);
	}
	int id = 1;
    void OnUIButtonClicked(UIButton uiButton)
	{
		if (uiButton.type == UIButton.types.CHARACTER_EDITOR) {
			SetActive (true);
		} else if (uiButton.type == UIButton.types.SCENEOBJECT_MENU)
		{
				SetActive (false);
		} else
        if (uiButton.type == UIButton.types.CHARACTER)
        {
            if (uiButton.id == 0)
            {
				AddNewButton(id);
				AddCharacter(id);
				id++;
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
   
    UIButton uiButton;
	void AddNewButton(int id)
    {
        uiButton = Instantiate(uiButton_to_instantiate);
        uiButton.transform.SetParent(container);
		uiButton.transform.localScale = Vector2.one;
		uiButton.GetComponent<RectTransform> ().sizeDelta = new Vector2 (uiButton.GetComponent<RectTransform> ().sizeDelta.x, 37.7f);
        uiButton.Init(id, "Char" + id);        
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
