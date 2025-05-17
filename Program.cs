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

// app.MapPost("/chatbot/about/eduardo", async (HttpContext context) =>
// {
//   try
//   {
//     SimpleMessagingChatRequest request = await context.Request.ReadFromJsonAsync<SimpleMessagingChatRequest>();

//     Console.WriteLine($"After deserialization:");
//     if (request!.previous_messages == null)
//     {
//       Console.WriteLine("PreviousMessages is null.");
//     }
//     else if (request.previous_messages.Length == 0)
//     {
//       Console.WriteLine("PreviousMessages is an empty array.");
//     }
//     else
//     {
//       Console.WriteLine($"PreviousMessages count: {request.previous_messages.Length}");
//       foreach (var prevMessage in request.previous_messages)
//       {
//         Console.WriteLine($"  Role: {prevMessage.Role}, Content: {prevMessage.Content}");
//       }
//     }
//     Console.WriteLine($"Message - Role: {request.message.Role}, Content: {request.message.Content}");

//     AboutEduardoChatResponseManager aboutEduardoChatResponseManager = new();

//     DeepSeekChatMessageModel aboutEduardoChatResponse = await aboutEduardoChatResponseManager.GetResponse(request);

//     string response = Newtonsoft.Json.JsonConvert.SerializeObject(aboutEduardoChatResponse);

//     context.Response.ContentType = "application/json";

//     return Results.Json(aboutEduardoChatResponse); 
//   }
//   catch (System.Exception exception)
//   {
//     return Results.BadRequest(exception.Message);
//   }
// });

app.MapPost("/chatbot/about/eduardo", async context =>
{
  await AIAssistantResponseHandler.HandleAIChat(context, new AboutEduardoChatResponseManager());
});

app.UseCors("AllowLocalhostReact");
app.Run();