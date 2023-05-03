using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drawer : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler , IPointerUpHandler
{
    [SerializeField] private GameObject prefabObj;
	[SerializeField] private GameObject DragImage;
	private GameObject objToInstatiate;
	private RectTransform rect; //dragged items rect transform
	private CanvasGroup draggedCanvasGroup; // dragged items canvas group
	private GameObject mycanvasGameObject;
	private Canvas mycanvas;

	private void Awake()
	{

		rect = GetComponent<RectTransform>();
		mycanvasGameObject = GameObject.Find("Canvas");
		mycanvas = mycanvasGameObject.GetComponent<Canvas>();
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		GameObject imgToDrag = Instantiate(DragImage);
		imgToDrag.transform.SetParent(mycanvas.transform,false);
		imgToDrag.transform.position = eventData.pointerDrag.transform.position;
		eventData.pointerDrag = imgToDrag;
		
		eventData.pointerDrag.AddComponent<CanvasGroup>();
		draggedCanvasGroup = eventData.pointerDrag.GetComponent<CanvasGroup>();
		draggedCanvasGroup.interactable = true;
		draggedCanvasGroup.alpha = .6f;
		draggedCanvasGroup.blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData)
	{
		rect.anchoredPosition += eventData.delta / mycanvas.scaleFactor;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		draggedCanvasGroup = eventData.pointerDrag.GetComponent<CanvasGroup>();
		draggedCanvasGroup.alpha = 1f;
		draggedCanvasGroup.blocksRaycasts = true;
		Destroy(eventData.pointerDrag);

		objToInstatiate = Instantiate(prefabObj);
		objToInstatiate.name = prefabObj.name;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
        Debug.Log("clicked");
    }

	public void OnPointerUp(PointerEventData eventData)
	{
		//objToInstatiate = Instantiate(prefabObj);
		//objToInstatiate.name = prefabObj.name;
	}
}
