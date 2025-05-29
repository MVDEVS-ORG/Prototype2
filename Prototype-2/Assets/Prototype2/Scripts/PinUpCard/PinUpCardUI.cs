using Newtonsoft.Json;
using System.Collections;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PinUpCardUI : MonoBehaviour, IPointerClickHandler
{
    private DescriptiveObject _pinUpCardInfo;

    [Header("UI inside")]
    [SerializeField] private RawImage _photoTaken;
    [SerializeField] private TMP_Text _fixedText;
    [SerializeField] private TMP_Text _speculations;

    [Header("Self UI")]
    [SerializeField] private Rect _selfTransform;
    [SerializeField] private float _shrunkBottomValue;
    [SerializeField] private float _expandedBottomValue;

    [Range(0.1f,3f)][SerializeField] private float shrinkExpandTime;
    private float _internalTimer = 0f;
    private Coroutine _scalingCoroutine = null;
    private FollowScript _followScript = null;
    private CardBehavior _cardMovement;
    private RectTransform _selfRectTransform;

    private void Start()
    {
        _followScript = GetComponent<FollowScript>();
        _selfRectTransform = GetComponent<RectTransform>();
        _cardMovement = _followScript.target.GetComponent<CardBehavior>();
        _fixedText.gameObject.SetActive(false);
        _speculations.gameObject.SetActive(false);
    }

    public void Initialize(DescriptiveObject obj)
    {
        _photoTaken.texture = GameManager.Instance._photoGalleryUI.GetPhoto(obj.Path);
        _fixedText.text = obj.FixedDescription;
        //TODO add the variable text part
    }

    public void OnSelected()
    {
        if (_scalingCoroutine == null)
        {
            _scalingCoroutine = StartCoroutine(ExpandPhoto());
            _followScript.enabled = false;
            _cardMovement.enabled = false;
        }
    }


    IEnumerator ExpandPhoto()
    {
        float initialHeight = _selfTransform.height;
        _internalTimer = 0f;
        while(_internalTimer<=1)
        {
            _selfTransform.height = Mathf.Lerp(initialHeight, _expandedBottomValue, _internalTimer);
            _selfRectTransform.sizeDelta = new Vector2(_selfTransform.width,_selfTransform.height);
            _internalTimer += Time.deltaTime * 1/shrinkExpandTime;
            yield return new WaitForEndOfFrame();
        }
        _selfTransform.height = Mathf.Lerp(initialHeight, _expandedBottomValue, 1);
        _scalingCoroutine = null;
        _fixedText.gameObject.SetActive(true);
        _speculations.gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.LogError(eventData.button);
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnSelected();
        }
    }
}
