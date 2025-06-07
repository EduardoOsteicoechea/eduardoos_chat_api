namespace eduardoos_chat_api;

public static class ApiKeyManager
{
    public static string GetKey()
    {
        return Wrappers.ManagedCommand<string>(() =>
        {
            string? key = Environment.GetEnvironmentVariable("DEEPSEEK_API_KEY");

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("The API Key Wasn't found in the server");
            }

            Console.WriteLine($"Obtained Api Key");

            return key;
        });
    }
}