namespace eduardoos_chat_api;

public static class Wrappers
{
    public static void ManagedCommand(Action action)
    {
        try
        {
            action.Invoke();
        }
        catch (System.Exception e)
        {
            HandleError(e);
        }
    }

    public static T ManagedCommand<T>(Func<T> action)
    {
        try
        {
            T reuslt = action.Invoke();
            return reuslt;
        }
        catch (System.Exception e)
        {
            HandleError(e);
            return default!;
        }
     }

    public async static Task<T> ManagedCommandAsync<T>(Func<Task<T>> action)
    {
        try
        {
            T reuslt = await action.Invoke();
            return reuslt;
        }
        catch (System.Exception e)
        {
            HandleError(e);
            return default!;
        }
     }

    public static void HandleError(System.Exception e)
    {
        Console.WriteLine($"{DateTime.Now}\n\n{e.Message}\n\n{e.StackTrace}");
    }
}