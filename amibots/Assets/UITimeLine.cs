using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITimeLine : MonoBehaviour {

	public GameObject functionsLineContainer;
    public CharacterScripts characterScripts;
	public List<UIFunctionLine> allFunctions;

	bool isPlaying;

	public Character character;
    AmiScript amiScript;


    void Start () {
		Events.OnDebug += OnDebug;
        Events.CreateNewEmptyScript += CreateNewEmptyScript;
    }
    void CreateNewEmptyScript(AmiScript.categories c, string s)
    {
		Reset ();
    }
	void Reset()
	{
		allFunctions.Clear();
		Utils.RemoveAllChildsIn(functionsLineContainer.transform);
        amiScript = null;

    }
	void OnDebug(bool _isPlaying)
	{
		this.isPlaying = _isPlaying;
        
		allFunctions.Clear ();
        if (isPlaying)
        {
            Events.OnUIFunctionChangeIconColor(Color.grey);
            CatchFunctions();
        }
        amiScript = characterScripts.CreateScriptFromLines(new AmiScript(), allFunctions);
        character.scriptsProcessor.ProcessScript(amiScript);
    }
	void CatchFunctions()
	{		
		int sequenceID = 0;
		foreach (UIFunctionLine uifl in functionsLineContainer.GetComponentsInChildren<UIFunctionLine>()) {            

            if (uifl.function.type == AmiClass.types.SIMPLE_ACTION && uifl.function.value == "Parallel") {
                sequenceID++;
				//print ("is a Parallel");
			} else if (uifl.parallelOf != null) {
				//print ("is child of a sequence...");
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
        if(uiEditting.edittingScript != null && uiEditting.edittingScript.classes != null)
            Events.UpdateScript(uiEditting.edittingScript, uiEditting.category, uiEditting.scriptName, allFunctions);
        else
            Events.SaveNewScript(uiEditting.category, uiEditting.scriptName, allFunctions);

        allFunctions.Clear();
        Events.OnUIChangeState(UIGame.states.PLAYING);
		Reset ();
    }
	
}