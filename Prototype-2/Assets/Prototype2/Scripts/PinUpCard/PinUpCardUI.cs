using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    private string _originalSpeculationsWithBlanks;
    List<int> _blanks = new();
    List<string> _selectedWords = new();
    [SerializeField] private Transform _KeyWordsTransform;
    [SerializeField] private Button _Button;

    [Header("Self UI")]
    [SerializeField] private Rect _selfTransform;
    [SerializeField] private float _shrunkBottomValue;
    [SerializeField] private float _expandedBottomValue;
    private Action<String> click;

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
        _KeyWordsTransform.gameObject.SetActive(false);
    }

    public void Initialize(DescriptiveObject obj)
    {
        Texture newTex = GameManager.Instance._photoGalleryUI.GetPhoto(obj.Path);
        if (newTex != null)
        {
            _photoTaken.texture = newTex;
            _fixedText.text = obj.FixedDescription;
            _speculations.text = obj.Speculations;
            _originalSpeculationsWithBlanks = obj.Speculations;
            int length = obj.Speculations.Length;
            for(int i = 0;i<length;i++)
            {
                if (obj.Speculations[i] == '_')
                {
                    _blanks.Add(i);
                    i = i + 3;
                }
            }
            foreach(string words in obj.UnlockWords)
            {
                Button temp = Instantiate(_Button, _KeyWordsTransform);
                temp.gameObject.name = $"button_{words}";
                temp.onClick.AddListener(() => AddWord(words));
                temp.transform.GetChild(0).GetComponent<TMP_Text>().text = words;
            }
            Button t = Instantiate(_Button, _KeyWordsTransform);
            t.gameObject.name = $"button_Clear";
            t.onClick.AddListener(() => RemoveWord());
            t.transform.GetChild(0).GetComponent<TMP_Text>().text = "Clear";
        }
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
        _KeyWordsTransform.gameObject.SetActive(true);
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
        _KeyWordsTransform.gameObject.SetActive(false);
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

    public void AddWord(string word)
    {
        if (_selectedWords.Count<_blanks.Count)
        {
            _selectedWords.Add(word);
            String temp = _speculations.text;
            temp = _speculations.text.Substring(0, _blanks[_selectedWords.Count - 1]);
            temp = temp + word;
            temp = temp + _speculations.text.Substring(_blanks[_selectedWords.Count - 1] + 4);
            if (_selectedWords.Count < _blanks.Count)
            {
                _blanks[_selectedWords.Count] = _blanks[_selectedWords.Count] - 4 + word.Length;
            }
            _speculations.text = temp;
        }
    }

    public void RemoveWord()
    {
        if(_selectedWords.Count>0)
        {
            _selectedWords.Clear();
            string temp = _originalSpeculationsWithBlanks;
            _speculations.text = _originalSpeculationsWithBlanks;
            _blanks.Clear();
            int length = _speculations.text.Length;
            for (int i = 0; i < length; i++)
            {
                if (_speculations.text[i] == '_')
                {
                    _blanks.Add(i);
                    i = i + 3;
                }
            }
        }
    }
}
