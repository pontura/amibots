using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharactersManager : MonoBehaviour
{
    public Shader matShader;
	public UIButton characterButton;
	public GameObject panel;
    public UIButton uiButton_to_instantiate;
    public Transform container;
    public Image image;

    void Start()
    {
		SetActive (true);
        Events.OnUIButtonClicked += OnUIButtonClicked;
        Events.OnSelectCharacter += OnSelectCharacter;
    }
    void OnSelectCharacter(Character character)
    {
       // Invoke("UpdateThumb", 0.2f);
    //}
    //public void UpdateThumb()
    //{
        Material mat = new Material(matShader);
        image.material = mat;
        CharacterCreated characterCreated = World.Instance.createdCharactersManager.GetCharacterCreatedByID(character.data.id);
        image.material.mainTexture = characterCreated.cam.targetTexture;
        image.enabled = false;
        image.enabled = true;
    }
    void OnUIButtonClicked(UIButton uiButton)
	{
		if (uiButton.type == UIButton.types.CHARACTER_EDITOR) {
			SetActive (true);
		} else if (uiButton.type == UIButton.types.SCENEOBJECT_MENU)
		{
				SetActive (false);
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
   
}
