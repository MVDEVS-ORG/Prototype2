using System;
using UnityEngine;
using UnityEngine.Events;

public class Interactiontrigger : MonoBehaviour, IInteractable
{
    public InteractionType InteractionType;
    InteractionType IInteractable.InteractionType => this.InteractionType;

    public UnityEvent InteractionCall;

    void IInteractable.Interact()
    {
        if (InteractionCall!=null)
        {
            InteractionCall?.Invoke();
        }
    }
}
