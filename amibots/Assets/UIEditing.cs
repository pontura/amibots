using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEditing : MonoBehaviour {

    public GameObject state1;
    public GameObject state2;
    public UiClassManager uiClassManager;

	void Start () {
        state1.SetActive(true);
        state2.SetActive(false);
    }
    public void OpenInitClasses()
    {
      //  state1.SetActive(false);
        state2.SetActive(true);
        uiClassManager.Init();
    }
	
}
