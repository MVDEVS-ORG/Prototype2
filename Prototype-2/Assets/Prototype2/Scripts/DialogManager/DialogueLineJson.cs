using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Prototype2.Scripts
{
    [System.Serializable]
    public class DialogueLineJson : IDialogueLine
    {
        public string speaker;
        public string text;

        public string Speaker => speaker;
        public string Text => text;
    }

    [System.Serializable]
    public class ConversationJson : IConversation
    {
        public string id;
        public List<DialogueLineJson> lines;

        public string Id => id;
        public List<IDialogueLine> Lines => lines.ConvertAll(line => (IDialogueLine)line);
    }

    [System.Serializable]
    public class DialogueDataJson
    {
        public List<ConversationJson> conversations;
    }
}