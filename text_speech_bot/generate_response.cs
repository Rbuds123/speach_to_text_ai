using System.Text;
using System.Xml.Schema;
using OllamaSharp;

namespace text_speech_bot
{
    internal class generate_response
    {
        /* Plan:
         - Set up Ctrl+C cancellation handling.
         - Create a single OllamaApiClient
         - Loop:
           - Prompt for user input ("> ").
           - If input is exit / quit or empty -> break.
           - Build a prompt with a small system instruction + user content.
           - Call GenerateAsync(prompt) and stream tokens to console as they arrive.
           - Write a newline after completion.
         - Dispose the client and exit gracefully.
        */
        public async static Task Main(string[] args)
        {
            using var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel(); //close program with no keyboard interupt error
            };


            const string baseUri = "http://127.0.0.1:11434";
            const string model = "llama3";
            const string bot_promt = "You are a helpful assistant here to answer any questions.";
            using var OllamaApiClient = new OllamaApiClient(uriString: baseUri, defaultModel: model);

            Console.WriteLine($"Model: {model} | Endpoint: {baseUri}");
            Console.WriteLine("Say 'Exit.' or 'Quit' to close. Press Ctrl+C to cancel a request.");
            Console.WriteLine();
            while (!cts.IsCancellationRequested) //if ctrl C pressed end
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("> ");
                Console.ResetColor();

                //get voice input
                var speech_result = await Program.voice_input_result(args);
                var input = speech_result.Text;

                if (string.IsNullOrWhiteSpace(input)) // if string is null continue
                {
                    continue;
                }
                if (input.Equals("Exit.", StringComparison.OrdinalIgnoreCase) || 
                    input.Equals("Quit.", StringComparison.OrdinalIgnoreCase)) //if input is exit or quit break 
                {
                    break;
                }
                var promt = new StringBuilder()
                .AppendLine(bot_promt) //how we want assitant to behave
                .AppendLine()
                .AppendLine("user:") // user input
                .AppendLine(input)
                .AppendLine()
                .AppendLine("assistant") //response
                .ToString();


                try
                {
                    Console.ForegroundColor = ConsoleColor.Green;

                    var responses = OllamaApiClient.GenerateAsync(promt, cancellationToken: cts.Token); //get ai response 
                    await foreach (var response in responses.WithCancellation(cts.Token)) //loop through ai output
                    {
                        if (!string.IsNullOrEmpty(response?.Response)) // if response is not null write response and speak response
                        {
                            Console.Write(response.Response);
                            TTS.speak(response.Response);
                        }
                    }
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nerror {ex.Message}"); //error message

                }
                finally
                {
                    Console.ResetColor();
                }
            

            }
            Console.WriteLine("Goodbye.");
        }
        
    }
}