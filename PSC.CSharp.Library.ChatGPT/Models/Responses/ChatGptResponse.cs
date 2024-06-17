namespace PSC.CSharp.Library.ChatGPT.Models.Responses
{
    /// <summary>
    /// Class ChatGptResponse.
    /// </summary>
    public class ChatGptResponse
    {
        /// <summary>
        /// Gets or sets the choices.
        /// </summary>
        /// <value>The choices.</value>
        [JsonPropertyName("choices")]
        public List<Choice>? Choices { get; set; }

        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        /// <value>The created.</value>
        [JsonPropertyName("created")]
        public long Created { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>The error.</value>
        [JsonPropertyName("error")]
        public Error? Error { get; set; }

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

        /// <summary>
        /// Gets or sets the usage.
        /// </summary>
        /// <value>The usage.</value>
        [JsonPropertyName("usage")]
        public Usage? Usage { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="ChatGptResponse"/> is success.
        /// </summary>
        /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
        [JsonPropertyName("success")]
        public bool Success => Error == null;
    }
}