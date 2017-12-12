using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PurchasePopup : PopupBase {

	private static GameObject _prefabRef;

	public int price;
	private TextMeshProUGUI UI_TitleText;
	private TextMeshProUGUI UI_MessageText;
	private TextMeshProUGUI UI_PriceText;
	private Button UI_CancelButton;
	private Button UI_PurchaseButton;
	

	// Use this for initialization
	public override void Start () {
		base.Start();
		AssignUIReferences();
		UI_TitleText.text = title;
		UI_MessageText.text = message;
		UI_PriceText.text = price.ToString();
		UI_CancelButton.onClick.AddListener(() => Close());
		UI_PurchaseButton.onClick.AddListener(() => {
			if(GameManager.Current.PayPins(price))
			{
				if(onConfirm != null)
					onConfirm();
				Close();
			}
		});
	}

	private void AssignUIReferences()
	{
		var window = transform.GetChild(0);
		UI_TitleText = window.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
		UI_MessageText = window.GetChild(1).GetComponent<TextMeshProUGUI>();
		var interactive = window.GetChild(2);
		UI_PriceText = interactive.GetChild(1).GetComponent<TextMeshProUGUI>();
		UI_CancelButton = interactive.GetChild(2).GetComponent<Button>();
		UI_PurchaseButton = interactive.GetChild(3).GetComponent<Button>();
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
	}
}
