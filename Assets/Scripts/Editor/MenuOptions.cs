using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MenuOptions {

	[MenuItem("VN Engine/Create ADV Script")]
	public static void CreateADVScript()
	{
		ScriptableObjectUtility.CreateAsset<ADVScript>();
	}
}
