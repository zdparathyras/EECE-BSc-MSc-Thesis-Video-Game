using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
	private float startPosX;
	private float startPosY;
	private bool isBeingHeld = false;
	private GameObject DragItem;

	private void Start()
	{
		DragItem = this.gameObject;

		Vector3 mousePos;
		mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint(mousePos);

		DragItem.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
		
	}

	private void Update()
	{
		if (isBeingHeld == true)
		{
			Vector3 mousePos;
			mousePos = Input.mousePosition;
			mousePos = Camera.main.ScreenToWorldPoint(mousePos);

			DragItem.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
		}
	}

	private void OnMouseDown()
	{
		if (Input.GetMouseButtonDown(0))
		{
			isBeingHeld = true;

			Vector3 mousePos;
			mousePos = Input.mousePosition;
			mousePos = Camera.main.ScreenToWorldPoint(mousePos);

			startPosX = mousePos.x - DragItem.transform.localPosition.x;
			startPosY = mousePos.y - DragItem.transform.localPosition.y;
		}
	}

	private void OnMouseUp()
	{
		isBeingHeld = false;
	}
}
