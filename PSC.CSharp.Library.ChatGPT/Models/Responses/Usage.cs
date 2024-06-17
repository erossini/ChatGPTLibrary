namespace PSC.CSharp.Library.ChatGPT.Models.Responses
{
    /// <summary>
    /// Class Usage.
    /// </summary>
    public class Usage
    {
        /// <summary>
        /// Gets or sets the completion tokens.
        /// </summary>
        /// <value>The completion tokens.</value>
        [JsonPropertyName("completion_tokens")]
        public long CompletionTokens { get; set; }

        /// <summary>
        /// Gets or sets the prompt tokens.
        /// </summary>
        /// <value>The prompt tokens.</value>
        [JsonPropertyName("prompt_tokens")]
        public long PromptTokens { get; set; }

        /// <summary>
        /// Gets or sets the total tokens.
        /// </summary>
        /// <value>The total tokens.</value>
        [JsonPropertyName("total_tokens")]
        public long TotalTokens { get; set; }
    }
}