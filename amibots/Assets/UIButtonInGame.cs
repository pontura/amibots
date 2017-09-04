using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonInGame : MonoBehaviour {

    Animation anim;
    public Image colorizable;
    public AmiScript script;

    public void Init(AmiScript _script)
    {
        this.script = _script;
    }
    public void Clicked()
    {
        Events.OnEditScript(script);
    }
    void Start()
    {
        anim = GetComponent<Animation>();
    }
    public void Activate()
    {
        anim.Play("late");
    }
    public void ChangeColor(Color _color)
    {
        colorizable.color = _color;
    }
}
