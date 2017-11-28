using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterButton : MonoBehaviour {

    public Shader matShader;
    public Image image;
    CharacterCreated characterCreated;

	public void Init (UICharacterSelector uiCharacterSelector, CharacterCreated characterCreated) {
        this.characterCreated = characterCreated;
        image.material = new Material(matShader);
        image.material.mainTexture = characterCreated.cam.targetTexture;
        print(characterCreated.character.data.id);

    }
}
