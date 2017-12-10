using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageBoxAspectRatioPreserver : MonoBehaviour {

	Vector2 currentScreenSize;
	// Use this for initialization
	void Start () {
		ResizeMessageWindow();
	}

	void ResizeMessageWindow()
	{
		var scrW = (float)Screen.width;
		var image = GetComponent<Image>();
		var boxW = image.sprite.bounds.size.x;
		var boxH = image.sprite.bounds.size.y;
		var ratio = boxW / boxH;
		if(scrW / boxH != ratio)
		{	
			var rt = image.GetComponent<RectTransform>();
			rt.sizeDelta = new Vector2(rt.sizeDelta.x, scrW / ratio);
		}
		currentScreenSize = new Vector2(scrW, Screen.height);
		Debug.Log("Message Window Resized!");
	}

	void Update() 
	{
		if(Screen.width != currentScreenSize.x || Screen.height != currentScreenSize.y)
			ResizeMessageWindow();
	}
}
