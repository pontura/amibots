using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class CharacterCustomizer : MonoBehaviour {
    
    public parts part;
	public string value;
	Character character;

    public enum parts
    {
        HEAD,
		CLOTHES,
		LEGS,
		FOOTS,
		SKIN
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

	//	print ("___________: part: " + part + " new image: " + newImage);

		if(characterID == character.id)
		{
			//print (characterID + " : " + part);

			if (part == parts.LEGS) {
				string part_url2_a = "customizer/CLOTHES/bottom/" + newImage + "_leg_a";
                string part_url2_b = "customizer/CLOTHES/bottom/" + newImage + "_leg_b";
                character.avatar.leg1.spriteMesh = Resources.Load (part_url2_a, typeof(SpriteMesh)) as SpriteMesh;
                character.avatar.leg2.spriteMesh = Resources.Load (part_url2_b, typeof(SpriteMesh)) as SpriteMesh;

				string part2_url = "customizer/CLOTHES/bottom/" + newImage + "_hips";
                character.avatar.body_bottom.spriteMesh = Resources.Load (part2_url, typeof(SpriteMesh)) as SpriteMesh;
				//brazo2.spriteMesh = mesh2;
				return;
			} else if (part == parts.CLOTHES) {
				string part3_url_a = "customizer/CLOTHES/top/" + newImage + "_arm_a";
                string part3_url_b= "customizer/CLOTHES/top/" + newImage + "_arm_b";
                character.avatar.arm1.spriteMesh = Resources.Load (part3_url_a, typeof(SpriteMesh)) as SpriteMesh;
                character.avatar.arm2.spriteMesh = Resources.Load (part3_url_b, typeof(SpriteMesh)) as SpriteMesh;

                string part_url_a = "customizer/CLOTHES/top/" + newImage + "_torax_a";
                string part_url_b = "customizer/CLOTHES/top/" + newImage + "_torax_b";
                character.avatar.body_up_a.spriteMesh = Resources.Load (part_url_a, typeof(SpriteMesh)) as SpriteMesh;
                character.avatar.body_up_b.spriteMesh = Resources.Load(part_url_b, typeof(SpriteMesh)) as SpriteMesh;
                //brazo2.spriteMesh = mesh2;
                return;
			}

			string part_url = "customizer/" + part.ToString() + "/" + newImage;

			//brazo.sprite = Resources.Load(part_url, typeof(Sprite)) as Sprite;
			return;


			SpriteRenderer thisPart = null;
			switch (part) {
			case parts.FOOTS:
				//thisPart = shoesContainer;
				break;
			}
			if (thisPart == null) {
				print ("CUSTOMIZADOR: No existe: " + part_url);
				return;
			}
			print ("carga:  " + part_url);
			//brazo.sprite = Resources.Load(part_url, typeof(Sprite)) as Sprite;
		}
    }
}
