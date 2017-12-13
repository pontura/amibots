using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class CharacterCustomizer : MonoBehaviour {
    
    public parts part;
	public string value;
    public string expression = "norm";
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
        value = "";
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
        this.expression = value;
        //HeadAsset.sprite = Resources.Load("character/expressions/" + value, typeof(Sprite)) as Sprite;
    }
    public void Init()
    {
        OnCustomize(character.data.id, parts.LEGS, character.data.legs);
        OnCustomize(character.data.id, parts.CLOTHES, character.data.clothes);
        OnCustomize(character.data.id, parts.HAIRS, character.data.hairs);
        OnCustomize(character.data.id, parts.COLORS, character.data.colors);
    }
    public void OnDupliacteCustomization(CharacterData data)
    {
        if (data.legs != "")
            OnCustomize(character.data.id, parts.LEGS, data.legs);
        if (data.clothes != "")
            OnCustomize(character.data.id, parts.CLOTHES, data.clothes);
        if (data.hairs != "")
            OnCustomize(character.data.id, parts.HAIRS, data.hairs);
        if (data.colors != "")
            OnCustomize(character.data.id, parts.COLORS, data.colors);

    }
	void OnCustomize(int characterID, parts part, string newImage) {
        if (newImage == "") return;
		if(characterID == character.data.id)
		{

            if (part == parts.LEGS) {
                character.data.legs = newImage;
                string leg1_image = "customizer/CLOTHES/bottom/" + newImage + "_leg_a";
                string leg2_image = "customizer/CLOTHES/bottom/" + newImage + "_leg_b";
                character.avatar.leg1.spriteMesh = Resources.Load (leg1_image, typeof(SpriteMesh)) as SpriteMesh;
                character.avatar.leg2.spriteMesh = Resources.Load (leg2_image, typeof(SpriteMesh)) as SpriteMesh;

                string body_bottom_image = "customizer/CLOTHES/bottom/" + newImage + "_hips";
                character.avatar.body_bottom.spriteMesh = Resources.Load (body_bottom_image, typeof(SpriteMesh)) as SpriteMesh;
				return;
			} else if (part == parts.CLOTHES) {
                character.data.clothes = newImage;
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
                character.data.hairs = newImage;
                //return;
                string hair_a_image = "customizer/HAIR/" + newImage + "_a";
                string hair_b_image = "customizer/HAIR/" + newImage + "_b";
                character.avatar.hair_up.sprite = Resources.Load(hair_a_image, typeof(Sprite)) as Sprite;
                character.avatar.hair_down.sprite = Resources.Load(hair_b_image, typeof(Sprite)) as Sprite;
                return;
            }
            else if (part == parts.COLORS)
            {
                character.data.colors = newImage;
                Color color = Data.Instance.clothesSettings.colors[int.Parse(character.data.colors)];
                character.avatar.hair_up.color = color;
                character.avatar.hair_down.color = color;
                return;
            }

            string part_url = "customizer/" + part.ToString() + "/" + newImage;
		}
    }
}
