using Assets.Prototype2.Scripts;
using Assets.Prototype2.Scripts.PhotoSaveSystem;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Services")]
    public ReticleManager _reticleManager;
    public DialogueManager _dialogueManager;
    public DetectableObjectsManager _detectableObjectsManager;
    public CameraCapture _cameraCapture;
    public PhotoGalleryUI _photoGalleryUI;
    public PinUpBoardManager _pinUpBoardManager;

    private void Awake()
    {
        if(Instance!=null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    bool _isStarted = false;
    bool _showGallery = false;
    bool _isTakingPhoto = false;
    private void Update()
    {
        //Temporary Code to check Dialogues
        /*if (Input.GetMouseButtonDown(0) && !_isStarted)
        {
            _isStarted = true;
            _dialogueManager.StartConversation();
        }
        if (Input.GetKeyDown(KeyCode.C) && !_isTakingPhoto)
        {
            _isTakingPhoto = true;
            string path = await _cameraCapture.TakePhotoAsync();
            Debug.Log($"Photo saved: {path}");
            _isTakingPhoto = false;
        }
        if (Input.GetKeyDown(KeyCode.I) && !_isTakingPhoto)
        {
            _showGallery = !_showGallery;
            _photoGalleryUI.gameObject.SetActive(_showGallery);
            if (_showGallery)
            {
                _photoGalleryUI.DisplayAllPhotos();
            }
        }*/
    }
}
