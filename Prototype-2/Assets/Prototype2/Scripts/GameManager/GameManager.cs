using Assets.Prototype2.Scripts;
using Assets.Prototype2.Scripts.PhotoSaveSystem;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Services")]
    public ReticleManager _reticleManager;
    [SerializeField] private DialogueManager _dialogueManager;
    [SerializeField] private CameraCapture _cameraCapture;
    [SerializeField] private PhotoGalleryUI _photoGalleryUI;
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
    bool isStarted = false;
    bool showGallery = false;
    private void Update()
    {
        //Temporary Code to check Dialogues
        if (Input.GetMouseButtonDown(0) && !isStarted)
        {
            isStarted = true;
            _dialogueManager.StartConversation();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            _cameraCapture.TakePhoto();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            showGallery = !showGallery;
            _photoGalleryUI.gameObject.SetActive(showGallery);
            if (showGallery)
            {
                _photoGalleryUI.DisplayAllPhotos();
            }
        }
    }


}
