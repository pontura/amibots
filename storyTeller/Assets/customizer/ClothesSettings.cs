using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ClothesSettings : MonoBehaviour {

	public types type;
	public enum types
	{
		CLOTHES,
		BOOTS,
		LEGS,
		SKIN
	}
	public List<string> legs;
	public  List<string> clothes;
	public  List<string> boots;
	public  List<string>skins;

	void Start () {
		clothes = LoadArray("customizer/CLOTHES/top/");
		boots = LoadArray("customizer/BOOTS/");
		skins = LoadArray("customizer/SKIN/");
		legs = LoadArray("customizer/CLOTHES/bottom/");
	}
	string lastClothAdded = "";
	List<string> LoadArray(string path)
    {
		Object[] arr = Resources.LoadAll(path, typeof(Texture2D));
		List<string> arrayNew= new List<string> ();
		for(int a=0; a<arr.Length; a++) {
			string[] arrString = arr[a].name.Split("_"[0]);
			string n = arrString [0] + "_" + arrString [1] + "_" + arrString [2];
			if (n != lastClothAdded) {
				lastClothAdded = n;
				arrayNew.Add(n);
			}
		}
		return arrayNew;
    }
}
