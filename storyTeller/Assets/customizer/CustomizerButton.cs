using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CustomizerButton : MonoBehaviour
{
	public Text field;
	public bool isMenu;
	public ClothesSettings.types type;
	public string value;

	public void Init(ClothesSettings.types type, string value)
	{
		this.type = type;
		this.value = value;
		field.text = value;
	}
    public void Clicked()
    {
		Events.OnCustomizeButtonClicked (isMenu, type, value);
    }
}
