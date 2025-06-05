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
    public PhotoCameraManager _photoCameraManager;
    public PlayerController _playerController;

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
    bool _isTakingPhoto = false;
    private async void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            _reticleManager.DisableReticle();
            _photoCameraManager.EnableSecondCamTracking();
        }
        if(Input.GetKeyDown(KeyCode.T))
        {
            _reticleManager.EnableReticle();
            _photoCameraManager.DisableSecondCamTracking();
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            _reticleManager.EnableReticle();
            _ = _detectableObjectsManager.TakePhoto();
            _photoCameraManager.DisableSecondCamTracking();
        }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            _playerController.StopMovementAndCamera = true;
            _pinUpBoardManager.EnablePinUpBoard();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _playerController.StopMovementAndCamera = false;
            _pinUpBoardManager.DisablePinUpBoard();
        }
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
