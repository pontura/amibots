using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public bool isEditorCharacter;
    public GameObject pivot;
    public GameObject allBody;

    public GameObject body;
    public GameObject hips;
    public GameObject head;

	public GameObject foot_right;
	public GameObject foot_left;

	public GameObject hand_right;
	public GameObject hand_left;

	Animation anim;
	CharacterRulesToFall characterRulesToFall;
    public CharacterScriptsProcessor scriptsProcessor;
    AmiTween amiTween;
    AmiLookAt amiLookAt;
    AmiCustomize amiCustomizer;
    public bool falled;

    public states state;
    public Vector3 lookAtTarget;

    public enum states
	{
		IDLE,
		FALL,
	}

	void Start () {
        amiTween = GetComponent<AmiTween>();
        amiLookAt = GetComponent<AmiLookAt>();
        scriptsProcessor = GetComponent<CharacterScriptsProcessor>();
        characterRulesToFall = GetComponent<CharacterRulesToFall> ();
        amiCustomizer = GetComponent<AmiCustomize>();
        anim = GetComponent<Animation> ();
        Events.CharacterFall += CharacterFall;
        Events.ClickedOn += ClickedOn;
    }
    void OnDestroy()
    {
        Events.CharacterFall -= CharacterFall;
        Events.ClickedOn -= ClickedOn;
    }
    void ClickedOn(Vector3 pos)
    {
        lookAtTarget = pos;
    }

    void CharacterFall(string _anim)
	{
        falled = true;
        state = states.FALL;
		anim.Play (_anim);
	}
	public void UpdateFunctions(AmiClass amiClass, float timer)
	{
		GameObject bodyPart = null;
		float distance = 1;
		float time = 1;
        string direction = "";
		foreach (AmiArgument amiArgument in amiClass.argumentValues) {
            if (amiArgument.type == AmiClass.types.EXPRESSIONS)
            {
                print("amiArgument.type: " + amiArgument.type + " " + amiArgument.value);
                amiCustomizer.Activate(CharacterCustomizer.parts.HEAD, amiArgument.value);
                return;
            }
            if (amiArgument.type == AmiClass.types.LOOK_AT_TARGET)
            {
				amiLookAt.Activate(amiArgument.value);
                return;
            }
			if (amiArgument.type == AmiClass.types.DIRECTION)
				direction = amiArgument.value;
			if (amiArgument.type == AmiClass.types.DISTANCE) 
				distance = float.Parse(amiArgument.value)/100;
			if (amiArgument.type == AmiClass.types.TIME) 
				time = float.Parse(amiArgument.value)/100;
			if (amiArgument.type == AmiClass.types.BODY_PART) {
				bodyPart = GetBodyPartByClassName(amiArgument.value);
            }
        }
        if (direction != "") {
            characterRulesToFall.Check(bodyPart);
            amiTween.Move(bodyPart, direction, distance / time);
        }
    }
    float XOffset;
	public void Reset()
	{
        if (isEditorCharacter)
        {
            transform.localPosition = Vector3.zero;
            amiTween.Reset();
        }
        else
        {            
           // XOffset = body.transform.localPosition.x;
            if (allBody.transform.localPosition.x != 0)
            {
                Vector3 newPos = transform.localPosition;
                newPos.x += allBody.transform.localPosition.x - (body.transform.localPosition.x - amiTween.body_r_OriginalPos.x);
                transform.localPosition = newPos;
                allBody.transform.localPosition = Vector2.zero;
            }
            amiTween.Reset();
            transform.localPosition = pivot.transform.localPosition;
        }
        
        //anim.Play ("idle");
        falled = false;
		state = states.IDLE;
    }
    GameObject GetBodyPartByClassName(string className)
    {
        switch (className)
        {
            case "right foot":
                return foot_right;
            case "left foot":
                return foot_left;
            case "right hand":
                return hand_right;
            case "left hand":
                return hand_left;
            case "hips":
                return hips;
			case "head":
				return head;
        }
        return null;
    }
    private float speedY = 40;
    public void OnCharacterMoveInX(float newX)
    {
        if (isEditorCharacter) return;
        Vector3 pos = allBody.transform.localPosition;
        pos.x = -body.transform.localPosition.x;
        allBody.transform.localPosition = pos;

        pivot.transform.Translate(Vector3.forward * Time.deltaTime * (newX* speedY));
        transform.localPosition = pivot.transform.localPosition;


    }

}
