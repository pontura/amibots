using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScriptsProcessor : MonoBehaviour {

    Character character;
    public float timer;
    private int activeSequence;
    bool characterFalled;
    AmiScript script;

    void Start () {
        character = GetComponent<Character>();
    }
    void Update()
    {
        if (script == null)
            return;
       Compute(script.lines);
    }
    public void ProcessScript(AmiScript _script) {
        Reset();
        this.script = new AmiScript();
        script.lines = new List<UIFunctionLine>();
        foreach (UIFunctionLine uifl in _script.lines)
        {
            uifl.done = false;
            script.lines.Add(uifl);
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
                    character.UpdateFunctions(uifl.function.variables, timer);
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
        if (duration >= timer)
        {
			uifl.SetFilled((timer / duration));
        }
        else
        {
            
			uifl.IsReady ();
			if (uifl.function.value == "Parallel") {
				//no suma sequencia:
			} else {				
				UIFunctionSlot parallelContainer = uifl.transform.parent.GetComponent<UIFunctionSlot> ();
				if (parallelContainer != null) {
					print ("es hijo de seq");
					if (IsParallelSequenceDone (parallelContainer)) {
						print ("es hijo y seqeu esta lista");
						activeSequence++;
						timer = 0;
					}	
				} else {
					activeSequence++;		
					timer = 0;
				}
			}

			Debug.Log ("value de la funcion:  " + uifl.function.value + "    activeSequence: " + activeSequence);            
        }
    }
	bool IsParallelSequenceDone(UIFunctionSlot parallelSequence)
	{
		foreach (UIFunctionLine uifl in parallelSequence.GetComponentsInChildren<UIFunctionLine>()) {
			if (!uifl.done) {
				return false;
			}
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
                return float.Parse(amiClass.className)/100;
        }
        return 0;
    }
    void Reset()
    {
        script = null;
        timer = 0;
        activeSequence = 0;
        character.Reset();
        if (character.isEditorCharacter)
        {
            Events.OnUIFunctionChangeIconColor(Color.green);
            Events.OnDebug(false);
        }
    }
}
