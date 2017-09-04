using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIFunctionLine : MonoBehaviour {

    public UIFunctionLine parallelOf;
    public Text field;
	public UIFunctionVarButton functionVarButton;
	public AmiClass amiClass;
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
        Destroy(GetComponent<EventTrigger>());

        CancelInvoke();
        Events.OnPopupClose -= OnPopupClose;
        Events.OnUIClassSelected -= OnUIClassSelected;
        Events.DragEnd -= DragEnd;
    }
    void DragEnd()
    {
        if(gameObject != null && childs.activeSelf)
            Invoke("RecalculateHeightByContainer", 0.05f);
    }
    void RecalculateHeightByContainer()
    {
        float _y = _initialHeight +( childs.GetComponentsInChildren<UIFunctionLine>().Length * _initialHeight);
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _y);
    }
    public void Init (AmiClass amiClass) {

        this.amiClass = amiClass;
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

        SetFunction(amiClass);
        field.text = amiClass.className;
        AddArguments();
    }
    public void SetFunction(AmiClass amiClass)
    {
        function = new AmiFunction();
        function.value = amiClass.className;
        function.variables = new List<AmiClass>();
        this.amiClass = amiClass;       
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
           
			newfunctionVarButton.SetValue(newClass.className, arg);

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
        Events.OnUIClassSelected -= OnUIClassSelected;
        Events.OnUIClassSelected += OnUIClassSelected;
		Events.OnPopup( arg);
	}
	void OnUIClassSelected(AmiClass animClass)
	{
		AmiClass amiClass= function.variables[lastIArgSelectedID];
		amiClass.className = animClass.className;

		UIFunctionVarButton functionVarButton= functionVarButtons [lastIArgSelectedID];

		functionVarButton.SetValue(animClass.className, lastArgSelected);
	}
	public void SetFilled(float fillAmount)
	{
        if (filledImage == null) return;
		filledImage.fillAmount = fillAmount;
	}
	public void IsReady()
	{
		done = true;
		ResetFilled ();
	}
	public void ResetFilled()
	{
        if(filledImage != null)
		    filledImage.fillAmount = 0;
	}
	public void PointerDown()
	{
        try
        {
            Events.DragStartGameObject(gameObject);
        }
        catch { }
    }
	public void PointerUp()
	{
        try { Events.DragEnd(); } catch { }
            
	}
    public void SetParallelOf(UIFunctionLine _parallelOf)
    {
        this.parallelOf = _parallelOf;
    }
}
