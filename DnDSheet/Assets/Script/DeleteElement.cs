using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeleteElement : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject elementToErase;

    public void OnPointerClick(PointerEventData eventData)
    {
        Destroy(elementToErase);
    }
}
