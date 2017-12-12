using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class PopupBase : MonoBehaviour {
	public string prefabPath;
	public string title;
	public string message;
	public UnityAction onConfirm;
	private float destroyCountdown;
	
	// Use this for initialization
	public virtual void Start () {
		destroyCountdown = -1;
	}
	
	// Update is called once per frame
	public virtual void Update () {
		if(destroyCountdown != -1)
		{
			destroyCountdown -= Time.deltaTime;
			if(destroyCountdown <= 0)
			{
				ADVController.Current.canAdvance = true;
				Destroy(gameObject);
			}
		}
	}

	public void Close()
	{
		if(destroyCountdown == -1) 
		{
			destroyCountdown = 0.5f;
		}
	}
}
