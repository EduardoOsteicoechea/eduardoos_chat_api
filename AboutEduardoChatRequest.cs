using Newtonsoft.Json;

namespace eduardoos_chat_api;

public class SimpleMessagingChatRequest
{
  [Newtonsoft.Json.JsonProperty("previous_messages")]
  public DeepSeekChatMessageModel[]? previous_messages {get;set;}

  [Newtonsoft.Json.JsonProperty("message")]
  public DeepSeekChatMessageModel? message {get;set;}
}