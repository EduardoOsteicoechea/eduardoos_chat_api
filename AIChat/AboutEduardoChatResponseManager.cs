using System.Text;

namespace eduardoos_chat_api;

public class AboutEduardoChatResponseManager : ISimpleChatResponseManager
{
    public string? ApiKeyName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string? ApiEndpoint { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public string? ModelTunningStatement { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public Dictionary<string, string>? RAGStatements { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    private string? ApiKey { get; set; }
    string? ISimpleChatResponseManager.ApiKey { get => ApiKey; set => ApiKey = value; }

    public async Task<string> GetResponse(SimpleMessagingChatRequest request)
    {
        ApiKey = ApiKeyManager.GetKey();

        DeepSeekRequestBodyModel deepSeekRequestBodyModel = await DeepSeek.StructuredTunnedRagMessage(
            "https://eduardoos.com/static_data/about_eduardo/model_tunning",
            request.message!,
            request.previous_messages
        );

        string apiResponse = await Requests.PostAuthorizedJsonResponse(
            "https://api.deepseek.com/chat/completions",
            ApiKey,
            deepSeekRequestBodyModel
        );

        return DeepSeek.BasicResponseDestructuring(apiResponse);
    }

    ////////////////////////////
    ////////////////////////////
    /// CLASS END
    ////////////////////////////
    ////////////////////////////
}