using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterButton : MonoBehaviour {

    public Shader matShader;
    public Image image;
    public Image ok_image;
    public bool isOn;
    public CharacterCreated characterCreated;
    UICharacterSelector uiCharacterSelector;


    public void Init (UICharacterSelector uiCharacterSelector, CharacterCreated characterCreated) {
        this.uiCharacterSelector = uiCharacterSelector;
        this.characterCreated = characterCreated;
        image.material = new Material(matShader);
        image.material.mainTexture = characterCreated.cam.targetTexture;
        isOn = true;
        SetActive();
    }
    public void Edit()
    {
        uiCharacterSelector.Edit(characterCreated.character.data);
    }
    public void Toggle()
    {
        isOn = !isOn;
        SetActive();
        uiCharacterSelector.Refresh();
    }
    public void SetActive()
    {
        if (isOn)
            ok_image.enabled = true;
        else
            ok_image.enabled = false;
    }
}
