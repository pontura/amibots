using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLineCharacter : MonoBehaviour {

	public Shader matShader;
	public Image image;
	public CharacterData data;
	public Transform container;

	void Start () {
		
	}
	public void Init( CharacterData data)
	{
		this.data = data;
		Material mat = new Material(matShader);
		image.material = mat;
		CharacterCreated characterCreated = World.Instance.createdCharactersManager.GetCharacterCreatedByID(data.id);
		image.material.mainTexture = characterCreated.cam.targetTexture;
		image.enabled = false;
		image.enabled = true;
	}
}
