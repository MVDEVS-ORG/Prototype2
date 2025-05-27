using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CameraRayInteractor : MonoBehaviour
{
    [SerializeField] private LayerMask _interactionLayerMask;
    [SerializeField] private float _interactionDistance;
    private Coroutine _heldInteraction;
    private bool _interactableInSight=false;

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, _interactionDistance, _interactionLayerMask))
        {
            Debug.DrawRay(transform.position, transform.forward * _interactionDistance, Color.green);
            if (hitInfo.transform.TryGetComponent(out IInteractable interaction))
            {
                _interactableInSight = true;
                if (interaction.InteractionType != InteractionType.Held)
                {
                    if (_heldInteraction != null)
                    {
                        StopCoroutine(_heldInteraction);
                        _heldInteraction = null;
                    }
                }
                switch (interaction.InteractionType)
                {
                    case InteractionType.None:
                        GameManager.Instance._reticleManager.ChangeReticle(InteractionType.None);
                        break;

                    case InteractionType.Spontaneous:
                        GameManager.Instance._reticleManager.ChangeReticle(interaction.InteractionType);
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            interaction.Interact();
                        }
                        break;

                    case InteractionType.Held:
                        GameManager.Instance._reticleManager.ChangeReticle(interaction.InteractionType);
                        if (_heldInteraction == null)
                        {
                            GameManager.Instance._reticleManager.ResetCircle();
                            _heldInteraction = StartCoroutine(interactionHeld(interaction));
                        }
                        break;
                }
            }
        }
        else if (GameManager.Instance._reticleManager.CurrentReticleType != InteractionType.None)
        {
            GameManager.Instance._reticleManager.ChangeReticle(InteractionType.None);
        }
        else if(_heldInteraction!=null)
        {
            _interactableInSight = false;
            StopCoroutine(_heldInteraction);
            _heldInteraction = null;
        }
    }

    private IEnumerator interactionHeld(IInteractable interaction)
    {
        while(!GameManager.Instance._reticleManager.IsCircledFilled())
        {
            yield return new WaitForEndOfFrame();
        }
        interaction.Interact();
    }
}
