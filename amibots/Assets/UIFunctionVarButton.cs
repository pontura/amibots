using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFunctionVarButton : MonoBehaviour {

	public Text field;
	UIFunctionLine line;
	AmiClass.types arg;
	public int id;

	public void Init(UIFunctionLine line,  AmiClass.types arg, int id)
	{
		this.id = id;
		this.arg = arg;
		this.line = line;
	}
	public void SetValue(string value)
	{
		field.text = value;
	}
	public void OnSelected()
	{
		line.OnArgumentSelected (arg, id);
	}
}
