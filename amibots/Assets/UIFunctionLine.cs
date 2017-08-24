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
    public Image bgImage;

    public int sequenceID;
	public bool done;
    public GameObject childs;
    float _initialHeight;

	void Start()
	{   
        Events.OnPopupClose += OnPopupClose;
        Events.DragEnd += DragEnd;
        _initialHeight = GetComponent<RectTransform>().sizeDelta.y;
    }
	void OnPopupClose()
	{
		Events.OnUIClassSelected -= OnUIClassSelected;        
        lastArgSelected = 0;
	}
    void OnDestroy()
    {
        Events.OnUIClassSelected -= OnUIClassSelected;
        Events.DragEnd -= DragEnd;
    }
    void DragEnd()
    {
        if(childs.activeSelf)
            Invoke("RecalculateHeightByContainer", 0.05f);
    }
    void RecalculateHeightByContainer()
    {
        float _y = _initialHeight +( childs.GetComponentsInChildren<UIFunctionLine>().Length * _initialHeight);
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _y);
    }
    public void Init (AmiClass amiClass) {

        if (amiClass.className == "Parallel")
        {
            childs.SetActive(true);
            bgImage.enabled = false;
        }
        else
        {
            childs.SetActive(false);
            bgImage.enabled = true;
        }

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
	int lastIArgSelectedID;
	AmiClass.types lastArgSelected;
	public void OnArgumentSelected(AmiClass.types arg, int id)
	{
		lastIArgSelectedID = id;
		lastArgSelected = arg;
		Events.OnUIClassSelected += OnUIClassSelected;
		Events.OnPopup( arg);
	}
	void OnUIClassSelected(AmiClass animClass)
	{
		AmiClass amiClass= function.variables[lastIArgSelectedID];
		amiClass.className = animClass.className;

		UIFunctionVarButton functionVarButton= functionVarButtons [lastIArgSelectedID];
		string sentence =  Data.Instance.amiClasses.GetSentenceFor (animClass.className, lastArgSelected);

		functionVarButton.SetValue(sentence);
	}
	public void SetFilled(float fillAmount)
	{
		filledImage.fillAmount = fillAmount;
	}
	public void IsReady()
	{
		done = true;
		ResetFilled ();
	}
	public void ResetFilled()
	{
		filledImage.fillAmount = 0;
	}
	public void PointerDown()
	{
		Events.DragStartGameObject (gameObject);
	}
	public void PointerUp()
	{
		Events.DragEnd ();
	}
}
