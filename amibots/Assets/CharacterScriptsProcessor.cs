using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScriptsProcessor : MonoBehaviour {

    Character character;
    public float timer;
    private int activeSequence;
    bool characterFalled;
    public List<UIFunctionLine> uiFunctionLines;

    void Start () {
        character = GetComponent<Character>();
        activeSequence = 1;
    }
    void Update()
    {
        if (uiFunctionLines.Count > 0)
             Compute(uiFunctionLines);
    }
    public void ProcessScript(AmiScript _script) {
        Reset();
        uiFunctionLines.Clear();
        int sequenceID = 1;
        foreach (AmiClass amiClass in _script.classes)
        {
            UIFunctionLine uiFunctionLine = new UIFunctionLine();
            uiFunctionLine.SetFunction(amiClass);
            uiFunctionLine.sequenceID = sequenceID;
            uiFunctionLine.functionVarButtons = new List<UIFunctionVarButton>();
            uiFunctionLine.function = new AmiFunction();
            uiFunctionLine.function.variables = new List<AmiClass>();
            foreach (AmiArgument amiArgument in amiClass.argumentValues)
            {
                AmiClass newClass = new AmiClass();
                newClass.className = amiArgument.value;
                newClass.type = amiArgument.argument;
                uiFunctionLine.function.variables.Add(newClass);    
            }
            uiFunctionLine.done = false;
            uiFunctionLine.amiClass = amiClass;
            uiFunctionLines.Add(uiFunctionLine);
            sequenceID++;
        }
    }
    
    public void Compute(List<UIFunctionLine> functions)
    {
        timer += Time.deltaTime;
      
        bool someFunctionIsActive = false;
        foreach (UIFunctionLine uifl in functions)
        {
            if (uifl.sequenceID <= activeSequence && !uifl.done)
            {
                someFunctionIsActive = true;

                UpdateFuncion(uifl);

                if (!characterFalled)
                {
                    character.UpdateFunctions(uifl.function.variables, timer);
                }
            }
        }
        if (!someFunctionIsActive)
        {
            functions.Clear();
            Reset();
        }
    }
    void UpdateFuncion(UIFunctionLine uifl)
    {
        float duration = (float)GetFunctionDuration(uifl.function);
        if (duration>0 && duration >= timer)
        {
            uifl.SetFilled((timer / duration));
        }
        else
        {            
			uifl.IsReady ();
            if (uifl.function.value == "Parallel") {
                // activeSequence++;
                timer = 0;
            } else {				
                if(uifl.parallelOf != null)
                { 
					if (IsParallelSequenceDone (uifl.parallelOf)) {
						activeSequence++;
						timer = 0;
					}	
				} else {
                    activeSequence++;		
					timer = 0;
				}
			}          
        }
    }
	bool IsParallelSequenceDone(UIFunctionLine parallelLine)
	{
	//	foreach (UIFunctionLine uifl in parallelSequence.GetComponentsInChildren<UIFunctionLine>()) {
			if (!parallelLine.done) {
				return false;
		//	}
		}
		return true;		
	}
    float GetFunctionDuration(AmiFunction function)
    {
        foreach (AmiClass amiClass in function.variables)
        {
            if (amiClass.type == AmiClass.types.WAIT)
                return float.Parse(amiClass.className)/100;
            if (amiClass.type == AmiClass.types.TIME)
            {
                return float.Parse(amiClass.className) / 100;
            }
        }
        return 0;
    }
    void Reset()
    {
        uiFunctionLines.Clear();
        timer = 0;
        activeSequence = 1;
        character.Reset();
        if (character.isEditorCharacter)
        {
            Events.OnUIFunctionChangeIconColor(Color.green);
            Events.OnDebug(false);
        }
    }
}
