using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ClothesSettings : MonoBehaviour {

	public types type;
	public enum types
	{
		CLOTHES,
		BOOTS,
		SKIN
	}
	public string[] clothes;
	public string[] boots;
	public string[] skins;

	void Start () {
		clothes = LoadArray("customizer/CLOTHES/");
		boots = LoadArray("customizer/BOOTS/");
		skins = LoadArray("customizer/SKIN/");
	}

	string[] LoadArray(string path)
    {
		Object[] arr = Resources.LoadAll(path, typeof(Texture2D));
		string[] newArr = new string[arr.Length];
		for(int a=0; a<arr.Length; a++) {
			newArr[a] = arr[a].name;
		}
		return newArr;
    }
}
