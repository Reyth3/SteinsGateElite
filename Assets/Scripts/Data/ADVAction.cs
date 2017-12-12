using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[Serializable]
public class ADVAction : System.Object {
	[Header("General")]
	public string message;
	public string speaker;
	public VideoClip video;
	[Header("Manage ADV View")]
	public bool changePhoneDate;
	public string phoneDate;
	[Header("Flags")]
	public bool isWorldLineShift;
	public bool isCrossAttractorFieldShift;
	public bool isPhoneUsed;
}
