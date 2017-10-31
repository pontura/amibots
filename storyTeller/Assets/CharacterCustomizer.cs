using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class CharacterCustomizer : MonoBehaviour {

	public SpriteMeshInstance brazo;

    public SpriteRenderer visor;
	public SpriteRenderer pupil;
	public SpriteRenderer eyeball;

	public SpriteRenderer shoesContainer;

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
		//HeadAsset.sprite = Resources.Load("character/expressions/" + value, typeof(Sprite)) as Sprite;
	}
	void OnCustomize(int characterID, parts part, string newImage) {

		string p = "customizer/CLOTHES/amibot_brazo_2x";
		brazo.spriteMesh.sprite = Resources.Load(p, typeof(Sprite)) as Sprite;
		return;

		if(characterID == character.id)
		{
			string part_url = "customizer/" + part.ToString() + "/" + newImage;
			print ("CUSTOMIZADOR: No existe: " + part_url);

			SpriteRenderer thisPart = null;
			switch (part) {
			case parts.FOOTS:
				thisPart = shoesContainer;
				break;
			}
			if(thisPart==null) return;

			thisPart.sprite = Resources.Load(part_url, typeof(Sprite)) as Sprite;
		}
    }
}
