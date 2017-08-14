using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFunctionLine : MonoBehaviour {

	public Text field;
	public UIFunctionVarButton functionVarButton;
	AmiClass amiClass;

	public List<AmiClass> classes;
	public List<UIFunctionVarButton> functionVarButtons;

	void Start()
	{
		Events.OnPopupClose += OnPopupClose;
	}
	void OnPopupClose()
	{
		Events.OnUIClassSelected -= OnUIClassSelected;
		lastIArgSelected = 0;
	}
	public void Init (AmiClass amiClass) {
		field.text = amiClass.className;
		this.amiClass = amiClass;
		AddArguments ();

	}
	void AddArguments()
	{
		int id = 0;
		foreach(AmiClass.types arg in amiClass.arguments)
		{
			UIFunctionVarButton newfunctionVarButton = Instantiate (functionVarButton);
			newfunctionVarButton.transform.SetParent (transform);
			newfunctionVarButton.transform.localScale = Vector3.one;
			List<AmiClass> all =  Data.Instance.amiClasses.GetClassesByArg (arg);

			AmiClass newClass = new AmiClass ();
			newClass.className = all [0].className;
			newClass.type = arg;
			classes.Add (newClass);

			newfunctionVarButton.Init ( this,  arg, id);
			newfunctionVarButton.SetValue (newClass.className);

			functionVarButtons.Add (newfunctionVarButton);

			id++;
		}
	}
	int lastIArgSelected;
	public void OnArgumentSelected(AmiClass.types arg, int id)
	{
		lastIArgSelected = id;
		Events.OnUIClassSelected += OnUIClassSelected;
		Events.OnPopup( arg);
	}
	void OnUIClassSelected(AmiClass animClass)
	{
		AmiClass amiClass= classes [lastIArgSelected];
		amiClass.className = animClass.className;

		UIFunctionVarButton functionVarButton= functionVarButtons [lastIArgSelected];
		functionVarButton.SetValue(animClass.className);
	}
}
