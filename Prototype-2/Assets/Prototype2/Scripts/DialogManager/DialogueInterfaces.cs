using System.Collections.Generic;

public interface IDialogueLine
{
    string Speaker { get; }
    string Text { get; }
}

public interface IConversation
{
    string Id { get; }
    List<IDialogueLine> Lines { get; }
}

public interface IDialogueProvider
{
    List<IConversation> GetAllConversations();
    IConversation GetConversationById(string id);
}
