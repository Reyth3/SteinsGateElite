using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopupManager : MonoBehaviour {

	public static PopupManager Current {get;set;}
	private static GameObject _purchasePopupPrefab;

	public PopupBase CurrentFullScreenPopup;

	void Start()
	{
		Current = this;
	}

	public void ShowPurchasePopup(string title, string message, int price, UnityAction onConfirm)
	{
		if(_purchasePopupPrefab == null)
			_purchasePopupPrefab = Resources.Load<GameObject>("UI/PurchasePopup");
		if(CurrentFullScreenPopup != null)
			return;
		var instance = Instantiate(_purchasePopupPrefab, transform) as GameObject;
		var popup = instance.GetComponent<PurchasePopup>();
		CurrentFullScreenPopup = popup;
		ADVController.Current.canAdvance = false;
		popup.title = title;
		popup.message = message;
		popup.price = price;
		popup.onConfirm = onConfirm;
	}
}
