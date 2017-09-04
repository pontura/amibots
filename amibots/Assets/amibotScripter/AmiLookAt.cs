using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmiLookAt : MonoBehaviour {

    Character character;    

    void Start()
    {
        character = GetComponent<Character>();
    }
    void OnDestroy()
    {
    }
    public void Activate(string direction)
    {
        switch (direction)
        {
            case "tap":
                if (character.transform.localPosition.x > character.lookAtTarget.x)
                    character.transform.localScale = new Vector3(1, 1, 1);
                else
                    character.transform.localScale = new Vector3(-1, 1, 1);
                break;
            case "opposite":
                transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
                break;
        }
    }
    
}
