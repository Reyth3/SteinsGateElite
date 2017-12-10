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
	#endregion
	bool canAdvance;
	float advanceCooldown;
	// Use this for initialization
	void Start () {
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
	}
	
	void InitializeUIReferences()
	{
		UI_SpeakerIndicator = GameObject.Find("SpeakerIndicator");
		UI_messageText = GameObject.Find("MessageText").GetComponent<TextMeshProUGUI>();
		UI_speakerText = GameObject.Find("SpeakerText").GetComponent<TextMeshProUGUI>();
	}

	void LoadActions()
	{
		var actions = loadedScript.ScriptActions.ToList();
		this.actions = actions;
	}

	void InitializeVideoPlayback()
	{
		
	}

	public bool Advance()
	{
		if(!canAdvance)
			return false;
		if(currentAction == actions.Count -1)
			return false;
		
		canAdvance = false;
		advanceCooldown = 1f;
		currentAction++;
		var action = actions[currentAction];
		UI_messageText.text = action.message;
		UI_speakerText.text = action.speaker;
		if(string.IsNullOrEmpty(action.speaker))
			UI_SpeakerIndicator.SetActive(false);
		else UI_SpeakerIndicator.SetActive(true);
		if(action.video != null)
			VideoController.Current.SetAction(action);
		return true;
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
}
