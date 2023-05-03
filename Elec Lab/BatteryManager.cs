using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryManager : MonoBehaviour
{
	[SerializeField] private GameObject prefabObj;
	private GameObject mycanvasGameObject;
	private Canvas mycanvas;
	private GameObject BatteryControlPanel;
	private bool panelActive;

	public int batteryVoltage;
	private GameObject myTextGameobject;
	private Text VoltsText;

	private void Awake()
	{
		BatteryControlPanel = null;
		panelActive = false;
		batteryVoltage = 0;
		mycanvasGameObject = GameObject.Find("Canvas");
		mycanvas = mycanvasGameObject.GetComponent<Canvas>();
	}

	private void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(1))
		{
			if (BatteryControlPanel == null)
			{
				if (mycanvas != null)
				{
					BatteryControlPanel = Instantiate(prefabObj);
					BatteryControlPanel.name = prefabObj.name;
					BatteryControlPanel.transform.SetParent(mycanvas.transform, false);
					panelActive = true;
				}
			}
			else
			{
				if (panelActive == true)
				{
					BatteryControlPanel.SetActive(false);
					panelActive = false;
				}
				else
				{
					BatteryControlPanel.SetActive(true);
					panelActive = true;
				}
			}
		}
    }

	public void MinusButton()
	{
		BatteryControlPanel = GameObject.Find("BatteryControlPanel");
		GameObject battery = GameObject.Find("Battery");
		BatteryManager battery_Script = battery.GetComponent<BatteryManager>();

		if (battery_Script == null) { return; }

		if (BatteryControlPanel != null)
		{
			myTextGameobject = BatteryControlPanel.transform.Find("VoltsTXT").gameObject;
			if (myTextGameobject != null)
			{
				myTextGameobject.TryGetComponent<Text>(out VoltsText);
				if (VoltsText != null)
				{
					if (battery_Script.batteryVoltage > 0)
					{
						battery_Script.batteryVoltage--;
						VoltsText.text = battery_Script.batteryVoltage.ToString() + " Volts";
					}
					else
					{
						Debug.Log("voltage>0 -- " + battery_Script.batteryVoltage);
					}
				}
				else
				{
					Debug.Log("text is null");
				}
			}
			else
			{
				Debug.Log("gameobject of text is null");
			}
		}
		else
		{
			Debug.Log("panel is null");
		}
	}

	public void PlusButton()
	{
		BatteryControlPanel = GameObject.Find("BatteryControlPanel");
		GameObject battery = GameObject.Find("Battery");
		BatteryManager battery_Script = battery.GetComponent<BatteryManager>();
		if (battery_Script == null) { return; }
		

		if (BatteryControlPanel != null)
		{
			myTextGameobject = BatteryControlPanel.transform.Find("VoltsTXT").gameObject;
			if (myTextGameobject != null)
			{
				myTextGameobject.TryGetComponent<Text>(out VoltsText);
				if (VoltsText != null)
				{
					battery_Script.batteryVoltage++;
					VoltsText.text = battery_Script.batteryVoltage.ToString() + " Volts";
				}
				else
				{
					Debug.Log("text is null");
				}
			}
			else
			{
				Debug.Log("gameobject of text is null");
			}
		}
		else
		{
			Debug.Log("panel is null");
		}
	}

}
