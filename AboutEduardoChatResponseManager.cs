using System.Text;

namespace eduardoos_chat_api;

public class AboutEduardoChatResponseManager
{
  private string ApiKey { get; set; }
  public async Task<List<DeepSeekChatMessageModel>> GetResponse(AboutEduardoChatRequest request)
  {
    try
    {
      ApiKey = Environment.GetEnvironmentVariable("DEEPSEEK_API_KEY"); Console.WriteLine($"Obtained Api Key");
    }
    catch (System.Exception exception)
    {
      Console.WriteLine($"Error:\n\nApiKey = Environment.GetEnvironmentVariable(\"DEEPSEEK_API_KEY\");\n\n{exception.Message}");
    }

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

      DeepSeekChatMessageModel apiContiguratorMessage = new DeepSeekChatMessageModel()
      {
        Role = "system",
        Content = $"You are a helpful assistant. Avoid phrases like \"based on the provided context\" and \"This individual\". Talk as if you know Eduardo from the university and you are his professional representant. Talk naturally and in a relaxed but formal manner. The signature character of this person is a relaxed and formal professional. Do not include the name of the person asking the questions. Give concise and direct answers. Do not provide analysis hints of your context evaluation process when parsing the context. Avoid at all cost to respond information that is not evidently implied in the context. Say that you do not know if you do not have the answer.",
      };

      List<DeepSeekChatMessageModel> chatMessages = [];
      chatMessages.Add(apiContiguratorMessage);

      try
      {
        if (request.previous_messages != null && request.previous_messages.Length > 0)
        {
          chatMessages.AddRange(request.previous_messages);
        }
        chatMessages.Add(request.message);

        deepSeekRequestBodyModel.Messages = chatMessages.ToArray();

        string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(deepSeekRequestBodyModel);

        Console.WriteLine($"deepSeekRequestBodyModel:\n{deepSeekRequestBodyModel}");

        requestMessage.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        using (HttpResponseMessage httpResponseMessage = await client.SendAsync(requestMessage))
        {
          string apiResponse = await httpResponseMessage.Content.ReadAsStringAsync();

          DeepSeekResponseModel deepSeekResponseModel = Newtonsoft.Json.JsonConvert.DeserializeObject<DeepSeekResponseModel>(apiResponse);

          DeepSeekResponseChoiceModel choice = deepSeekResponseModel.Choices.FirstOrDefault();
          DeepSeekChatMessageModel responseMessage = new DeepSeekChatMessageModel();
          responseMessage.Content = choice.Message.Content;
          responseMessage.Role = "bot";
          chatMessages.Add(responseMessage);

          return chatMessages;
        }
      }
      catch (System.Exception exception)
      {
        Console.WriteLine($"{exception.Message}\n{exception.StackTrace}");
        return [];
      }
    }
  }
}

