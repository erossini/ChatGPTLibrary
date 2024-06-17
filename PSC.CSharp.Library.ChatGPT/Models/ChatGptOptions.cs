using PSC.CSharp.Library.ChatGPT.Enums;

namespace PSC.CSharp.Library.ChatGPT.Models;

/// <summary>
/// Class ChatGptOptions.
/// </summary>
public class ChatGptOptions
{
    public ChatGptOptions()
    {
        Model = ModelValue.GPT35Turbo.Value;
    }

    /// <summary>
    /// Gets or sets the base URL.
    /// </summary>
    /// <value>The base URL.</value>
    public string BaseUrl { get; set; } = "https://api.openai.com";

    /// <summary>
    /// Gets or sets the frequency penalty.
    /// </summary>
    /// <remarks>
    /// How much to penalize new tokens based on their existing frequency in the text so far. Decreases the model's likelihood to repeat the same line verbatim.
    /// </remarks>
    /// <value>The frequency penalty.</value>
    public double FrequencyPenalty { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the maximum tokens.
    /// </summary>
    /// <remarks>
    /// The maximum number of tokens to <strong>generate</strong> shared between the prompt and completion. The exact limit varies by model. (One token is roughly 4 characters for standard English text)
    /// </remarks>
    /// <value>The maximum tokens.</value>
    public long MaxTokens { get; set; } = 256;

    /// <summary>
    /// Gets or sets the model.
    /// </summary>
    /// <value>The model.</value>
    public string? Model { get; set; }

    /// <summary>
    /// Gets or sets the presence penalty.
    /// </summary>
    /// <remarks>
    /// How much to penalize new tokens based on whether they appear in the text so far. Increases the model's likelihood to talk about new topics.
    /// </remarks>
    /// <value>The presence penalty.</value>
    public double PresencePenalty { get; set; } = 0.0;

    /// <summary>
    /// Gets or sets the stop.
    /// </summary>
    /// <remarks>
    /// Up to four sequences where the API will stop generating further tokens. The returned text will not contain the stop sequence.
    /// </remarks>
    /// <value>The stop.</value>
    public string[]? Stop { get; set; } = null;

    /// <summary>
    /// Gets or sets the temperature.
    /// </summary>
    /// <remarks>
    /// Controls randomness: Lowering results in less random completions. As the temperature approaches zero, the model will become deterministic and repetitive.
    /// </remarks>
    /// <value>The temperature.</value>
    public double Temperature { get; set; } = 0.7;

    /// <summary>
    /// Gets or sets the top p.
    /// </summary>
    /// <remarks>
    /// Controls diversity via nucleus sampling: 0.5 means half of all likelihood-weighted options are considered.
    /// </remarks>
    /// <value>The top p.</value>
    public double TopP { get; set; } = 0.9;
}