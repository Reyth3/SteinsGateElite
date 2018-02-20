using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour {

	TextMeshProUGUI _messageTextMesh;
	Button _confirmButton;
	TextMeshProUGUI _priceTextMesh;

	public string message;
	public int price;
	// Use this for initialization
	void Start () {
		_messageTextMesh = GetComponentInChildren<TextMeshProUGUI>();
		_confirmButton = transform.GetChild(2).GetComponent<Button>();
		_priceTextMesh = _confirmButton.GetComponentInChildren<TextMeshProUGUI>();
		UpdateUI();
	}

	void UpdateUI() {
		_messageTextMesh.text = message;
		_priceTextMesh.text = price.ToString();
	}

	void FixedUpdate() {
		UpdateUI();
	}
}
