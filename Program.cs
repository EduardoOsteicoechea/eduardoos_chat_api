using eduardoos_chat_api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowLocalhostReact", policy =>
  {
    policy.WithOrigins("http://localhost:5173")
    .AllowAnyHeader()
    .AllowAnyMethod();
  });
});

var app = builder.Build();

app.MapGet("/chatbot", () => "Hello World!");

app.MapPost("/chatbot/about/eduardo", async context =>
{
  // try
  // {
  //   Console.WriteLine($"Starting request processing at: {DateTime.Now}");

  //   SimpleMessagingChatRequest? request = await context.Request.ReadFromJsonAsync<SimpleMessagingChatRequest>();

  //   Console.WriteLine($"After deserialization:");
  //   if (request!.previous_messages == null)
  //   {
  //     Console.WriteLine("PreviousMessages is null.");
  //   }
  //   else if (request.previous_messages.Length == 0)
  //   {
  //     Console.WriteLine("PreviousMessages is an empty array.");
  //   }
  //   else
  //   {
  //     Console.WriteLine($"PreviousMessages count: {request.previous_messages.Length}");
  //     foreach (var prevMessage in request.previous_messages)
  //     {
  //       Console.WriteLine($"  Role: {prevMessage.Role}, Content: {prevMessage.Content}");
  //     }
  //   }

  //   Console.WriteLine($"Message - Role: {request.message?.Role}, Content: {request.message?.Content}");

  //   // string aboutEduardoChatResponse = await new AboutEduardoChatResponseManager().GetResponse(request);
  //   // Console.WriteLine($"Message Before Api Return: {aboutEduardoChatResponse}");

  //   // Results.Text(aboutEduardoChatResponse);

  //   string hardcodedResponse = "This is a hardcoded profile response.";
  //   Console.WriteLine($"Message Before Api Return: {hardcodedResponse}");

  //   Results.Text(hardcodedResponse, contentType: "application/text");
  // }
  // catch (System.Exception exception)
  // {
  //   Results.BadRequest(exception.Message);
  // }

  Results.Ok("Great!");
});

app.UseCors("AllowLocalhostReact");
app.Run();