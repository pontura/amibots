using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScreenTitle : MonoBehaviour {

	public GameObject panel;
	public InputField field;
	public Text title;
	public Text cancelField;
	bool intro;

	void Start () {
		intro = true;
		panel.SetActive (false);
		cancelField.text = "No, thanks!";
	}
	public void Open()
	{
		panel.SetActive (true);
	}
	public void Ok () 
	{
		if (field.text.Length < 1)
			return;
		Events.AddKeyFrameScreenTitle (field.text, 0);
		if (intro)
			GetComponent<UISceneSelector> ().Open (true);

		Close ();
	}
	public void Cancel () 
	{
		if (intro)
			GetComponent<UISceneSelector> ().Open (true);

		Close ();
	}
	void Close()
	{
		intro = false;
		panel.SetActive (false);
		cancelField.text = "Cancel";
	}
}
