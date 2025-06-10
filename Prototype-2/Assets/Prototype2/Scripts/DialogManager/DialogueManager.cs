using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Prototype2.Scripts
{
    public class DialogueManager : MonoBehaviour
    {
        public TMP_Text speakerText;
        public TMP_Text dialogueText;
        public Button nextButton;
        public Transform dialogueManagerUI;

        private Queue<IDialogueLine> dialogueLines;
        private IDialogueProvider dialogueProvider;

        void Start()
        {
            dialogueLines = new Queue<IDialogueLine>();
            nextButton.onClick.AddListener(DisplayNextLine);
            dialogueManagerUI.gameObject.SetActive(false);
        }

        public void StartConversation(string ConversationId)
        {
            dialogueManagerUI.gameObject.SetActive(true);
            dialogueProvider = new JsonDialogueProvider("dialogues");
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            GameManager.Instance._playerController.StopMovementAndCamera = true;
            StartDialogue(ConversationId);
        }
        public void StartDialogue(string conversationId)
        {
            IConversation conversation = dialogueProvider.GetConversationById(conversationId);

            if (conversation != null)
            {
                dialogueLines.Clear();
                foreach (var line in conversation.Lines)
                {
                    dialogueLines.Enqueue(line);
                }

                DisplayNextLine();
            }
            else
            {
                Debug.LogWarning($"Conversation ID {conversationId} not found.");
            }
        }

        public void DisplayNextLine()
        {
            if (dialogueLines.Count == 0)
            {
                EndDialogue();
                return;
            }

            IDialogueLine line = dialogueLines.Dequeue();
            speakerText.text = line.Speaker;
            dialogueText.text = line.Text;
        }

        void EndDialogue()
        {
            speakerText.text = "";
            dialogueText.text = "";
            Debug.Log("Dialogue ended.");
            dialogueManagerUI.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
            GameManager.Instance._playerController.StopMovementAndCamera = false;
        }
    }
}