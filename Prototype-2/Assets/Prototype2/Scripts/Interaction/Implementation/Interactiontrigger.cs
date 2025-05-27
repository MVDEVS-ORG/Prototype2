using System;
using UnityEngine;

public class Interactiontrigger : MonoBehaviour, IInteractable
{
    public InteractionType InteractionType;
    InteractionType IInteractable.InteractionType => this.InteractionType;

    public event Action InteractionCall;

    void IInteractable.Interact()
    {
        if (InteractionCall!=null)
        {
            InteractionCall?.Invoke();
        }
    }
}
