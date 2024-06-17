namespace PSC.CSharp.Library.ChatGPT.Models;

/// <summary>
/// Class ChatGptConversation.
/// </summary>
public class ChatGptConversation
{
    /// <summary>
    /// Gets or sets the created.
    /// </summary>
    /// <value>The created.</value>
    public DateTime Created { get; set; } = DateTime.Now;

    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>The identifier.</value>
    public string? Id { get; set; } = Guid.NewGuid().ToString();
    /// <summary>
    /// Gets or sets the messages.
    /// </summary>
    /// <value>The messages.</value>
    public List<ChatGptMessage>? Messages { get; set; } = new();

    /// <summary>
    /// Gets or sets the updated.
    /// </summary>
    /// <value>The updated.</value>
    public DateTime Updated { get; set; } = DateTime.Now;
}