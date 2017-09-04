using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewScriptPopup : MonoBehaviour {

    public GameObject panel;
    public Text field;
    public InputField inputField;
    public AmiScript.categories category;

    void Start () {
        Events.EditNameOfAction += EditNameOfAction;
        panel.SetActive(false);
    }
    void EditNameOfAction(AmiScript.categories category)
    {
        this.category = category;

        field.text = "Name your new ";
        switch (category)
        {
            case AmiScript.categories.ON_TAP:
                field.text += "On Tap action";
                break;
            case AmiScript.categories.AVATAR_ACTION:
                field.text += "Avatar action";
                break;
        }
        panel.SetActive(true);
    }
    public void Done()
    {
        if (inputField.text == "") return;
        panel.SetActive(false);
        Events.CreateNewEmptyScript(category, inputField.text);
    }
    public void Cancel()
    {
        panel.SetActive(false);
        Events.OnUIChangeState(UIGame.states.PLAYING);
    }


}
