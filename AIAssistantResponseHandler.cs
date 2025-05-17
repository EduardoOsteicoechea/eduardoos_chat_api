using Microsoft.AspNetCore.Http.HttpResults;

namespace eduardoos_chat_api;

public static class AIAssistantResponseHandler
{
	public async static Task<IResult> HandleAIChat(HttpContext context, ISimpleChatResponseManager responseManager)
	{
		try
		{
			Console.WriteLine($"Starting request processing at: {DateTime.Now}");

			SimpleMessagingChatRequest? request = await context.Request.ReadFromJsonAsync<SimpleMessagingChatRequest>();
			
			Console.WriteLine($"After deserialization:");
			if (request!.previous_messages == null)
			{
				Console.WriteLine("PreviousMessages is null.");
			}
			else if (request.previous_messages.Length == 0)
			{
				Console.WriteLine("PreviousMessages is an empty array.");
			}
			else
			{
				Console.WriteLine($"PreviousMessages count: {request.previous_messages.Length}");
				foreach (var prevMessage in request.previous_messages)
				{
					Console.WriteLine($"  Role: {prevMessage.Role}, Content: {prevMessage.Content}");
				}
			}

			Console.WriteLine($"Message - Role: {request.message?.Role}, Content: {request.message?.Content}");
			// context.Response.ContentType = "application/text";
			
			string aboutEduardoChatResponse = await responseManager.GetResponse(request);
			Console.WriteLine($"Message Before Api Return: {aboutEduardoChatResponse}");

			return Results.Text(aboutEduardoChatResponse);
		}
		catch (System.Exception exception)
		{
			return Results.BadRequest(exception.Message);
		}
	}
}