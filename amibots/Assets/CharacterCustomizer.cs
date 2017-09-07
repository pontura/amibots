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
        Events.OnCustomize += OnCustomize;
    }
	
	void OnCustomize(parts part, string newImage) {
        print("ok" + newImage);
        HeadAsset.sprite = Resources.Load("heads/" + newImage, typeof(Sprite)) as Sprite;
    }
}
