using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace text_speech_bot
{
    internal class Program
    {
        static void OutputSpeechRecognitionResult(SpeechRecognitionResult speechRecognitionResult)
        {
            switch (speechRecognitionResult.Reason) //error handling
            {
                case ResultReason.RecognizedSpeech: //success
                    Console.WriteLine($"{speechRecognitionResult.Text}");
                    break;
                case ResultReason.NoMatch: //couldnt understand 
                    Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                    break;
                case ResultReason.Canceled: //cancelled 
                    var cancellation = CancellationDetails.FromResult(speechRecognitionResult);
                    Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                        Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                        Console.WriteLine($"CANCELED: Did you set the speech resource key and endpoint values?");
                    }
                    break;
            }
        }

        public static async Task<SpeechRecognitionResult> voice_input_result(string[] args)
        {
            string speechKey = Environment.GetEnvironmentVariable("SPEECH_KEY");
            string endpoint = Environment.GetEnvironmentVariable("ENDPOINT");
            var speechConfig = SpeechConfig.FromEndpoint(new Uri(endpoint), speechKey);
            speechConfig.SpeechRecognitionLanguage = "en-GB"; //set lang it will understand 

            using var audioConfig = AudioConfig.FromDefaultMicrophoneInput(); //input device
            using var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig); //create speech recogniser

            //Console.WriteLine("Speak into your microphone.");

            var speechRecognitionResult = await speechRecognizer.RecognizeOnceAsync().ConfigureAwait(false); //detected speech recognised
            OutputSpeechRecognitionResult(speechRecognitionResult);
            return speechRecognitionResult;
        }
    }
}
