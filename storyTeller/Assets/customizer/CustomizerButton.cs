using UnityEngine;
using System.Collections;

public class CustomizerButton : MonoBehaviour
{
	public bool isMenu;
	public ClothesSettings.types type;
	public string value;

	public void Init(ClothesSettings.types type, string value)
	{
		this.type = type;
		this.value = value;
	}
    public void Clicked()
    {
		Events.OnCustomizeButtonClicked (isMenu, type, value);
    }
}
