using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class CharacterCustomizer : MonoBehaviour {
    
    public parts part;
	public string value;

    public string legs = "";
    public string clothes = "";

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
		value = Settings.expressions.angry.ToString ();
		character = GetComponent<Character> ();
		Events.OnCustomize += OnCustomize;
    }
    void OnDestroy () {
		Events.OnCustomize -= OnCustomize;
	}
	public void OnChangeExpression(string value)
	{
		this.value = value;
        character.actions.SetExpression(value);
		//HeadAsset.sprite = Resources.Load("character/expressions/" + value, typeof(Sprite)) as Sprite;
	}
    public void Init()
    {
        if (character.id == -1) return;

        OnCustomize(character.id, parts.LEGS, "ropa_bottom_1");
        OnCustomize(character.id, parts.CLOTHES, "ropa_top_1");
    }
    public void OnDupliacteCustomization(CharacterCustomizer newCustomizer)
    {
        if (newCustomizer.legs != "")
            OnCustomize(-1, parts.LEGS, newCustomizer.legs);
        if (newCustomizer.clothes != "")
            OnCustomize(-1, parts.CLOTHES, newCustomizer.clothes);

    }
	void OnCustomize(int characterID, parts part, string newImage) {

		if(characterID == character.id)
		{

            if (part == parts.LEGS) {
                legs = newImage;
                string leg1_image = "customizer/CLOTHES/bottom/" + newImage + "_leg_a";
                string leg2_image = "customizer/CLOTHES/bottom/" + newImage + "_leg_b";
                character.avatar.leg1.spriteMesh = Resources.Load (leg1_image, typeof(SpriteMesh)) as SpriteMesh;
                character.avatar.leg2.spriteMesh = Resources.Load (leg2_image, typeof(SpriteMesh)) as SpriteMesh;

                string body_bottom_image = "customizer/CLOTHES/bottom/" + newImage + "_hips";
                character.avatar.body_bottom.spriteMesh = Resources.Load (body_bottom_image, typeof(SpriteMesh)) as SpriteMesh;
				return;
			} else if (part == parts.CLOTHES) {
                clothes = newImage;
                string arm_a_image = "customizer/CLOTHES/top/" + newImage + "_arm_a";
                string arm_b_image = "customizer/CLOTHES/top/" + newImage + "_arm_b";
                character.avatar.arm1.spriteMesh = Resources.Load (arm_a_image, typeof(SpriteMesh)) as SpriteMesh;
                character.avatar.arm2.spriteMesh = Resources.Load (arm_b_image, typeof(SpriteMesh)) as SpriteMesh;

                string body_up_a_image = "customizer/CLOTHES/top/" + newImage + "_torax_a";
                string body_up_b_image = "customizer/CLOTHES/top/" + newImage + "_torax_b";
                character.avatar.body_up_a.spriteMesh = Resources.Load (body_up_a_image, typeof(SpriteMesh)) as SpriteMesh;
                character.avatar.body_up_b.spriteMesh = Resources.Load(body_up_b_image, typeof(SpriteMesh)) as SpriteMesh;
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
