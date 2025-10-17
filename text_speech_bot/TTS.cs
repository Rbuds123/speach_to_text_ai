using System;
using System.Speech.Synthesis;

namespace text_speech_bot
{
    internal class TTS(string input)
    {
        public static void speak(string input) {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.Volume = 70;
            synth.Rate = 5;
            synth.Speak(input);
            }
    }
}
