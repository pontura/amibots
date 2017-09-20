using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomizer : MonoBehaviour {

    public SpriteRenderer HeadAsset;
    public parts part;
    public enum parts
    {
        HEAD
    }
	void Start () {

    }
	public void OnChangeExpression(string value)
	{
		print("OnChangeExpression " + value);
		HeadAsset.sprite = Resources.Load("character/expressions/" + value, typeof(Sprite)) as Sprite;
	}
	void OnCustomize(parts part, string newImage) {
      //  print("ok" + newImage);
      //  HeadAsset.sprite = Resources.Load("character/expressions/" + newImage, typeof(Sprite)) as Sprite;
    }
}
