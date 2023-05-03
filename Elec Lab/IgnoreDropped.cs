using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreDropped : MonoBehaviour
{


	private void OnCollisionEnter(Collision collision)
	{
		Collider this_collider;
		Collider colliderToIngore;

		gameObject.TryGetComponent<Collider>(out this_collider);
		collision.gameObject.TryGetComponent<Collider>(out colliderToIngore);

		if (gameObject.tag == "objectInDropArea" && collision.gameObject.tag=="Draggable")
		{
			if (this_collider != null && colliderToIngore != null)
			{
				Physics.IgnoreCollision(colliderToIngore, this_collider,true);
				Debug.Log("ignored collision between "+ gameObject.name +" and "+collision.gameObject.name);
			}
		}
		else if (gameObject.tag == "Draggable" && collision.gameObject.tag == "objectInDropArea")
		{
			if (this_collider != null && colliderToIngore != null)
			{
				Physics.IgnoreCollision(colliderToIngore, this_collider, true);
				Debug.Log("ignored collision between " + gameObject.name + " and " + collision.gameObject.name);
			}
		}
		else if (gameObject.tag == "objectInDropArea" && collision.gameObject.tag == "objectInDropArea")
		{
			if (this_collider != null && colliderToIngore != null)
			{
				Physics.IgnoreCollision(colliderToIngore, this_collider, true);
				Debug.Log("ignored collision between " + gameObject.name + " and " + collision.gameObject.name);
			}
		}
		else
		{
			//Debug.Log(gameObject.name + " collided with " + collision.gameObject.name);
		}
		
	}
}
/*
		else if (gameObject.tag == "Draggable" && collision.gameObject.tag == "DropAreaOccupied")
		{
			if (this_collider != null && colliderToIngore != null)
			{
				Physics.IgnoreCollision(colliderToIngore, this_collider, true);
				Debug.Log("ignored collision between " + gameObject.name + " and " + collision.gameObject.name);
			}
		}
*/
