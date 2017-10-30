using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UISubmenu : MonoBehaviour {

    public UIButton uiButton;
    public GameObject panel;
    public Transform container;
	public Animator anim;

	void Start () {
        Reset();
        Events.OnUIButtonClicked += OnUIButtonClicked;
    }
    public void Open(UIButton.types type)
    {
		Time.timeScale = 0;
        Utils.RemoveAllChildsIn(container);
        string[] lists;
        UIButton.types newType;
        switch(type)
        {
            case UIButton.types.ACTION:
                lists = System.Enum.GetNames(typeof(Settings.actions));
                newType = UIButton.types.REAL_ACTION;
                break;
			case UIButton.types.EXPRESION:
				lists = System.Enum.GetNames(typeof(Settings.expressions));
				newType = UIButton.types.REAL_EXPRESION;
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
			newUiButton.transform.localScale = Vector2.one;
            newUiButton.Init(id, buttonString);
            newUiButton.type = newType;
            id++;
        }
        panel.SetActive(true);
        anim.Play("open");
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
                Events.OnCharacterAction( uiButton.value );
                Close();
                break;
            case UIButton.types.REAL_EXPRESION:
				Events.OnChangeExpression( uiButton.value );
                Close();
                break;
        }
    }
    public void Close()
    {
		Time.timeScale = 1;
        anim.Play("close");
    }
    void Reset()
    {
        panel.SetActive(false);
    }

}
