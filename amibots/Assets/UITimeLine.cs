using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITimeLine : MonoBehaviour {

	public GameObject functionsLineContainer;

	public List<UIFunctionLine> allFunctions;

	bool isPlaying;
	public float timer;

	public Character character;

	void Start () {
		Events.OnDebug += OnDebug;
        Events.CreateNewEmptyScript += CreateNewEmptyScript;
    }
    void CreateNewEmptyScript(AmiScript.categories c, string s)
    {
        allFunctions.Clear();
        Utils.RemoveAllChildsIn(functionsLineContainer.transform);
    }

    void Update()
	{
		if (!isPlaying)
			return;
		if (allFunctions.Count == 0)
			return;
        character.scriptsProcessor.Compute(allFunctions);
	}
	void OnDebug(bool _isPlaying)
	{
		this.isPlaying = _isPlaying;

		timer = 0;

		allFunctions.Clear ();
        if (isPlaying)
        {
            Events.OnUIFunctionChangeIconColor(Color.grey);
            CatchFunctions();
        }
	}
	void CatchFunctions()
	{		
		int sequenceID = 0;
		foreach (UIFunctionLine uifl in functionsLineContainer.GetComponentsInChildren<UIFunctionLine>()) {            

            if (uifl.function.type == AmiClass.types.SIMPLE_ACTION && uifl.function.value == "Parallel") {
                sequenceID++;
            } else if (uifl.transform.parent.gameObject.GetComponent<UIFunctionSlot> () != null) {
			//	print ("is child of a sequence...");
				// is child of a sequence...
			} else {
				//print ("is a free function");
				sequenceID++;
			}

            uifl.sequenceID = sequenceID;
            uifl.done = false;
            allFunctions.Add(uifl);
        }
	}
    public void SaveFunction()
    {
        CatchFunctions();
        UIEditing uiEditting = GetComponent<UIEditing>();

        if(uiEditting.edittingScript != null)
            Events.UpdateScript(uiEditting.edittingScript, uiEditting.category, uiEditting.scriptName, allFunctions);
        else
            Events.SaveNewScript(uiEditting.category, uiEditting.scriptName, allFunctions);

        allFunctions.Clear();
        Events.OnUIChangeState(UIGame.states.PLAYING);
    }
	
}