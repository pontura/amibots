using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {

    public GameObject tooltip;
    public Text field;

    void Start () {
        OnTooltipHide();
        Events.OnTooltip += OnTooltip;
        Events.OnTooltipHide += OnTooltipHide;
    }

    void OnTooltipHide()
    {
        tooltip.SetActive(false);
    }

    void OnTooltip(string text, Vector3 pos) {
        tooltip.SetActive(true);
        field.text = text;
    }
}
