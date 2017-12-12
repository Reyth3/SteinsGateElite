using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager Current {get;set;}

	public int Pins;
	public bool phoneUnlocked;
	// Use this for initialization
	void Start () {
		if(Current == null)
			Current = this;
		else if (Current != this)
			Destroy(gameObject);
		DontDestroyOnLoad(this);
	}

	#region Game Data Management
	public void AddPins(int pins)
	{
		Pins += pins;
		ADVController.Current.UpdateADVDataUI();
	}

	public bool PayPins(int amount)
	{
		if(Pins >= amount)
		{
			Pins -= amount;
			ADVController.Current.UpdateADVDataUI();
			return true;
		}
		else return false;
	}

	public void UnlockPhone(bool enabled)
	{
		phoneUnlocked = enabled;
		ADVController.Current.UpdateADVDataUI();
	}
	#endregion
}
