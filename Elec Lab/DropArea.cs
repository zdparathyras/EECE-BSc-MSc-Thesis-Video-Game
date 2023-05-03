using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour
{
	public bool occupied;
	public string row;
	public char column;
	private test manager_script;
	private GameObject breadboard;

	private void Start()
	{
		breadboard = GameObject.Find("breadboard");
		if (breadboard != null)
		{
			breadboard.TryGetComponent<test>(out manager_script);
		}
		occupied = false;
		row = gameObject.transform.parent.name;
		int nameLength = gameObject.name.Length;
		column = gameObject.name[nameLength - 1];
	}

	private void OnTriggerEnter(Collider other)
	{
		
		if (manager_script != null && other.gameObject.tag=="Draggable")
		{
			//Debug.Log(gameObject.name + " triggered by " + other.gameObject.name);
			//Debug.Log(gameObject.name + " sent data");
			manager_script.OnChildTriggerEnter(gameObject, other.gameObject);
		}
	}
}
