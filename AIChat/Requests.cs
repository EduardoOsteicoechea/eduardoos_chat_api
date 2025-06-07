using System.Text;

namespace eduardoos_chat_api;

public static class Requests
{
    private static readonly HttpClient _httpClient = new HttpClient();
    public async static Task<string> PostAuthorizedJsonResponse(string url, string apiKey, object requestBody)
    {
        return await Wrappers.ManagedCommand<Task<string>>(async () =>
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, url);

            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

            requestMessage.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);

            Console.WriteLine($"requestBody:{jsonContent}");
            Console.WriteLine();

            requestMessage.Content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            using HttpResponseMessage httpResponseMessage = await _httpClient.SendAsync(requestMessage);

            string apiResponse = await httpResponseMessage.Content.ReadAsStringAsync();

            Console.WriteLine($"AIApiResponse:\n{apiResponse}");
            Console.WriteLine();

            return apiResponse;
        });
    }

    public async static Task<string> GetJsonResponse(string url)
    {
        return await Wrappers.ManagedCommand<Task<string>>(async () =>
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(
              HttpMethod.Get,
              url
            );

            requestMessage.Headers.Accept.Add(
              new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
            );

            using HttpResponseMessage httpResponseMessage = await _httpClient.SendAsync(requestMessage);

            string apiResponse = await httpResponseMessage.Content.ReadAsStringAsync();

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"API request failed with status code {httpResponseMessage.StatusCode}. Response:\n\t{apiResponse}");
            }

            Console.WriteLine($"StaticDataApiResponse:\n{apiResponse}");
            Console.WriteLine();

            return apiResponse;
        });
    }

    public async static Task<string> GetTextResponse(string url)
    {
        return await Wrappers.ManagedCommand<Task<string>>(async () =>
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(
              HttpMethod.Get,
              url
            );

            requestMessage.Headers.Accept.Add(
              new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/text")
            );

            using HttpResponseMessage httpResponseMessage = await _httpClient.SendAsync(requestMessage);

            string apiResponse = await httpResponseMessage.Content.ReadAsStringAsync();

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"API request failed with status code {httpResponseMessage.StatusCode}. Response:\n\t{apiResponse}");
            }

            Console.WriteLine($"StaticDataApiResponse:\n{apiResponse}");
            Console.WriteLine();

            return apiResponse;
        });
    }
}