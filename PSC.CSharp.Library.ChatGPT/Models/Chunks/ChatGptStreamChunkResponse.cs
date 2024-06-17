namespace PSC.CSharp.Library.ChatGPT.Models.Chunks;

/// <summary>
/// Class ChatGptStreamChunkResponse.
/// </summary>
public class ChatGptStreamChunkResponse
{
    /// <summary>
    /// Gets or sets the choices.
    /// </summary>
    /// <value>The choices.</value>
    [JsonPropertyName("choices")]
    public List<ChunkChoice>? Choices { get; set; }

    /// <summary>
    /// Gets or sets the created.
    /// </summary>
    /// <value>The created.</value>
    [JsonPropertyName("created")]
    public long Created { get; set; }

    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// Gets or sets the model.
    /// </summary>
    /// <value>The model.</value>
    [JsonPropertyName("model")]
    public string? Model { get; set; }

    /// <summary>
    /// Gets or sets the object.
    /// </summary>
    /// <value>The object.</value>
    [JsonPropertyName("object")]
    public string? Object { get; set; }
}