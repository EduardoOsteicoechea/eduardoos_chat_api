using System.Text;

namespace eduardoos_chat_api;

public static class LLMS
{

  public static string GenerateRagDataString
  (
      ModelTuningConfiguration ModelTuningConfiguration
  )
  {
    return Wrappers.ManagedCommand<string>(() =>
    {
      StringBuilder context = new();

      foreach (RagDataItem item in ModelTuningConfiguration.RagData)
      {
        context.AppendLine();
        context.AppendLine($"---");
        context.AppendLine($"**{item.ItemName}**");
        context.AppendLine($"**{item.ItemContent}**");
      }
 
      context.AppendLine();

      context.AppendLine();
      context.AppendLine($"EXAMPLE INPUT: {ModelTuningConfiguration.StructuredOutput.ExampleInput}");
      context.AppendLine();
      context.AppendLine();
      context.AppendLine($"EXAMPLE JSON OUTPUT:\n{Newtonsoft.Json.JsonConvert.SerializeObject(ModelTuningConfiguration.StructuredOutput.ExampleOutput)}");

      return context.ToString();
    });
  }
}