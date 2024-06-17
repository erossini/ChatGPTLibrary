namespace PSC.CSharp.Library.ChatGPT.Models.Responses
{
    /// <summary>
    /// Class Choice.
    /// </summary>
    public class Choice
    {
        /// <summary>
        /// Gets or sets the finish reason.
        /// </summary>
        /// <value>The finish reason.</value>
        [JsonPropertyName("finish_reason")]
        public string? FinishReason { get; set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>The index.</value>
        [JsonPropertyName("index")]
        public long Index { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        [JsonPropertyName("message")]
        public ChatGptMessage? Message { get; set; }
    }
}