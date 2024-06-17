using PSC.CSharp.Library.ChatGPT.Models;
using PSC.CSharp.Library.ChatGPT.Models.Chunks;
using PSC.CSharp.Library.ChatGPT.Models.Requests;
using PSC.CSharp.Library.ChatGPT.Models.Responses;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace PSC.CSharp.Library.ChatGPT;

/// <summary>
/// Class ChatGpt.
/// </summary>
public class ChatGpt
{
    #region Parameters

    /// <summary>
    /// Gets or sets the API key.
    /// </summary>
    /// <value>The API key.</value>
    public string APIKey { get; set; }

    /// <summary>
    /// Gets or sets the configuration.
    /// </summary>
    /// <value>The configuration.</value>
    public ChatGptOptions Config { get; set; } = new();

    /// <summary>
    /// Gets or sets the conversations.
    /// </summary>
    /// <value>The conversations.</value>
    public List<ChatGptConversation> Conversations { get; set; } = new();

    /// <summary>
    /// Gets or sets the session identifier.
    /// </summary>
    /// <value>The session identifier.</value>
    public Guid SessionId { get; set; }

    #endregion Parameters

    /// <summary>
    /// Initializes a new instance of the <see cref="ChatGpt"/> class.
    /// </summary>
    /// <param name="apikey">The apikey.</param>
    /// <param name="config">The configuration.</param>
    public ChatGpt(string apikey, ChatGptOptions? config = null)
    {
        Config = config ?? new ChatGptOptions();
        SessionId = Guid.NewGuid();
        APIKey = apikey;

        if (string.IsNullOrWhiteSpace(APIKey))
            throw new Exception("API Key is required");
    }

    /// <summary>
    /// Asks the specified prompt.
    /// </summary>
    /// <param name="prompt">The prompt.</param>
    /// <param name="conversationId">The conversation identifier.</param>
    /// <returns>System.String.</returns>
    public async Task<string> Ask(string prompt, string? conversationId = null)
    {
        if (string.IsNullOrWhiteSpace(APIKey))
            throw new Exception("API Key is required");

        var conversation = GetConversation(conversationId);

        conversation.Messages.Add(new ChatGptMessage
        {
            Role = "user",
            Content = prompt
        });

        var reply = await SendMessage(new ChatGptRequest
        {
            Messages = conversation.Messages,
            Model = Config.Model,
            Stream = false,
            Temperature = Config.Temperature,
            TopP = Config.TopP,
            FrequencyPenalty = Config.FrequencyPenalty,
            PresencePenalty = Config.PresencePenalty,
            Stop = Config.Stop,
            MaxTokens = Config.MaxTokens,
        });

        conversation.Updated = DateTime.Now;

        var response = reply.Choices.FirstOrDefault()?.Message.Content ?? "";

        conversation.Messages.Add(new ChatGptMessage
        {
            Role = "assistant",
            Content = response
        });

        return response;
    }

    /// <summary>
    /// Asks the stream.
    /// </summary>
    /// <param name="callback">The callback.</param>
    /// <param name="prompt">The prompt.</param>
    /// <param name="conversationId">The conversation identifier.</param>
    /// <returns>System.String.</returns>
    public async Task<string> AskStream(Action<string> callback, string prompt, string? conversationId = null)
    {
        var conversation = GetConversation(conversationId);

        conversation.Messages.Add(new ChatGptMessage
        {
            Role = "user",
            Content = prompt
        });

        var reply = await SendMessage(new ChatGptRequest
        {
            Messages = conversation.Messages,
            Model = Config.Model,
            Stream = true,
            Temperature = Config.Temperature,
            TopP = Config.TopP,
            FrequencyPenalty = Config.FrequencyPenalty,
            PresencePenalty = Config.PresencePenalty,
            Stop = Config.Stop,
            MaxTokens = Config.MaxTokens,
        }, response =>
        {
            var content = response.Choices.FirstOrDefault()?.Delta.Content;
            if (content is null) return;
            if (!string.IsNullOrWhiteSpace(content)) callback(content);
        });

        conversation.Updated = DateTime.Now;

        return reply.Choices.FirstOrDefault()?.Message.Content ?? "";
    }

    /// <summary>
    /// Clears the conversations.
    /// </summary>
    public void ClearConversations()
    {
        Conversations.Clear();
    }

    /// <summary>
    /// Gets the conversation.
    /// </summary>
    /// <param name="conversationId">The conversation identifier.</param>
    /// <returns>ChatGptConversation.</returns>
    public ChatGptConversation GetConversation(string? conversationId)
    {
        if (conversationId is null)
        {
            return new ChatGptConversation();
        }

        var conversation = Conversations.FirstOrDefault(x => x.Id == conversationId);

        if (conversation != null) return conversation;
        conversation = new ChatGptConversation()
        {
            Id = conversationId
        };
        Conversations.Add(conversation);

        return conversation;
    }

    /// <summary>
    /// Gets the conversations.
    /// </summary>
    /// <returns>List&lt;ChatGptConversation&gt;.</returns>
    public List<ChatGptConversation> GetConversations()
    {
        return Conversations;
    }

    /// <summary>
    /// Removes the conversation.
    /// </summary>
    /// <param name="conversationId">The conversation identifier.</param>
    public void RemoveConversation(string conversationId)
    {
        var conversation = Conversations.FirstOrDefault(x => x.Id == conversationId);

        if (conversation != null)
        {
            Conversations.Remove(conversation);
        }
    }

    /// <summary>
    /// Removes the conversation system messages.
    /// </summary>
    /// <param name="conversationId">The conversation identifier.</param>
    /// <param name="message">The message.</param>
    public void RemoveConversationSystemMessages(string conversationId, string message)
    {
        var conversation = GetConversation(conversationId);
        conversation.Messages = conversation.Messages.Where(x => x.Role != "system").ToList();
    }

    /// <summary>
    /// Replaces the conversation system message.
    /// </summary>
    /// <param name="conversationId">The conversation identifier.</param>
    /// <param name="message">The message.</param>
    public void ReplaceConversationSystemMessage(string conversationId, string message)
    {
        var conversation = GetConversation(conversationId);
        conversation.Messages = conversation.Messages.Where(x => x.Role != "system").ToList();
        conversation.Messages.Add(new ChatGptMessage
        {
            Role = "system",
            Content = message
        });
    }

    /// <summary>
    /// Resets the conversation.
    /// </summary>
    /// <param name="conversationId">The conversation identifier.</param>
    public void ResetConversation(string conversationId)
    {
        var conversation = Conversations.FirstOrDefault(x => x.Id == conversationId);

        if (conversation == null) return;
        conversation.Messages = new();
    }

    public async Task<ChatGptResponse> SendMessage(ChatGptRequest requestBody, Action<ChatGptStreamChunkResponse>? callback = null)
    {
        if (string.IsNullOrWhiteSpace(APIKey))
            throw new Exception("API Key is required");

        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri($"{Config.BaseUrl}/v1/chat/completions"),
            Headers =
            {
                {"Authorization", $"Bearer {APIKey}" }
            },
            Content = new StringContent(JsonSerializer.Serialize(requestBody))
            {
                Headers =
                {
                    ContentType = new MediaTypeHeaderValue("application/json")
                }
            }
        };

        try
        {
            //var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errContent = await response.Content.ReadFromJsonAsync<ChatGptResponse>();
                string json = JsonSerializer.Serialize(errContent);
                return errContent;
            }

            if (requestBody.Stream)
            {
                var contentType = response.Content.Headers.ContentType?.MediaType;
                if (contentType != "text/event-stream")
                {
                    var error = await response.Content.ReadFromJsonAsync<ChatGptResponse>();
                    throw new Exception(error?.Error?.Message ?? "Unknown error");
                }

                var concatMessages = string.Empty;

                ChatGptStreamChunkResponse? reply = null;
                var stream = await response.Content.ReadAsStreamAsync();
                await foreach (var data in StreamCompletion(stream))
                {
                    var jsonString = data.Replace("data: ", "");

                    if (string.IsNullOrWhiteSpace(jsonString)) continue;
                    if (jsonString == "[DONE]") break;

                    reply = JsonSerializer.Deserialize<ChatGptStreamChunkResponse>(jsonString);
                    if (reply is null) continue;

                    concatMessages += reply.Choices.FirstOrDefault()?.Delta.Content;
                    callback?.Invoke(reply);
                }

                return new ChatGptResponse
                {
                    Id = reply?.Id ?? Guid.NewGuid().ToString(),
                    Model = reply?.Model ?? "gpt-3.5-turbo",
                    Created = reply?.Created ?? 0,
                    Choices = new List<Choice>
                {
                    new()
                    {
                        Message = new ChatGptMessage
                        {
                            Content = concatMessages
                        }
                    }
                }
                };
            }

            var content = await response.Content.ReadFromJsonAsync<ChatGptResponse>();

            if (content is null) throw new Exception("Unknown error");
            if (content.Error is not null) throw new Exception(content.Error.Message);

            return content;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Sets the conversation.
    /// </summary>
    /// <param name="conversationId">The conversation identifier.</param>
    /// <param name="conversation">The conversation.</param>
    public void SetConversation(string conversationId, ChatGptConversation conversation)
    {
        var conv = Conversations.FirstOrDefault(x => x.Id == conversationId);

        if (conv != null)
        {
            conv = conversation;
        }
        else
        {
            Conversations.Add(conversation);
        }
    }

    /// <summary>
    /// Sets the conversations.
    /// </summary>
    /// <param name="conversations">The conversations.</param>
    public void SetConversations(List<ChatGptConversation> conversations)
    {
        Conversations = conversations;
    }

    /// <summary>
    /// Sets the conversation system message.
    /// </summary>
    /// <param name="conversationId">The conversation identifier.</param>
    /// <param name="message">The message.</param>
    public void SetConversationSystemMessage(string conversationId, string message)
    {
        var conversation = GetConversation(conversationId);
        conversation.Messages.Add(new ChatGptMessage
        {
            Role = "system",
            Content = message
        });
    }

    /// <summary>
    /// Streams the completion.
    /// </summary>
    /// <param name="stream">The stream.</param>
    /// <returns>IAsyncEnumerable&lt;System.String&gt;.</returns>
    private async IAsyncEnumerable<string> StreamCompletion(Stream stream)
    {
        using var reader = new StreamReader(stream);
        while (!reader.EndOfStream)
        {
            var line = await reader.ReadLineAsync();
            if (line != null)
            {
                yield return line;
            }
        }
    }
}