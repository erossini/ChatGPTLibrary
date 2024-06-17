namespace PSC.CSharp.Library.ChatGPT.Models.Responses
{
    /// <summary>
    /// Class Error.
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        [JsonPropertyName("message")]
        public string? Message { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Gets or sets the parameter.
        /// </summary>
        /// <value>The parameter.</value>
        [JsonPropertyName("param")]
        public object? Param { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>The code.</value>
        [JsonPropertyName("code")]
        public string? Code { get; set; }
    }
}
