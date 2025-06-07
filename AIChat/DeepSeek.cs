using System.Text;

namespace eduardoos_chat_api;

public static class DeepSeek
{

  public static string BasicResponseDestructuring
  (
      string apiResponse
  )
  {
    return Wrappers.ManagedCommand<string>(() =>
    {
      DeepSeekResponseModel deepSeekResponseModel = Newtonsoft.Json.JsonConvert.DeserializeObject<DeepSeekResponseModel>(apiResponse)!;

      DeepSeekResponseChoiceModel choice = deepSeekResponseModel?.Choices?.FirstOrDefault()!;

      string responseMessage = choice.Message!.Content!;

      Console.WriteLine($"eduardoosApiResponse:\n{responseMessage}");
      Console.WriteLine();

      return responseMessage;
    });
  }

  public async static Task<DeepSeekRequestBodyModel> StructuredTunnedRagMessage
  (
      string tunningUrl,
      DeepSeekChatMessageModel currentMessage,
      DeepSeekChatMessageModel[]? previousMessages = default
  )
  {
    return await Wrappers.ManagedCommand<Task<DeepSeekRequestBodyModel>>(async () =>
    {
      string rawModelTuningConfiguration = await Requests.GetTextResponse(tunningUrl);

      ModelTuningConfiguration modelTuningConfiguration = Newtonsoft.Json.JsonConvert.DeserializeObject<ModelTuningConfiguration>(rawModelTuningConfiguration)!;

      if (modelTuningConfiguration == null)
      {
        throw new Exception("Invalid Api model tunning response.");
      }

      DeepSeekRequestBodyModel deepSeekRequestBodyModel = new DeepSeekRequestBodyModel()
      {
        Model = "deepseek-chat",
        Stream = false
      };

      DeepSeekChatMessageModel apiContiguratorMessage = new DeepSeekChatMessageModel()
      {
        Role = "system",
        Content = string.Join("\n", modelTuningConfiguration.ModelTuningItems)
      };

      DeepSeekChatMessageModel contextContiguratorMessage = new DeepSeekChatMessageModel()
      {
        Role = "user",
        Content = $"Context:\n${LLMS.GenerateRagDataString(modelTuningConfiguration)}"
      };

      List<DeepSeekChatMessageModel> chatMessages = new()
      {
          apiContiguratorMessage,
          contextContiguratorMessage
      };

      if (previousMessages != null && previousMessages.Length > 0)
      {
        chatMessages.AddRange(previousMessages);
      }

      chatMessages.Add(currentMessage);

      deepSeekRequestBodyModel.Messages = [.. chatMessages];

      return deepSeekRequestBodyModel;
    });
  }
}