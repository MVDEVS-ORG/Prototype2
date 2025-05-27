using Assets.Prototype2.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Prototype2.Scripts
{
    public class JsonDialogueProvider : IDialogueProvider
    {
        private List<IConversation> conversations;

        public JsonDialogueProvider(string fileName)
        {
            TextAsset jsonFile = Resources.Load<TextAsset>(fileName);
            DialogueDataJson data = JsonUtility.FromJson<DialogueDataJson>(jsonFile.text);
            conversations = new List<IConversation>(data.conversations);
        }

        public List<IConversation> GetAllConversations()
        {
            return conversations;
        }

        public IConversation GetConversationById(string id)
        {
            return conversations.Find(c => c.Id == id);
        }
    }
}