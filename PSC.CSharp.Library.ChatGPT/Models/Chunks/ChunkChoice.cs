namespace PSC.CSharp.Library.ChatGPT.Models.Chunks
{
    /// <summary>
    /// Class ChunkChoice.
    /// </summary>
    public class ChunkChoice
    {
        /// <summary>
        /// Gets or sets the delta.
        /// </summary>
        /// <value>The delta.</value>
        [JsonPropertyName("delta")]
        public Delta? Delta { get; set; }

        /// <summary>
        /// Gets or sets the finish reason.
        /// </summary>
        /// <value>The finish reason.</value>
        [JsonPropertyName("finish_reason")]
        public object? FinishReason { get; set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>The index.</value>
        [JsonPropertyName("index")]
        public long Index { get; set; }
    }
}