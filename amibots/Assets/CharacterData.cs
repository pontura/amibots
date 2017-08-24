using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour {
    
    static CharacterData mInstance = null;
    public CharacterScripts characterScripts;

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
        characterScripts = GetComponent<CharacterScripts>();
    }
}
