using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventPopup : PopupBase {

	[Range(5, 3600)]
	public float time;

	private Transform UI_windowRoot;
	private TextMeshProUGUI UI_messageBox;
	private Slider UI_progressSlider;
	private TextMeshProUGUI UI_progressText;

	private float remainingTime;

	// Use this for initialization
	public override void Start () {
		base.Start();
		UI_windowRoot = transform.GetChild(0);
		UI_messageBox = UI_windowRoot.GetChild(1).GetComponent<TextMeshProUGUI>();
		UI_progressSlider = UI_windowRoot.GetComponentInChildren<Slider>();
		UI_progressText = UI_progressSlider.GetComponentInChildren<TextMeshProUGUI>();
		UI_messageBox.text = message;
		remainingTime = time;
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
		if(remainingTime > 0)
		{
			remainingTime -= Time.deltaTime;
			UI_progressSlider.value = 1 - (remainingTime/time);
			var minutes = remainingTime / 60;
			var seconds = remainingTime % 60;
			UI_progressText.text = string.Format("{0:00}m {1:00}s", minutes, seconds);
		}
		else 
		{
			if(onConfirm != null)
				onConfirm();
			Close();
		}
	}
}
