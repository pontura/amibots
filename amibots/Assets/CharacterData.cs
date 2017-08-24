using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour {
    
    static CharacterData mInstance = null;
    public CharacterFunctions characterFunctions;

    public static CharacterData Instance
    {
        get
        {
            return mInstance;
        }
    }
   
    void Awake()
    {
        if (!mInstance)
            mInstance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        characterFunctions = GetComponent<CharacterFunctions>();
    }
}
