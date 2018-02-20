using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopupManager : MonoBehaviour {

	public static PopupManager Current {get;set;}
	private static GameObject _purchasePopupPrefab;
	private static GameObject _eventPopupPrefab;
	private static GameObject _notificationPrefab;

	public PopupBase CurrentFullScreenPopup;
	
	private Transform notificationsArea;

	void Start()
	{
		Current = this;
		notificationsArea = GameObject.Find("NotificationsArea").transform;
	}

	public void ShowEventPopup(string message, float time, UnityAction onConfirm)
	{
		if(_eventPopupPrefab == null)
			_eventPopupPrefab = Resources.Load<GameObject>("UI/EventPopup");
		if(CurrentFullScreenPopup != null)
			return;
		var instance = Instantiate(_eventPopupPrefab, transform) as GameObject;
		var popup = instance.GetComponent<EventPopup>();
		CurrentFullScreenPopup = popup;
		ADVController.Current.canAdvance = false;
		popup.message = message;
		popup.time = time;
		popup.onConfirm = onConfirm;
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

	public void ShowNotification(string message, int price) 
	{
		if(_notificationPrefab == null)
			_notificationPrefab = Resources.Load<GameObject>("UI/Notification");
		var go = Instantiate(_notificationPrefab, notificationsArea) as GameObject;
		var notification = go.GetComponent<Notification>();
		notification.message = message;
		notification.price = price;
	}
}
