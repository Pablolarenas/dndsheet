using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeController : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    private float startingPoint;
    private float finishingPoint;
    private float sensitivity;
    private UiManager uiManager;

    private void Awake()
    {
        uiManager = FindObjectOfType<UiManager>();
        sensitivity = uiManager.Sensitivity;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startingPoint = Input.mousePosition.x;
        Debug.Log(startingPoint);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        finishingPoint = Input.mousePosition.x;
        Debug.Log(finishingPoint);

        if (Mathf.Abs(startingPoint - finishingPoint) < sensitivity) return;

        if (startingPoint < finishingPoint)
        {
            uiManager.SwipeLeft();
        }
        else
        {
            uiManager.SwipeRight();
        }
    }
}
