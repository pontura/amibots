using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomizer : MonoBehaviour {

    public SpriteRenderer HeadAsset;
    public parts part;
	public string value;
	Character character;
    public enum parts
    {
        HEAD,
		CLOTHES,
		FOOTS
    }
	void Awake () {
		value = Settings.expressions.h0.ToString ();
		character = GetComponent<Character> ();
		Events.OnCustomize += OnCustomize;
    }
	void OnDestroy () {
		Events.OnCustomize -= OnCustomize;
	}
	public void OnChangeExpression(string value)
	{
		this.value = value;
		HeadAsset.sprite = Resources.Load("character/expressions/" + value, typeof(Sprite)) as Sprite;
	}
	void OnCustomize(int characterID, parts part, string newImage) {
		
		if(characterID == character.id)
		{
			string part_url = "customizer/" + part.ToString() + "/" + newImage;
			print ("part_url:  " + part_url);
			HeadAsset.sprite = Resources.Load(part_url, typeof(Sprite)) as Sprite;
		}
    }
}
