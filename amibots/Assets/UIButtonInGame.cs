using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonInGame : MonoBehaviour {

    Animation anim;
    public Image colorizable;
    public AmiScript script;
	public GameObject selectedIcon;
	UIButtonsInGame uiButtonsInGame;

	public void Init(UIButtonsInGame uiButtonsInGame, AmiScript _script)
    {
		this.uiButtonsInGame = uiButtonsInGame;
        this.script = _script;
    }
	public void Edit()
	{
		Events.OnEditScript(script);
	}
    public void Clicked()
    {
		uiButtonsInGame.SetSelected (this);
    }
    void Start()
    {
        anim = GetComponent<Animation>();
    }
    public void Activate()
    {
        anim.Play("late");
    }
    public void ChangeColor(Color _color)
    {
        colorizable.color = _color;
    }
	public void SetSelected(bool isOn)
	{
		selectedIcon.SetActive (isOn);
	}
	public bool IsSelected()
	{
		if (selectedIcon.activeSelf)
			return true;
		return false;

	}
}
