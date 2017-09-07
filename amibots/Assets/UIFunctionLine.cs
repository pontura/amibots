using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIFunctionLine : MonoBehaviour {

    public bool isParallel;
    public Text field;
	public UIFunctionVarButton functionVarButton;
	public AmiClass amiClass;
	public Transform container;

	public AmiFunction function;
	public List<UIFunctionVarButton> functionVarButtons;
	public Image filledImage;
    public Image bgImage;

    public int sequenceID;
	public bool isDone;
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
    public void Init (AmiClass _amiClass) {

		this.amiClass = _amiClass;
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
    }
	void AddArguments()
	{

		int id = 0;
		foreach(AmiArgument arg in amiClass.argumentValues)
		{
            List<AmiClass> all = Data.Instance.amiClasses.GetClassesByArg(arg.type);

            //no tiene argumentos
            if (all.Count == 0) return;

            UIFunctionVarButton newfunctionVarButton = Instantiate (functionVarButton);
			newfunctionVarButton.transform.SetParent (container);
			newfunctionVarButton.transform.localScale = Vector3.one;
			

			AmiClass newClass = new AmiClass ();

			//si el boton es nuevo:
			if (arg.value == null || arg.value == "") {
			//	print ("es nuevo");
               
				    newClass.className = all [0].className;
			} else {
				//print ("estas editando");
				newClass.className = arg.value;
			}

			newClass.type = arg.type;
			function.variables.Add (newClass);
			newfunctionVarButton.Init( this,  arg.type, id);           
			newfunctionVarButton.SetValue(newClass.className, arg.type);
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
        if (fillAmount > 1) fillAmount = 1;
		filledImage.fillAmount = fillAmount;
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
        this.isParallel = true;
    }
}
