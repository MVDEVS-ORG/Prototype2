using Newtonsoft.Json;
using System.Collections;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PinUpCardUI : MonoBehaviour
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

    [Range(0.1f, 3f)][SerializeField] private float shrinkExpandTime;
    private float _internalTimer = 0f;
    private Coroutine _scalingCoroutine = null;
    private FollowScript _followScript = null;
    private CardBehavior _cardMovement;
    private RectTransform _selfRectTransform;

    private Vector2 OriginalPos;
    [SerializeField] private Vector2 CenterPos;
    [Range(0.1f, 3f)][SerializeField] private float moveTime;

    public bool inFocus;

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
            _scalingCoroutine = StartCoroutine(!inFocus?ExpandPhoto():CollapsePhoto());
            _followScript.enabled = false;
            _cardMovement.enabled = false;
        }
    }


    IEnumerator ExpandPhoto()
    {
        OriginalPos = _selfRectTransform.localPosition;
        _internalTimer = 0f;
        while (_internalTimer<=1)
        {
            _selfRectTransform.anchoredPosition = Vector2.Lerp(OriginalPos, CenterPos, _internalTimer);
            _internalTimer += Time.deltaTime * 1 / moveTime;
            yield return new WaitForEndOfFrame();
        }
        float initialHeight = _selfRectTransform.rect.height;
        float initialWidth = _selfRectTransform.rect.width;
        _internalTimer = 0f;
        while (_internalTimer <= 1)
        {
            _selfTransform.height = Mathf.Lerp(initialHeight, _expandedBottomValue, _internalTimer);
            _selfRectTransform.sizeDelta = new Vector2(initialWidth, _selfTransform.height);
            _internalTimer += Time.deltaTime * 1 / shrinkExpandTime;
            yield return new WaitForEndOfFrame();
        }
        _selfTransform.height = Mathf.Lerp(initialHeight, _expandedBottomValue, 1);
        _scalingCoroutine = null;
        _fixedText.gameObject.SetActive(true);
        _speculations.gameObject.SetActive(true);
        _internalTimer = 0f;
        _followScript.enabled = true;
        _cardMovement.enabled = true;
        inFocus = true;
    }

    IEnumerator CollapsePhoto()
    {
        float initialHeight = _selfRectTransform.rect.height;
        float initialWidth = _selfRectTransform.rect.width;
        
        _fixedText.gameObject.SetActive(false);
        _speculations.gameObject.SetActive(false);
        _internalTimer = 0f;
        while (_internalTimer <= 1)
        {
            _selfTransform.height = Mathf.Lerp(initialHeight, _shrunkBottomValue, _internalTimer);
            _selfRectTransform.sizeDelta = new Vector2(initialWidth, _selfTransform.height);
            _internalTimer += Time.deltaTime * 1 / shrinkExpandTime;
            yield return new WaitForEndOfFrame();
        }
        _selfTransform.height = Mathf.Lerp(initialHeight, _expandedBottomValue, 1);
        _scalingCoroutine = null;
        _internalTimer = 0f;
        while (_internalTimer <= 1)
        {
            _selfRectTransform.anchoredPosition = Vector2.Lerp(CenterPos, OriginalPos, _internalTimer);
            _internalTimer += Time.deltaTime * 1 / moveTime;
            yield return new WaitForEndOfFrame();
        }
        _internalTimer = 0f;
        _followScript.enabled = true;
        _cardMovement.enabled = true;
        yield return new WaitForSeconds(0.1f);
        inFocus = false;
    }
}
