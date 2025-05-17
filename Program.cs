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

app.MapPost("/chatbot/about/eduardo", async (HttpContext context) =>
{
  return await Task.FromResult<IResult>(await AIAssistantResponseHandler.HandleAIChat(context, new AboutEduardoChatResponseManager()));
});

app.UseCors("AllowLocalhostReact");
app.Run();