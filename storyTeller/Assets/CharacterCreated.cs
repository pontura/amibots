using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCreated : MonoBehaviour {

    public Camera cam;
    public Character character;

	public void Init(CharacterData data) {
        cam.targetTexture = new RenderTexture(400, 400, 24);
        character.Init(data.id);
        character.data = data;
        Invoke("Delayed", 0.5f);
    }
    void Delayed()
    {
        character.customizer.Init();
    }
}
