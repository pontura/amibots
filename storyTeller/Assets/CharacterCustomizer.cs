using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomizer : MonoBehaviour {

    public SpriteRenderer HeadAsset;
    public parts part;
	public string value;

    public enum parts
    {
        HEAD
    }
	void Awake () {
		value = Settings.expressions.h0.ToString ();
    }
	public void OnChangeExpression(string value)
	{
		this.value = value;
		HeadAsset.sprite = Resources.Load("character/expressions/" + value, typeof(Sprite)) as Sprite;
	}
	void OnCustomize(parts part, string newImage) {
      //  print("ok" + newImage);
      //  HeadAsset.sprite = Resources.Load("character/expressions/" + newImage, typeof(Sprite)) as Sprite;
    }
}
