using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScriptsProcessor : MonoBehaviour {

    Character character;
    public float timer;
    public int activeSequence;
    bool characterFalled;
	public AmiScript script;
	bool isProcessing;

    void Start () {
        character = GetComponent<Character>();
        activeSequence = 0;
    }
    void Update()
    {
		if (isProcessing)
             Compute();
    }
    public void ProcessScript(AmiScript _script) {		
        //Reset();
		this.script = _script;
		isProcessing = true;
    }
    
    public void Compute()
    {
        timer += Time.deltaTime;
      
        bool someFunctionIsActive = false;

		foreach (AmiClass amiClass in script.classes) {
			if (amiClass.sequenceID <= activeSequence && !amiClass.isDone) {
				someFunctionIsActive = true;

				UpdateFuncion (amiClass);

				if (!characterFalled) {
					character.UpdateFunctions (amiClass, timer);
				}
			}
		}
		if (!someFunctionIsActive)
			Reset ();
      
    }
	void UpdateFuncion(AmiClass amiClass)
    {
		float duration = (float)GetFunctionDuration(amiClass);
		if (duration>0 && duration >= timer)
		{
			//uifl.SetFilled((timer / duration));
		}
		else
		{            
			//print ("done " + uifl.function.value);
			amiClass.isDone = true;
			activeSequence++;
			timer = 0;
			//if (uifl.function.value == "Parallel") {
				//  activeSequence++;
			//	timer = 0;
			//} else {				
			//	if(uifl.parallelOf != null)
			//	{ 
				//	if (IsParallelSequenceDone (uifl.sequenceID)) {
				//		activeSequence++;
				//		timer = 0;
				//	}	
			//	} else {
				//	activeSequence++;		
					
			//	}
			//}
		}
    }
	bool IsParallelSequenceDone(int sequenceID)
	{
		foreach (AmiClass amiClass in script.classes) {
			if (amiClass.sequenceID == sequenceID && !amiClass.isDone) {
				return false;
			}
		}	
		return true;
	}

	//	foreach (AmiClass amiClass in script.classes) {
			
	//	}
	//	foreach (UIFunctionLine uifl in parallelSequence.GetComponentsInChildren<UIFunctionLine>()) {
		//	if (!parallelLine.done) {
			//	return false;
		//	}
		//}
	//	return true;		
	//}
	float GetFunctionDuration(AmiClass amiClass)
    {
		foreach (AmiArgument type in amiClass.argumentValues)
        {
			if (type.type == AmiClass.types.WAIT)
				return float.Parse(type.value)/100;
			if (type.type == AmiClass.types.TIME)
            {
				return float.Parse(type.value) / 100;
            }
        }
        return 0;
    }
    void Reset()
    {
		isProcessing = false;

        timer = 0;
        activeSequence = 0;
        character.Reset();
		foreach (AmiClass amiClass in script.classes) {
			amiClass.isDone = false;
		}
        if (character.isEditorCharacter)
        {
            Events.OnUIFunctionChangeIconColor(Color.green);
            Events.OnDebug(false);
        }
    }
}
