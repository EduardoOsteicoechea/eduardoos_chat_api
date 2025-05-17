using System.Text;

namespace eduardoos_chat_api;

public class BetweenTheRoleAndTheTragedyResponseManager : ISimpleChatResponseManager
{
  public string? ApiKeyName { get; set; }
  public string? ApiEndpoint { get; set; }
  public string? ModelTunningStatement { get; set; }
  public Dictionary<string, string>? RAGStatements { get; set; }
  public string? ApiKey { get; set; }
  public IAIChatMessageModel? ApiContiguratorMessage { get; set; }
  public IAIChatMessageModel? ContextConfiguratorMessage { get; set; }

  public async Task<string> GetResponse(SimpleMessagingChatRequest request)
  {
    GetApyKey();
    FineTuneModel();
    ConfigureRAG();

    using (HttpClient client = new HttpClient())
    {
      HttpRequestMessage requestMessage = new HttpRequestMessage(
        HttpMethod.Post,
        "https://api.deepseek.com/chat/completions"
      );

      requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
        "Bearer",
        ApiKey
      );

      requestMessage.Headers.Accept.Add(
        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
      );

      DeepSeekRequestBodyModel deepSeekRequestBodyModel = new DeepSeekRequestBodyModel();
      deepSeekRequestBodyModel.Model = "deepseek-chat";
      deepSeekRequestBodyModel.Stream = false;

      List<IAIChatMessageModel> chatMessages = [];
      chatMessages.Add(ApiContiguratorMessage!);
      chatMessages.Add(ContextConfiguratorMessage!);

      try
      {
        if (request.previous_messages != null && request.previous_messages.Length > 0)
        {
          chatMessages.AddRange(request.previous_messages);
        }
        chatMessages.Add(request.message!);

        deepSeekRequestBodyModel.Messages = chatMessages.ToArray();

        string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(deepSeekRequestBodyModel);

        Console.WriteLine($"deepSeekRequestBodyModel:{deepSeekRequestBodyModel}");

        requestMessage.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        using (HttpResponseMessage httpResponseMessage = await client.SendAsync(requestMessage))
        {
          string apiResponse = await httpResponseMessage.Content.ReadAsStringAsync();

          Console.WriteLine($"AIApiResponse:\n{apiResponse}");
          Console.WriteLine();

          DeepSeekResponseModel deepSeekResponseModel = Newtonsoft.Json.JsonConvert.DeserializeObject<DeepSeekResponseModel>(apiResponse)!;

          DeepSeekResponseChoiceModel choice = deepSeekResponseModel?.Choices?.FirstOrDefault()!;

          string responseMessage = choice.Message!.Content!;

          Console.WriteLine($"eduardoosApiResponse:\n{responseMessage}");
          Console.WriteLine();

          return responseMessage;
        }
      }
      catch (System.Exception exception)
      {
        Console.WriteLine($"{exception.Message}\n{exception.StackTrace}");
        throw;
      }
    }
  }


  public async Task Run(Func<Task> action)
  {
    try
    {
      await action.Invoke();
    }
    catch (System.Exception exception)
    {
      Console.WriteLine($"Error:\n\n{exception.Message}");
      throw;
    }
  }

  public void GetApyKey()
  {
    ApiKey = Environment.GetEnvironmentVariable("DEEPSEEK_API_KEY")!;
    Console.WriteLine($"Obtained Api Key");
  }

  public void FineTuneModel()
  {
    ApiContiguratorMessage = new DeepSeekChatMessageModel()
    {
      Role = "system",
      Content = $@"
  You are a patient theology teacher. Avoid phrases like ""based on the provided context"" and ""This individual"". 
  Your objective is to evaluate the percentage of coherence of the user's response to the last possed question. 
  Do not provide the option of talking about another idea ot topic that is not the last question topic.
  Always, absolutely always, return an estimate in percentage of the accuray of the response with the context.
  If the accuracy is bellow 70%, ask the last question again.
  ",
    };
  }

  public void ConfigureRAG()
  {
    string context = $@"
---
**Theme Title**
Between the Role and the Tragedy: A biblical and practical perspective on women's role in a fallen universe.

";
    ContextConfiguratorMessage = new DeepSeekChatMessageModel()
    {
      Role = "user",
      Content = $"Context:\n${context}"
    };
  }
  ////////////////////////////
  ////////////////////////////
  /// CLASS END
  ////////////////////////////
  ////////////////////////////
}

