using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CardBehavior : MonoBehaviour, IDragHandler , IDropHandler, IPointerClickHandler
{
    bool drag;
    public PinUpCardUI cardUI;
    public void OnDrag(PointerEventData eventData)
    {
        if (!cardUI.inFocus)
        {
            transform.position = eventData.position;
            drag = eventData.dragging;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        drag = false;
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (cardUI.inFocus && eventData.button == PointerEventData.InputButton.Right)
        {
            cardUI.OnSelected();
        }
        Debug.LogError(eventData.button);
        if (!cardUI.inFocus && eventData.button == PointerEventData.InputButton.Right)
        {
            cardUI.OnSelected();
        }
    }
}
