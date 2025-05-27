using UnityEngine;

public interface IInteractable
{
    InteractionType InteractionType { get; }
    void Interact();
}

public enum InteractionType
{
    None,
    Spontaneous,
    Held
}