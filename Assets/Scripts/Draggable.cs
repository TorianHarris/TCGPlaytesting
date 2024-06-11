using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public bool isDraggable;
    public bool mouseOver;
    public bool hasMoved;
    private CanvasGroup canvasGroup;
    private Transform origParent;

    void Start ()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        origParent = transform.parent;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && mouseOver && hasMoved)
        {
            Destroy(gameObject);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        print("begin drag");
        if(!hasMoved)
            Instantiate(gameObject, origParent);
        transform.SetParent(origParent);
        canvasGroup.blocksRaycasts = false;
        hasMoved = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("end drag");
        canvasGroup.blocksRaycasts = true;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
    }
}
