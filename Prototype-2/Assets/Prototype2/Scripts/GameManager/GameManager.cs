using Assets.Prototype2.Scripts;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Services")]
    public ReticleManager _reticleManager;
    [SerializeField] private DialogueManager _dialogueManager;
    public DetectableObjectsManager _detectableObjectsManager;
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
    private void Update()
    {
        //Temporary Code to check Dialogues
        /*if (Input.GetMouseButtonDown(0) && !isStarted)
        {
            isStarted = true;
            _dialogueManager.StartConversation();
        }*/
    }


}
