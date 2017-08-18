using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : MonoBehaviour {

	public Actionbutton WalkButton;
	public GameObject UiEditing;
	public GameObject UiPlaying;
	public GameObject tootltipActionEmpty;

	void Start () {
		UiEditing.SetActive (false);
		UiPlaying.SetActive (true);
		Events.OnUIFunctionChangeIconColor += OnUIFunctionChangeIconColor;
	}
	Color _color;
	void OnUIFunctionChangeIconColor(Color color)
	{
		_color = color;
		WalkButton.ChangeColor (color);
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			if (_color == Color.green)
				return;
			WalkButton.Activate ();
			tootltipActionEmpty.SetActive (true);
			CancelInvoke ();
			Invoke ("ResetTooltip", 2);
		}
	}
	void ResetTooltip()
	{
		tootltipActionEmpty.SetActive (false);
	}
	public void ButtonPressed()
	{
		UiEditing.SetActive (true);
		UiPlaying.SetActive (false);
	}
	public void BakToGame()
	{
		UiEditing.SetActive (false);
		UiPlaying.SetActive (true);
	}
}
