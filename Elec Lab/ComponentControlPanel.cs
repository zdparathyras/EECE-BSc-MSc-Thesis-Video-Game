using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentControlPanel : MonoBehaviour
{
	private GameObject myTextGameobject;
	private Text controlPanelText;

	public GameObject controlPanel;
	public GameObject electrocomponent;

	private string measure_point;

	public void PlusButton()
	{
		bool null_found = false;

		controlPanel = gameObject.transform.parent.gameObject;
		if (controlPanel == null)
		{
			Debug.Log("controlPanel==null");
			null_found = true;
		}

		ComponentControlPanel controlPanel_script = controlPanel.GetComponent<ComponentControlPanel>();
		if (controlPanel_script == null)
		{
			Debug.Log("controlPanel_script==null");
			null_found = true;
		}

		electrocomponent = controlPanel_script.electrocomponent;
		if (electrocomponent == null)
		{
			Debug.Log("electrocomponent==null");
			null_found = true;
		}

		switch (electrocomponent.name)
		{
			case "Fuse":
				measure_point = " A";
				break;
			case "Condenser":
				measure_point = " F";
				break;
			case "Inductive":
				measure_point = " H";
				break;
			case "Resistor":
				measure_point = " Ohm";
				break;
		}

		ComponentManager electrocomponent_script = electrocomponent.GetComponent<ComponentManager>();
		if (electrocomponent_script == null)
		{
			Debug.Log("electrocomponent_script==null");
			null_found = true;
		}

		myTextGameobject = controlPanel.transform.Find("TXT").gameObject;
		if (myTextGameobject == null)
		{
			Debug.Log("myTextGameobject==null");
			null_found = true;
		}

		controlPanelText = myTextGameobject.GetComponent<Text>();
		if (controlPanelText == null)
		{
			Debug.Log("controlPanelText==null");
			null_found = true;
		}

		if (null_found == false)
		{
			electrocomponent_script.value++;
			controlPanelText.text = electrocomponent_script.value.ToString() + measure_point;
		}
	}

	public void MinusButton()
	{
		bool null_found = false;

		controlPanel = gameObject.transform.parent.gameObject;
		if (controlPanel == null)
		{
			Debug.Log("controlPanel==null");
			null_found = true;
		}

		ComponentControlPanel controlPanel_script = controlPanel.GetComponent<ComponentControlPanel>();
		if (controlPanel_script == null)
		{
			Debug.Log("controlPanel_script==null");
			null_found = true;
		}

		electrocomponent = controlPanel_script.electrocomponent;
		if (electrocomponent == null)
		{
			Debug.Log("electrocomponent==null");
			null_found = true;
		}

		switch (electrocomponent.name)
		{
			case "Fuse":
				measure_point = " A";
				break;
			case "Condenser":
				measure_point = " F";
				break;
			case "Inductive":
				measure_point = " H";
				break;
			case "Resistor":
				measure_point = " Ohm";
				break;
		}

		ComponentManager electrocomponent_script = electrocomponent.GetComponent<ComponentManager>();
		if (electrocomponent_script == null)
		{
			Debug.Log("electrocomponent_script==null");
			null_found = true;
		}

		myTextGameobject = controlPanel.transform.Find("TXT").gameObject;
		if (myTextGameobject == null)
		{
			Debug.Log("myTextGameobject==null");
			null_found = true;
		}

		controlPanelText = myTextGameobject.GetComponent<Text>();
		if (controlPanelText == null)
		{
			Debug.Log("controlPanelText==null");
			null_found = true;
		}

		if (null_found == false)
		{
			if (electrocomponent_script.value > 0)
			{
				electrocomponent_script.value--;
				controlPanelText.text = electrocomponent_script.value.ToString() + measure_point;
			}
		}
		
	}
}
