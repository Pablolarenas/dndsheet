using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NavButtonTrigger : MonoBehaviour, IPointerClickHandler
{
    public UiState whenPressedChangeTo;

    public void OnPointerClick(PointerEventData eventData)
    {
        FindObjectOfType<UiManager>().SetNewUiState(whenPressedChangeTo);
    }
}
