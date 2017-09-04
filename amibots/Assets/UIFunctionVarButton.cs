using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFunctionVarButton : MonoBehaviour {

	public Text field;
	UIFunctionLine line;
    public AmiClass.types arg;
	public int id;
    public string value;

	public void Init(UIFunctionLine line,  AmiClass.types arg, int id)
	{
		this.id = id;
		this.arg = arg;
		this.line = line;
	}
	public void SetValue(string className, AmiClass.types type)
	{
        this.value = className;
        string sentence = Data.Instance.amiClasses.GetSentenceFor(className, arg);
        field.text = sentence;
	}
	public void OnSelected()
	{
		line.OnArgumentSelected (arg, id);
	}
    public string GetValue()
    {
        return value;
    }
}
