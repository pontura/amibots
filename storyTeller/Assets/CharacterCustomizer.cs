using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class CharacterCustomizer : MonoBehaviour {

	public SpriteMeshInstance leg1;
	public SpriteMeshInstance leg2;
	public SpriteMeshInstance body_bottom;

	public SpriteRenderer brazo;

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

	

		if(characterID == character.id)
		{

			string part_url2 = "customizer/CLOTHES/bottom/amibot_pierna_1_mesh";
			leg1.spriteMesh = Resources.Load(part_url2, typeof(SpriteMesh)) as SpriteMesh;
			leg2.spriteMesh = Resources.Load(part_url2, typeof(SpriteMesh)) as SpriteMesh;

			part_url2 = "customizer/CLOTHES/bottom/amibot_bottom_1_hips_mesh";
			body_bottom.spriteMesh = Resources.Load(part_url2, typeof(SpriteMesh)) as SpriteMesh;
			//brazo2.spriteMesh = mesh2;
			return;

			string part_url = "customizer/" + part.ToString() + "/" + newImage;

			brazo.sprite = Resources.Load(part_url, typeof(Sprite)) as Sprite;
			return;


			SpriteRenderer thisPart = null;
			switch (part) {
			case parts.FOOTS:
				thisPart = shoesContainer;
				break;
			}
			if (thisPart == null) {
				print ("CUSTOMIZADOR: No existe: " + part_url);
				return;
			}
			print ("carga:  " + part_url);
			brazo.sprite = Resources.Load(part_url, typeof(Sprite)) as Sprite;
		}
    }
}
