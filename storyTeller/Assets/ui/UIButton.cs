using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour {

    public Text field;
    public int id;
    public types type;
    public string value;
    public Image thumb;

    public enum types
    {
        CHARACTER,
        EXPRESION,
        ACTION,
        CHAT,
        REAL_ACTION,
        REAL_EXPRESION,
		CHARACTER_EDITOR,
		SCENEOBJECT_MENU,
		SCENEOBJECT,
		REC_TOGGLE,
		PLAY_TOGGLE,
		REWIND,
		FAST_FORWARD,
		REW_TO_CHECKPOINT,
		FAST_TO_CHECKPOINT
    }
	public void Init(int id, string text) {
        field.text = text;
        this.id = id;
        this.value = text;
    }
    public void SetThumb(string url)
    {
        //field.text = "";
        field.enabled = false;
        thumb.enabled = true;
        thumb.sprite = Resources.Load(url, typeof(Sprite)) as Sprite;
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
