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
		HAIR,
        COLOR
	}
	public  List<string> legs;
	public  List<string> clothes;
	public  List<string> boots;
	public  List<string>skins;
    public  List<string> hairs;
    public List<Color> colors;

    void Start () {
		clothes = LoadArray("customizer/CLOTHES/top/",3);
		boots = LoadArray("customizer/BOOTS/", 3);
		legs = LoadArray("customizer/CLOTHES/bottom/", 3);
        hairs = LoadArray("customizer/HAIR/",2);
    }
	string lastClothAdded = "";
	List<string> LoadArray(string path, int itemsToSplit)
    {
		Object[] arr = Resources.LoadAll(path, typeof(Texture2D));
		List<string> arrayNew= new List<string> ();
		for(int a=0; a<arr.Length; a++) {
			string[] arrString = arr[a].name.Split("_"[0]);
            string n;
            if(itemsToSplit==3)
                n = arrString [0] + "_" + arrString [1] + "_" + arrString [2];
            else
                n = arrString[0] + "_" + arrString[1];
            if (n != lastClothAdded) {
				lastClothAdded = n;
				arrayNew.Add(n);
			}
		}
		return arrayNew;
    }
}
