using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ReticleManager : MonoBehaviour
{
    [Header("Reticles")]
    [Tooltip("0: normal \n 1: Sponataneous \n 2: Held")]
    [SerializeField] private List<GameObject> Reticles = new();
    [SerializeField] private Image _heldReticle;
    [SerializeField] private float _fillTime;
    private GameObject _currentReticle = null;
    public InteractionType CurrentReticleType = InteractionType.None;

    public void Start()
    {
        _currentReticle = Reticles[(int)InteractionType.None];
        CurrentReticleType = InteractionType.None;
    }

    public void DisableReticle()
    {
        gameObject.SetActive(false);
    }


    public void EnableReticle()
    {
        gameObject.SetActive(true);
    }

    public void ChangeReticle(InteractionType typeOfInteraction)
    {
        try
        {
            _currentReticle.SetActive(false);
            CurrentReticleType = typeOfInteraction;
            _currentReticle = Reticles[(int)CurrentReticleType];
            _currentReticle.SetActive(true);
        }
        catch(Exception exception)
        {
            Debug.LogError(exception);
        }
    }

    private void FixedUpdate()
    {
        if (Reticles.IndexOf(_currentReticle)== (int)InteractionType.Held)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                _heldReticle.fillAmount = MathF.Min(1, _heldReticle.fillAmount + (Time.fixedDeltaTime * 1 / _fillTime));
            }
            else
            {
                _heldReticle.fillAmount = MathF.Max(0, _heldReticle.fillAmount - (Time.fixedDeltaTime * 1 / _fillTime));
            }
        }
    }

    public void ResetCircle()
    {
        _heldReticle.fillAmount = 0;
    }

    public bool IsCircledFilled()
    {
        return _heldReticle.fillAmount == 1;
    }
}
