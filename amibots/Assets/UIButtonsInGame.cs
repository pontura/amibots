using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonsInGame : MonoBehaviour {

    public UIButtonInGame buttonInGame;
    public Transform container;

	void Start () {
        Events.OnUIChangeState += OnUIChangeState;
    }
    void OnUIChangeState(UIGame.states state)
    {
        if (state == UIGame.states.PLAYING)
            RefreshButtons();
    }

    void RefreshButtons()
    {
        Utils.RemoveAllChildsIn(container);
        foreach(AmiScript amiScript in CharacterData.Instance.characterScripts.scripts)
        {
            UIButtonInGame newButton = Instantiate(buttonInGame);
            newButton.transform.SetParent(container);
            newButton.transform.localScale = Vector3.one;
            newButton.Init(amiScript);
        }
       

    }
}
