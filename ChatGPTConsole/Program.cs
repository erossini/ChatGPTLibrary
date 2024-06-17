using PSC.CSharp.Library.ChatGPT;
using System.Text;
using System.Text.Json;

var chat = new ChatGpt("<your API key>");

var prompt = string.Empty;

while (true)
{
    Console.Write("You: ");
    prompt = Console.ReadLine();

    if (prompt is null) break;
    if (string.IsNullOrWhiteSpace(prompt)) break;
    if (prompt == "exit") break;

    Console.Write("ChatGPT: ");
    await chat.AskStream(Console.Write, prompt, "default");

    Console.WriteLine();
}