using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {

    public GameObject tooltip;
    public Text field;

    void Start () {
      
        Events.OnTooltip += OnTooltip;
    }

    void OnTooltip(string text, Vector3 pos) {

    }
}
