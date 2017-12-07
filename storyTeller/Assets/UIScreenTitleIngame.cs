using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScreenTitleIngame : MonoBehaviour {

    public Text field;
    public GameObject panel;
    
    public void Open(string _text)
    {
        field.text = _text;
        panel.SetActive(true);
    }
    public void Close()
    {
        panel.SetActive(false);
    }
}
