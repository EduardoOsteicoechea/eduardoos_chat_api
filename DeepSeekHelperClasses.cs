using Newtonsoft.Json;

namespace eduardoos_chat_api;

public enum DeepSeekModels
{
  DeepSeekChat = 0,
  DeepSeekReasoner = 1
}
public enum DeepSeekMessagesRoles
{
  System = 0,
  User = 1
}
public class DeepSeekRequestBodyModel
{
  [JsonProperty("model")]
  public string Model { get; set; }

  [JsonProperty("messages")]
  public DeepSeekChatMessageModel[] Messages { get; set; }

  [JsonProperty("stream")]
  public bool Stream { get; set; }
}

public class DeepSeekResponseModel
{
  [JsonProperty("id")]
  public Guid Id { get; set; }

  [JsonProperty("object")]
  public string Object { get; set; }

  [JsonProperty("created")]
  public long Created { get; set; }

  [JsonProperty("model")]
  public string Model { get; set; }

  [JsonProperty("choices")]
  public List<DeepSeekResponseChoiceModel> Choices { get; set; }

  [JsonProperty("usage")]
  public DeepSeekAPIAccountUsage Usage { get; set; }

  [JsonProperty("system_fingerprint")]
  public string SystemFingerprint { get; set; }
}

public class DeepSeekResponseChoiceModel
{
  [JsonProperty("index")]
  public int Index { get; set; }

  [JsonProperty("message")]
  public DeepSeekChatMessageModel Message { get; set; }

  [JsonProperty("logprobs")]
  public object Logprobs { get; set; } // Can be null, using object for flexibility

  [JsonProperty("finish_reason")]
  public string FinishReason { get; set; }
}

public class DeepSeekChatMessageModel
{
  [JsonProperty("role")]
  public string Role { get; set; }

  [JsonProperty("content")]
  public string Content { get; set; }
}

public class DeepSeekAPIAccountUsage
{
  [JsonProperty("prompt_tokens")]
  public int PromptTokens { get; set; }

  [JsonProperty("completion_tokens")]
  public int CompletionTokens { get; set; }

  [JsonProperty("total_tokens")]
  public int TotalTokens { get; set; }

  [JsonProperty("prompt_tokens_details")]
  public DeepSeekResponsePromptTokensDetailsModel PromptTokensDetails { get; set; }

  [JsonProperty("prompt_cache_hit_tokens")]
  public int PromptCacheHitTokens { get; set; }

  [JsonProperty("prompt_cache_miss_tokens")]
  public int PromptCacheMissTokens { get; set; }
}

public class DeepSeekResponsePromptTokensDetailsModel
{
  [JsonProperty("cached_tokens")]
  public int CachedTokens { get; set; }
}