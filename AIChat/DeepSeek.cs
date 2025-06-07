namespace eduardoos_chat_api;

public static class DeepSeek
{

  public class AssessmentResult
  {
    public string Score { get; set; }
    public string Assessment { get; set; }
    public string Action { get; set; }
  }

  public class OutputStructureModel
  {
    public string ExampleInput { get; set; }
    public AssessmentResult ExampleOutput { get; set; }
  }

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
      string ragUrl,
      string outputStructureUrl,
      DeepSeekChatMessageModel currentMessage,
      DeepSeekChatMessageModel[]? previousMessages = default
  )
  {
    return await Wrappers.ManagedCommand<Task<DeepSeekRequestBodyModel>>(async () =>
    {
      DeepSeekRequestBodyModel deepSeekRequestBodyModel = new DeepSeekRequestBodyModel()
      {
        Model = "deepseek-chat",
        Stream = false
      };

      DeepSeekChatMessageModel apiContiguratorMessage = new DeepSeekChatMessageModel()
      {
        Role = "system",
        Content = await Requests.GetTextResponse(tunningUrl)
      };

      string context = await Requests.GetTextResponse(ragUrl);

      string rawOutputStructureData = await Requests.GetTextResponse(outputStructureUrl);

      OutputStructureModel outputStructureModel = Newtonsoft.Json.JsonConvert.DeserializeObject<OutputStructureModel>(rawOutputStructureData)!;

      AssessmentResult outputStructureModelExampleOutput = outputStructureModel.ExampleOutput!;

      context += $"EXAMPLE INPUT: {outputStructureModel.ExampleInput}";
      context += $"EXAMPLE JSON OUTPUT: {outputStructureModelExampleOutput}";

      DeepSeekChatMessageModel contextContiguratorMessage = new DeepSeekChatMessageModel()
      {
        Role = "user",
        Content = $"Context:\n${context}"
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