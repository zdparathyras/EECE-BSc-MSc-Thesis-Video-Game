using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag2 : MonoBehaviour
{
    //public float yOffsetForDraggedObject = 1;
    Plane plane;
    float distance;

    private Rigidbody rb;

    private void Start()
    {

        //plane = new Plane(Vector3.up, new Vector3(0, yOffsetForDraggedObject, 0));
        plane = new Plane(Vector3.up, new Vector3(0, 6.5f, 0));

        //transform.rotation = Quaternion.identity;
        //Vector3 rotat = transform.rotation.eulerAngles;
        //transform.Rotate(-rotat.x, 0.0f, -rotat.z);
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0, eulerRotation.y, 0);

        gameObject.TryGetComponent<Rigidbody>(out rb);
    }

    void OnMouseDrag()
    {
        if (gameObject.CompareTag("Draggable") || gameObject.CompareTag("objectInDropArea"))
        {

            if (rb != null)
			{
                rb.isKinematic = false;
                //rb.constraints = RigidbodyConstraints.None;
                if (rb.freezeRotation == true)
				{
                   //rb.freezeRotation = false;
				}
			}


            //obj dragging follow mouse at y=6.5f and keep rotation (0,0,0,)
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out distance))
            {
                transform.position = ray.GetPoint(distance);
            }
            //transform.rotation = Quaternion.identity;
            //Vector3 rotat2 = transform.rotation.eulerAngles;
            //transform.Rotate(-rotat2.x, 0.0f, -rotat2.z);
            Vector3 eulerRotation = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(0, eulerRotation.y, 0);
        }
    }
}