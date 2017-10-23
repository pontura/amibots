using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CustomizerManager : MonoBehaviour {

	public CustomizerButton button;
	public Transform buttonsContainer;

	void Start () {
		Events.OnCustomizeButtonClicked += OnCustomizeButtonClicked;
		AddButtons (ClothesSettings.types.CLOTHES);
	}
	void AddButtons(ClothesSettings.types type)
	{
		string[] arr = null;
		switch (type) {
		case ClothesSettings.types.CLOTHES:
			arr = Data.Instance.GetComponent<ClothesSettings> ().clothes;
			break;
		}
		if (arr == null)
			return;
		foreach (string name in arr) {
			CustomizerButton cb = Instantiate (button);
			cb.transform.SetParent (buttonsContainer);
			cb.transform.localScale = Vector2.one;
			cb.Init (type, name);
		}
	}
	void OnCustomizeButtonClicked(bool isMenu, ClothesSettings.types type, string value)
    {
		Events.OnCustomize (-1, CharacterCustomizer.parts.CLOTHES, value + "_on");
    }
}
