using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOBJ : MonoBehaviour
{
	public int pinWidth_x; //drop areas width
	public int pinWidth_z; //rows width
	private Rigidbody rb;
	public bool RotationHorizontal;

	private void Start()
	{
		RotationHorizontal = true;
	}

	private void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(1) && gameObject.tag=="Draggable")
		{
			//gameObject.TryGetComponent<Rigidbody>(out rb);
			Debug.Log("Pressed right click.");
			gameObject.transform.Rotate(0.0f, 90.0f, 0.0f, Space.Self);
			//rb.constraints = RigidbodyConstraints.FreezeRotationY;
			RotationHorizontal = !RotationHorizontal;
		}
	}

}
