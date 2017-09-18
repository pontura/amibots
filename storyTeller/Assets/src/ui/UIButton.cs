using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour {

    public Text field;
    public int id;
    public types type;
    public enum types
    {
        CHARACTER,
        EXPRESION,
        ACTION,
        CHAT
    }
	public void Init(int id, string text) {
        field.text = text;
        this.id = id;
    }
	public void OnSelected()
    {
        Events.OnUIButtonClicked(this);
    }
    public void Select(bool isSelected)
    {
        GetComponent<Button>().interactable = !isSelected;
    }
}
