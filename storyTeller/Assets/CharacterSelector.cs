using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour {

    public GameObject toInstantiate;
    GameObject asset;
    public Vector3 pos;

    private void Start()
    {
        asset = Instantiate(toInstantiate);
        asset.transform.SetParent(transform);
        asset.transform.localScale = Vector3.one;
        asset.transform.localEulerAngles = Vector3.zero;
        asset.transform.localPosition = pos;
    }
    public void SetState(bool isOn)
    {
        asset.SetActive(isOn);
    }
}
