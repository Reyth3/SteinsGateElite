using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour {
	public static VideoController Current { get; set; }

	public Camera VideoTarget;
	private VideoPlayer player;
	void Awake()
	{
		if(Current == null)
			Current = this;
		else if(Current != this)
		Destroy(gameObject);
		player = VideoTarget.gameObject.AddComponent<VideoPlayer>();
		player.audioOutputMode = VideoAudioOutputMode.AudioSource;
		player.renderMode = VideoRenderMode.CameraNearPlane;
	}

	public void SetAction(ADVAction action)
	{
		player.clip = action.video;
		player.Play();
	}

	public void Pause() {
		player.Pause();
	}
}
