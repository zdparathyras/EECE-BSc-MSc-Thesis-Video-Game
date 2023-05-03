using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragUI : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
	[SerializeField] private GameObject prefabObj;
	[SerializeField] private GameObject DragImage;
	private GameObject objToInstatiate;
	private RectTransform rect; //dragged items rect transform
	private CanvasGroup draggedCanvasGroup; // dragged items canvas group
	private GameObject mycanvasGameObject;
	private Canvas mycanvas;

	private Vector3 mouseWorldPosition;

	private void Awake()
	{

		rect = GetComponent<RectTransform>();
		mycanvasGameObject = GameObject.Find("Canvas");
		mycanvas = mycanvasGameObject.GetComponent<Canvas>();
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (gameObject.CompareTag("DraggableUI")) {
			GameObject imgToDrag = Instantiate(DragImage);
			imgToDrag.transform.SetParent(mycanvas.transform, false);
			imgToDrag.transform.position = eventData.pointerDrag.transform.position;
			eventData.pointerDrag = imgToDrag;

			eventData.pointerDrag.AddComponent<CanvasGroup>();
			draggedCanvasGroup = eventData.pointerDrag.GetComponent<CanvasGroup>();
			draggedCanvasGroup.interactable = true;
			draggedCanvasGroup.alpha = .6f;
			draggedCanvasGroup.blocksRaycasts = false;
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		rect.anchoredPosition += eventData.delta / mycanvas.scaleFactor;

		//Get mouse position on world and y set to 6
		Plane plane = new Plane(Vector3.up, new Vector3(0, 6.5f, 0));
		float distance;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (plane.Raycast(ray, out distance))
		{
			mouseWorldPosition = ray.GetPoint(distance);
		}

	}

	public void OnEndDrag(PointerEventData eventData)
	{
		draggedCanvasGroup = eventData.pointerDrag.GetComponent<CanvasGroup>();
		draggedCanvasGroup.alpha = 1f;
		draggedCanvasGroup.blocksRaycasts = true;
		Destroy(eventData.pointerDrag);

		objToInstatiate = Instantiate(prefabObj);
		objToInstatiate.name = prefabObj.name;

		objToInstatiate.transform.position = mouseWorldPosition;
		//Debug.Log(mouseWorldPosition);
	}

}
