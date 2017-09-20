﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour {

    public Text field;
    public int id;
    public types type;
    public string value;

    public enum types
    {
        CHARACTER,
        EXPRESION,
        ACTION,
        CHAT,
        REAL_ACTION,
        REAL_EXPRESION
    }
	public void Init(int id, string text) {
        field.text = text;
        this.id = id;
        this.value = text;
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
