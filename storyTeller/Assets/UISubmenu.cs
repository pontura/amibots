using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UISubmenu : MonoBehaviour {

    public UIButton uiButton;
    public GameObject panel;
    public Transform container;
    public Animation anim;

	void Start () {
        Reset();
        Events.OnUIButtonClicked += OnUIButtonClicked;
    }
    public void Open(UIButton.types type)
    {
        Utils.RemoveAllChildsIn(container);
        string[] lists;
        UIButton.types newType;
        switch(type)
        {
            case UIButton.types.ACTION:
                lists = System.Enum.GetNames(typeof(Settings.actions));
                newType = UIButton.types.REAL_ACTION;
                break;
            default:
                lists = System.Enum.GetNames(typeof(Settings.actions));
                newType = UIButton.types.REAL_EXPRESION;
                break;
        }
        int id = 0;
        foreach (string buttonString in lists)
        {
            UIButton newUiButton = Instantiate(uiButton);
            newUiButton.transform.SetParent(container);
            newUiButton.Init(id, buttonString);
            newUiButton.type = newType;
            id++;
        }
        panel.SetActive(true);
        anim.Play("on");
    }
    void OnUIButtonClicked(UIButton uiButton)
    {
        switch (uiButton.type)
        {
            case UIButton.types.ACTION:
            case UIButton.types.EXPRESION:
                Open(uiButton.type);
                break;
            case UIButton.types.REAL_ACTION:
                Events.OnCharacterAction( GetAction(uiButton.value) );
                Close();
                break;
            case UIButton.types.REAL_EXPRESION:
                Close();
                break;
        }
    }
    Settings.actions GetAction(string value)
    {
        foreach (Settings.actions enumValue in System.Enum.GetValues(typeof(Settings.actions)))
        {
            if (enumValue.ToString() == value)
                return enumValue;
        }
        return Settings.actions.IDLE;
    }
    public void Close()
    {
        anim.Play("off");
    }
    void Reset()
    {
        panel.SetActive(false);
    }

}
