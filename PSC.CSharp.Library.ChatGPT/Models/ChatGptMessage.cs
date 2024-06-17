namespace PSC.CSharp.Library.ChatGPT.Models;

/// <summary>
/// Class ChatGptMessage.
/// </summary>
public class ChatGptMessage
{
    /// <summary>
    /// Gets or sets the content.
    /// </summary>
    /// <value>The content.</value>
    [JsonPropertyName("content")]
    public string? Content { get; set; }

    /// <summary>
    /// Gets or sets the role.
    /// </summary>
    /// <value>The role.</value>
    [JsonPropertyName("role")]
    public string? Role { get; set; } = "user";
}