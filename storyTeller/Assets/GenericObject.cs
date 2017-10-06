using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericObject : SceneObject {

	public GameObject[] all;

	public override void OnStart()
	{
		foreach (GameObject go in all) {
			go.SetActive (false);
		}
		GetAsset (data.sceneObjectName).SetActive(true);
	}
	GameObject GetAsset(string assetName)
	{
		foreach (GameObject go in all) {
			if (go.name == assetName)
				return go;
		}
		return null;
	}
}
