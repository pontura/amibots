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
            amiScript = characterScripts.CreateScriptFromLines(new AmiScript(), allFunctions);
            character.scriptsProcessor.ProcessScript(amiScript);
        }        
    }
	void CatchFunctions()
	{		
		int sequenceID = -1;
		foreach (UIFunctionLine uifl in functionsLineContainer.GetComponentsInChildren<UIFunctionLine>()) {

            
            uifl.ResetFilled();
            allFunctions.Add(uifl);           

             if (uifl.isParallel) {
             //   print("is a function in a parallel" + sequenceID);
            } else {
				//print ("is a free function " + sequenceID);
				sequenceID++;
			}
            uifl.sequenceID = sequenceID;


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
    void Update()
    {
        if (isPlaying)
        {
            float timer = character.scriptsProcessor.timer;
            int activeSequence = character.scriptsProcessor.activeSequence;
            foreach (UIFunctionLine line in allFunctions)
            {
                if (line.sequenceID == activeSequence)
                {
                    line.SetFilled(timer / GetFunctionDuration(line.function.variables));
                }
                else
                {
                    line.ResetFilled();
                }
            }
         //   print(timer + " " + activeSequence + "    " + allFunctions.Count);
        }
    }
    float GetFunctionDuration(List<AmiClass> variables)
    {
        foreach (AmiClass amiClass in variables)
        {
            if (amiClass.type == AmiClass.types.WAIT)
                return float.Parse(amiClass.className) / 100;
            if (amiClass.type == AmiClass.types.TIME)
            {
                return float.Parse(amiClass.className) / 100;
            }
        }
        return 0;
    }

}