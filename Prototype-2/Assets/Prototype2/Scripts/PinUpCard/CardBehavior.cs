using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CardBehavior : MonoBehaviour, IDragHandler , IDropHandler
{
    bool drag;
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        drag = eventData.dragging;
    }

    public void OnDrop(PointerEventData eventData)
    {
        drag = false;
    }
}
