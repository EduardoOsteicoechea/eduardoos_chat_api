using Newtonsoft.Json;
using System.Collections.Generic;

namespace eduardoos_chat_api;

/// <summary>
/// Represents the overall configuration data for tuning an inference model.
/// </summary>
public class ModelTuningConfiguration
{
  [JsonProperty("model_tunning_items")]
  public List<string> ModelTuningItems { get; set; } = new List<string>();

  [JsonProperty("structured_output")]
  public StructuredOutput StructuredOutput { get; set; } = new StructuredOutput();

  [JsonProperty("rag_data")]
  public List<RagDataItem> RagData { get; set; } = new List<RagDataItem>();
}

/// <summary>
/// Represents the structured output example for model responses.
/// </summary>
public class StructuredOutput
{
  [JsonProperty("example_input")]
  public string ExampleInput { get; set; } = string.Empty;

  [JsonProperty("example_output")]
  public ExampleOutput ExampleOutput { get; set; } = new ExampleOutput();
}

/// <summary>
/// Represents the format of an example output within the structured output.
/// </summary>
public class ExampleOutput
{
  [JsonProperty("score")]
  public string Score { get; set; } = string.Empty;

  [JsonProperty("assessment")]
  public string Assessment { get; set; } = string.Empty;

  [JsonProperty("action")]
  public string Action { get; set; } = string.Empty;
}

/// <summary>
/// Represents a single item of RAG (Retrieval-Augmented Generation) data.
/// </summary>
public class RagDataItem
{
  [JsonProperty("item_name")]
  public string ItemName { get; set; } = string.Empty;

  [JsonProperty("item_content")]
  public string ItemContent { get; set; } = string.Empty;
}
public class AssessmentResult
{
  public string Score { get; set; }
  public string Assessment { get; set; }
  public string Action { get; set; }
}

public class OutputStructureModel
{
  public string ExampleInput { get; set; }
  public AssessmentResult ExampleOutput { get; set; }
}