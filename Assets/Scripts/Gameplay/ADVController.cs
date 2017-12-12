using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ADVController : MonoBehaviour {

	public static ADVController Current { get; set; }
	public ADVScript loadedScript;
	private List<ADVAction> actions;
	private int currentAction;

	#region  UI References
	private TextMeshProUGUI UI_messageText;
	private TextMeshProUGUI UI_speakerText;
	private GameObject UI_SpeakerIndicator;
	private Animator UI_messageTextAnimator;
	private GameObject UI_PhoneLockedOverlay;
	private TextMeshProUGUI UI_PinsCountText;
	#endregion
	public bool canAdvance;
	float advanceCooldown;
	// Use this for initialization
	void Start() {
		Current = this;
		if(loadedScript == null)
		{
			Debug.LogError("[ADVController] No script was loaded!");
			Destroy(gameObject);
		}
		
		canAdvance = true;
		InitializeUIReferences();
		LoadActions();
		currentAction = -1;
		Advance();
		UpdateADVDataUI();
	}
	
	void InitializeUIReferences()
	{
		UI_SpeakerIndicator = GameObject.Find("SpeakerIndicator");
		UI_messageText = GameObject.Find("MessageText").GetComponent<TextMeshProUGUI>();
		UI_messageTextAnimator = UI_messageText.gameObject.GetComponent<Animator>();
		UI_speakerText = GameObject.Find("SpeakerText").GetComponent<TextMeshProUGUI>();
		UI_PhoneLockedOverlay = GameObject.Find("Locked");
		UI_PinsCountText = GameObject.Find("LabMemPinIcon").GetComponentInChildren<TextMeshProUGUI>();
	}

	void LoadActions()
	{
		var actions = loadedScript.ScriptActions.ToList();
		this.actions = actions;
	}

	public bool Advance()
	{
		if(!canAdvance || PopupManager.Current.CurrentFullScreenPopup != null)
			return false;
		if(currentAction == actions.Count -1)
			return false;
		
		canAdvance = false;
		advanceCooldown = 0.5f;
		UI_messageTextAnimator.SetTrigger("FadeIn");
		currentAction++;
		var action = actions[currentAction];
		UI_messageText.text = action.message;
		UI_speakerText.text = action.speaker;
		if(string.IsNullOrEmpty(action.speaker))
			UI_SpeakerIndicator.SetActive(false);
		else UI_SpeakerIndicator.SetActive(true);
		if(action.video != null)
			VideoController.Current.SetAction(action);
		if(action.isPhoneUsed)
			PhoneTriggerCheck();
		return true;
	}

	void PhoneTriggerCheck()
	{
		if(GameManager.Current.phoneUnlocked)
			return;
		VideoController.Current.Pause();
		PopupManager.Current.ShowPurchasePopup("Unlock Phone?", "You need to unlock your phone to continue. Proceed?", 10, () => {
			GameManager.Current.UnlockPhone(true);
		});
	}

	void Update () {
		if(!canAdvance)
		{
			advanceCooldown -= Time.deltaTime;
			if(advanceCooldown <= 0f)
				canAdvance = true;
		}
		if(Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump") || Input.GetButtonDown("Submit"))
			Advance();
		
	}

	public void UpdateADVDataUI() 
	{
		var _ref = GameManager.Current;
		UI_PinsCountText.text = _ref.Pins.ToString();
		UI_PhoneLockedOverlay.SetActive(!_ref.phoneUnlocked);
	}
}
