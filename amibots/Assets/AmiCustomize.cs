using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmiCustomize : MonoBehaviour {

    Character character;

    void Start()
    {
        character = GetComponent<Character>();
    }
    void OnDestroy()
    {
    }
    public void Activate(CharacterCustomizer.parts part, string newImage)
    {
        Events.OnCustomize(part, newImage);
    }
}
