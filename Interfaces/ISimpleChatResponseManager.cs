namespace eduardoos_chat_api;

public interface ISimpleChatResponseManager
{
    string? ApiKey { get; set; }
    string? ApiKeyName { get; set; }
    string? ApiEndpoint { get; set; }
    string? ModelTunningStatement { get; set; }
    Dictionary<string, string>? RAGStatements { get; set; }
    Task<string> GetResponse(SimpleMessagingChatRequest request);
}

