using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentManager : MonoBehaviour
{
	public int value;
	[SerializeField] private GameObject prefabObj;
	private GameObject mycanvasGameObject;
	private Canvas mycanvas;

	private ComponentControlPanel controlPanel_script;
	public GameObject controlPanel;
	private bool panelActive;
	public GameObject electrocomponent;

	private GameObject myTextGameobject;
	private Text controlPanelText;
	private string measure_point;

	private void Start()
	{
		value = 0;
		panelActive = false;
		mycanvasGameObject = GameObject.Find("Canvas");
		mycanvas = mycanvasGameObject.GetComponent<Canvas>();

		switch (gameObject.name)
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
	}

	private void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(1) && gameObject.tag == "objectInDropArea")
		{
			electrocomponent = gameObject;
			if (controlPanel == null)
			{
				if (mycanvas != null)
				{
					GameObject neo = Instantiate(prefabObj);
					controlPanel = neo;

					controlPanel.name = prefabObj.name;
					controlPanel.transform.SetParent(mycanvas.transform, false);
					panelActive = true;

					myTextGameobject = controlPanel.transform.Find("TXT").gameObject;
					if (myTextGameobject!=null)
					{
						controlPanelText = myTextGameobject.GetComponent<Text>();
					}

					if (controlPanelText != null)
					{
						controlPanelText.text = controlPanelText.text + measure_point;
					}

					Vector3 pos = Camera.main.WorldToScreenPoint(electrocomponent.transform.position);
					pos += new Vector3(180 * 0.7f, 0f, 0f);
					controlPanel.transform.position = pos;

					controlPanel_script = controlPanel.GetComponent<ComponentControlPanel>();
					if (controlPanel_script != null)
					{
						controlPanel_script.controlPanel = controlPanel;
						controlPanel_script.electrocomponent = gameObject;
					}
					else
					{
						Debug.Log("controlPanel_script==null");
					}
				}
			}
			else
			{
				if (panelActive == true)
				{
					controlPanel.SetActive(false);
					panelActive = false;
				}
				else
				{
					Vector3 pos = Camera.main.WorldToScreenPoint(electrocomponent.transform.position);
					pos += new Vector3(180 * 0.7f, 0f, 0f);
					controlPanel.transform.position = pos;
					controlPanel.SetActive(true);
					panelActive = true;
				}
			}
		}
	}
}
