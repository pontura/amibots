using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class CharacterCustomizer : MonoBehaviour {
    
    public parts part;
	public string value;

    public string legs = "";
    public string clothes = "";
    public string hairs = "";
    public string colors = "";

    Character character;

    public enum parts
    {
        HEAD,
		CLOTHES,
		LEGS,
		FOOTS,
		HAIRS,
        COLORS
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
        OnCustomize(character.id, parts.HAIRS, "hair_1");
        OnCustomize(character.id, parts.COLORS, "4");
    }
    public void OnDupliacteCustomization(CharacterCustomizer newCustomizer)
    {
        if (newCustomizer.legs != "")
            OnCustomize(-1, parts.LEGS, newCustomizer.legs);
        if (newCustomizer.clothes != "")
            OnCustomize(-1, parts.CLOTHES, newCustomizer.clothes);
        if (newCustomizer.hairs != "")
            OnCustomize(-1, parts.HAIRS, newCustomizer.hairs);
        if (newCustomizer.colors != "")
            OnCustomize(-1, parts.COLORS, newCustomizer.colors);

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
            else if (part == parts.HAIRS)
            {
                hairs = newImage;
                //return;
                string hair_a_image = "customizer/HAIR/" + newImage + "_a";
                string hair_b_image = "customizer/HAIR/" + newImage + "_b";
                character.avatar.hair_up.sprite = Resources.Load(hair_a_image, typeof(Sprite)) as Sprite;
                character.avatar.hair_down.sprite = Resources.Load(hair_b_image, typeof(Sprite)) as Sprite;
                return;
            }
            else if (part == parts.COLORS)
            {
                colors = newImage;
                Color color = Data.Instance.clothesSettings.colors[int.Parse(colors)];
                character.avatar.hair_up.color = color;
                character.avatar.hair_down.color = color;
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
