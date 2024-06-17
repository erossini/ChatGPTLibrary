# ChatGPT library for C#

![openai-chatgpt-library](https://github.com/erossini/ChatGPTLibrary/assets/9497415/a6f7722c-c111-4b4b-b105-31a5b81fd2a5)

![NuGet badge](https://img.shields.io/nuget/v/PSC.CSharp.Library.ChatGPT)

This is another C# library for [ChatGPT](https://openai.com/chatgpt) using official OpenAI API that allows developers to access ChatGPT, a chat-based large language model. 
With this API, developers can send queries to ChatGPT and receive responses in real-time, making it easy to integrate ChatGPT into their own applications.

For more details about this project, see my following posts:
- [New ChatGPT library for C#](https://puresourcecode.com/dotnet/net8/new-chatgpt-library-for-c/)
- [Write a ChatGPT client](https://puresourcecode.com/dotnet/csharp/write-a-chatgpt-client/)
- [GitHub repository](https://github.com/erossini/ChatGPTLibrary)

## Quick example

```csharp
using PSC.CSharp.Library.ChatGPT;


var chat = new ChatGpt("<API_KEY>");
var response = await chat.Ask("What is the weather like today?");
Console.WriteLine(response);
```

## Table of Contents

- [Features](#features)
- [Getting Started](#getting-started)
    - [Usage](#usage)
        - [ChatGPT Official API](#chatgpt-official-api)
- [Configuration options](#configuration-options)
    - [ChatGPT Official API](#chatgpt-official-api)
- [Examples](#examples)
    - [ChatGPT Console App](#chatgpt-console-app)
    - [Use a different model](#use-a-different-model)
    - [Using ChatGPT Official API For Free](#using-chatgpt-official-api-for-free)

## Features

- Easy to use.
- Using official OpenAI API.
- Supports both free and pro accounts.
- Supports multiple accounts, and multiple conversations.
- Support response streaming, so you can get response while the model is still generating it.

## Getting Started

To install `PSC.CSharp.Library.ChatGPT`, run the following command in the Package Manager Console:

```bash
Install-Package PSC.CSharp.Library.ChatGPT
```

Alternatively, you can install it using the .NET Core command-line interface:

```bash
dotnet add package PSC.CSharp.Library.ChatGPT
```

## Usage

### ChatGPT Official API

Here is a sample code showing how to use `PSC.CSharp.Library.ChatGPT`:

```csharp
using PSC.CSharp.Library.ChatGPT;

var chat = new ChatGpt("<API_KEY>");

// get response
var response = await chat.Ask("What is the weather like today?");
Console.WriteLine(response);

// stream response
await chat.AskStream(response => {
    Console.WriteLine(response);
}, "What is the weather like today?");

// get a response for a specific conversation
var response = await chat.Ask("What is the weather like today?", "conversation name");
Console.WriteLine(response);

// stream response for a specific conversation
await chat.AskStream(response => {
    Console.WriteLine(response);
}, "What is the weather like today?", "conversation name");
```

## Configuration options

### ChatGPT Official API

```csharp
ChatGptOptions
{
    string    BaseUrl;           // Default: https://api.openai.com
    double    FrequencyPenalty;  // Default: 0.0;
    long      MaxTokens;         // Default: 64;
    string    Model;             // Default: gpt-3.5-turbo
    double    PresencePenalty;   // Default: 0.0;
    string[]? Stop;              // Default: null;
    double    Temperature;       // Default: 0.9;
    double    TopP;              // Default: 1.0;
}
```

#### BaseUrl

By default, the base URL is `https://api.openai.com`. You can set the `BaseUrl` to a free reverse proxy server to use ChatGPT Official API for free.

#### Frequency Penalty

How much to penalize new tokens based on their existing frequency in the text so far. Decreases the model's likelihood to repeat the same line verbatim.

#### Max Tokens

The maximum number of tokens to <strong>generate</strong> is shared between the prompt and completion. The exact limit varies by model. (One token is roughly 4 characters for standard English text)

#### Model

The model to use. The default is `gpt-3.5-turbo`. The `ModelValue` class contains the popular models to use.

- gpt-3.5-turbo
- gpt-3.5-turbo-16k
- gpt-4-turbo

You can also set the `Model` with any other model name. For more details, see the [OpenAI API Model documentation](https://platform.openai.com/docs/models).

#### Presence Penalty

How much to penalize new tokens based on whether they appear in the text so far. Increases the model's likelihood to talk about new topics.

#### Stop

Up to four sequences where the API will stop generating further tokens. The returned text will not contain the stop sequence.

#### Temperature

Controls randomness: Lowering results in less random completions. As the temperature approaches zero, the model will become deterministic and repetitive.

#### TopP

Controls diversity via nucleus sampling: 0.5 means half of all likelihood-weighted options are considered.

## Examples

### ChatGPT Console App

This is a simple console app that uses `PSC.CSharp.Library.ChatGPT` to interact with ChatGPT.

```csharp
using PSC.CSharp.Library.ChatGPT;

// ChatGPT Official API
var chat = new ChatGpt("<API_KEY>");

var prompt = string.Empty;

while (true)
{
    Console.Write("You: ");
    prompt = Console.ReadLine();

    if (prompt is null) break;
    if (string.IsNullOrWhiteSpace(prompt)) break;
    if (prompt == "exit") break;

    Console.Write("ChatGPT: ");
    await bot.AskStream(Console.Write, prompt, "default");
    Console.WriteLine();
}
```

### Use a different model

You can use a different model by passing the model name to the constructor.

```csharp
var chat = new ChatGpt("<API_KEY>", new ChatGptOptions
{
    Model = "gpt-3.5-turbo"
});
```

### Using ChatGPT Official API For Free

you can use ChatGPT Official API by setting the base URL to a free reverse proxy server.

```csharp
var chat = new ChatGpt("<API_KEY>", new ChatGptOptions
{
    BaseUrl = "https://api.youreverseproxy.com"
});
```

## Errors

When a call to ChatGPT fails, there are several possible errors that can be returned. The full error will be in the `Error` property. A common example is

```json
{
  "choices": null,
  "created": 0,
  "error": {
    "message": "You exceeded your current quota, please check your plan and billing details. For more information on this error, read the docs: https://platform.openai.com/docs/guides/error-codes/api-errors.",
    "type": "insufficient_quota",
    "param": null,
    "code": "insufficient_quota"
  },
  "id": null,
  "model": null,
  "object": null,
  "usage": null,
  "success": false
}
```

### insufficient_quota

This is a very common error. That means you haven't add a payment details on your account, you haven't add money in the account or you have reached the limit of your quote.
