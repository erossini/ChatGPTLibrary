namespace PSC.CSharp.Library.ChatGPT.Models.Chunks
{
    /// <summary>
    /// Class Delta.
    /// </summary>
    public class Delta
    {
        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        [JsonPropertyName("content")]
        public string? Content { get; set; }
    }
}