using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentCalculator : MonoBehaviour
{
    private GameObject battery;
    private BatteryManager battery_script;
    private GameObject breadboard;
    private test breadboard_script;
    private List<GameObject> electrocomponents;
    
    
    void Start()
    {
        electrocomponents = new List<GameObject>();
        battery = GameObject.Find("Battery");    
        if (battery != null)
		{
            battery_script = battery.GetComponent<BatteryManager>();
		}
        breadboard = GameObject.Find("breadboard");
        if (breadboard != null)
        {
            breadboard_script = breadboard.GetComponent<test>();
        }

    }

	private void FixedUpdate()
	{
        if (breadboard_script == null || battery_script == null) { return; }
       // electrocomponents = breadboard_script.objectssss
    }
}
