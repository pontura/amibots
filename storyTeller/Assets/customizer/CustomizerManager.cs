using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CustomizerManager : MonoBehaviour {

	public CustomizerButton button;
	public Transform buttonsContainer;

	void Start () {
		Events.OnCustomizeButtonClicked += OnCustomizeButtonClicked;
		AddButtons (ClothesSettings.types.CLOTHES);
	}
	void AddButtons(ClothesSettings.types type)
	{
		Utils.RemoveAllChildsIn (buttonsContainer);
		List<string> arr = new List<string> ();
		switch (type) {
		case ClothesSettings.types.CLOTHES:
			arr = Data.Instance.GetComponent<ClothesSettings> ().clothes;
			break;
		case ClothesSettings.types.BOOTS:
			arr = Data.Instance.GetComponent<ClothesSettings> ().boots;
			break;
		case ClothesSettings.types.LEGS:
			arr = Data.Instance.GetComponent<ClothesSettings> ().legs;
			break;
		case ClothesSettings.types.HAIR:
			arr = Data.Instance.GetComponent<ClothesSettings> ().hairs;
			break;
        case ClothesSettings.types.COLOR:
            for (int a = 0; a < Data.Instance.GetComponent<ClothesSettings>().colors.Count; a++)
                arr.Add( a.ToString());
            break;
        }
		if (arr == null)
			return;
		foreach (string name in arr) {
			CustomizerButton cb = Instantiate (button);
			cb.transform.SetParent (buttonsContainer);
			cb.transform.localScale = Vector2.one;
			cb.isMenu = false;
			cb.Init (type, name);
		}
	}
	void OnCustomizeButtonClicked(bool isMenu, ClothesSettings.types type, string value)
    {
		if (isMenu)
			AddButtons (type);
		else {
			CharacterCustomizer.parts parts;
			switch (type) {
			case ClothesSettings.types.CLOTHES:
				parts = CharacterCustomizer.parts.CLOTHES;
				break;
			case ClothesSettings.types.LEGS:
				parts = CharacterCustomizer.parts.LEGS;
				break;
			case ClothesSettings.types.HAIR:
				parts = CharacterCustomizer.parts.HAIRS;
				break;
			default:
				parts = CharacterCustomizer.parts.COLORS;
				break;
			}
			Events.OnCustomize (-1, parts, value);

			if(World.Instance.charactersManager.selectedCharacter != null)
				Events.OnCustomize (World.Instance.charactersManager.selectedCharacter.id, parts, value);
		}
    }
}
