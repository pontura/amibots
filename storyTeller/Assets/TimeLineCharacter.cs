using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLineCharacter : MonoBehaviour {

	public Shader matShader;
	public RawImage image;
	public CharacterData data;

	void Start () {
		
	}
	public void Init( CharacterData data)
	{
		this.data = data;

		CharacterCreated characterCreated = World.Instance.createdCharactersManager.GetCharacterCreatedByID(data.id);
		image.texture = characterCreated.cam.targetTexture;

		image.enabled = false;
		image.enabled = true;
	}
}
