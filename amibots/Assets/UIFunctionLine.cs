using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFunctionLine : MonoBehaviour {

	public Text field;
	public UIFunctionVarButton functionVarButton;
	AmiClass amiClass;
	public Transform container;

	public AmiFunction function;
	public List<UIFunctionVarButton> functionVarButtons;
	public Image filledImage;

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
		
		function = new AmiFunction ();
		function.value = amiClass.className;
		function.variables = new List<AmiClass> ();

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
			newfunctionVarButton.transform.SetParent (container);
			newfunctionVarButton.transform.localScale = Vector3.one;
			List<AmiClass> all =  Data.Instance.amiClasses.GetClassesByArg (arg);

			AmiClass newClass = new AmiClass ();
			newClass.className = all [0].className;
			newClass.type = arg;

			function.variables.Add (newClass);

			newfunctionVarButton.Init ( this,  arg, id);

			string sentence =  Data.Instance.amiClasses.GetSentenceFor (newClass.className, arg);
			newfunctionVarButton.SetValue (sentence);

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
		AmiClass amiClass= function.variables[lastIArgSelected];
		amiClass.className = animClass.className;

		UIFunctionVarButton functionVarButton= functionVarButtons [lastIArgSelected];
		functionVarButton.SetValue(animClass.className);
	}
	public void SetFilled(float fillAmount)
	{
		filledImage.fillAmount = fillAmount;
	}
	public void ResetFilled()
	{
		filledImage.fillAmount = 0;
	}
}
