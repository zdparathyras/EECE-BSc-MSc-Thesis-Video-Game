using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentDisplay : MonoBehaviour
{
	GameObject breadboard;
	GameObject txtGamoebj;
	Text myText;
	private test manager_script;
	private float this_current;
	private string str;
	private float breadboard_current;

	private void Start()
	{
		str = "Current= ";
		this_current = 0.0f;
		breadboard_current = 0.0f;
		breadboard = GameObject.Find("breadboard");
		if (breadboard != null)
		{
			breadboard.TryGetComponent<test>(out manager_script);
		}
		if (manager_script != null)
		{
			breadboard_current = manager_script.current;
		}
		txtGamoebj = gameObject.transform.Find("Text").gameObject;
		txtGamoebj.TryGetComponent<Text>(out myText);
	}

	private void FixedUpdate()
	{
		if (manager_script != null)
		{
			breadboard_current = manager_script.current;
		}

		if (this_current != breadboard_current)
		{
			
		}
		this_current = breadboard_current;
		myText.text = str + this_current.ToString() + " A";
	}

}
