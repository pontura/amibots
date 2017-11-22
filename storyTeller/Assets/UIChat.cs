using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIChat : MonoBehaviour {

	public Image flecha;
	public Image image;
	public Text field;
	public Character character;
	public Vector2 offset;
	public int offsetHeight;
	public int flechaOffset;
	float dinamicHeight;
	public Camera cam;
	RectTransform rt;

	void Awake()
	{
		flecha.gameObject.SetActive (false);
		image.gameObject.SetActive (false);
		rt = GetComponent<RectTransform> ();
	}
	public void Init(int characterID, string text) {
		field.text = text;
		
		character = World.Instance.charactersManager.GetCharacter (characterID);
        print("y: " + character.transform.position.y);
        StartCoroutine(Live());
    }
	IEnumerator Live(){
		yield return new WaitForEndOfFrame ();
		dinamicHeight = field.GetComponent<RectTransform> ().sizeDelta.y;
		flecha.gameObject.SetActive (true);
		image.gameObject.SetActive (true);
		flecha.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (0, (-1*dinamicHeight/2) - flechaOffset);
		image.GetComponent<RectTransform> ().sizeDelta = new Vector2(image.GetComponent<RectTransform> ().sizeDelta.x, dinamicHeight + offsetHeight);

		Vector3 pos = character.transform.position;
		Vector2 viewportPoint = Camera.main.WorldToScreenPoint(pos);  
		viewportPoint.y += Time.deltaTime * ((dinamicHeight*100) / 4);
		rt.position = viewportPoint+offset;

		yield return new WaitForSeconds (4);
		Destroy (this.gameObject);
	}
	void Update()
	{
		Vector3 pos = character.transform.position;
		Vector2 viewportPoint = Camera.main.WorldToScreenPoint(pos);  
		viewportPoint.y += dinamicHeight;
		rt.position = Vector2.Lerp(rt.position, viewportPoint+offset, 0.3f);
	}
}
