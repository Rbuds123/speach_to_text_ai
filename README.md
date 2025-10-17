# Speech-to-Text AI Bot

A production-ready speech-to-text and conversational assistant that connects local audio input to a generative AI model. This project integrates speech recognition (Azure Speech) with a large language model (Llama 3 via Ollama) to accept spoken input, transcribe it, and generate spoken or text responses.

This README explains how to install, configure, build, and run the application, and includes troubleshooting, security, and contribution guidance.

Table of contents
- Project overview
- Features
- Requirements
- Quickstart (local)
- Configuration
  - Azure Speech
  - Ollama / Llama 3
  - Environment variables
- Build & run (development and production)
- Usage examples
- Troubleshooting
- Contributing
- Security & privacy
- License & contact

Project overview
This application:
- Listens to microphone input
- Uses Azure Speech SDK for accurate speech-to-text transcription.
- Sends transcribed text to a generative model (Llama 3 via Ollama).
- Returns model responses as text and can optionally synthesize speech.

Features
- Configurable model endpoint (local Ollama or hosted)
- Cross-platform .NET 9 application (Windows, macOS, Linux)

Requirements
- .NET 9 SDK (dotnet 9.x) — development and build
- Ollama installed and configured (if using local Llama 3)
- Access to Llama 3 (license and model availability per Meta/Mosaic terms)
- Azure Speech subscription (for speech-to-text / text-to-speech)
- Microphone (for live input) or audio file support

Quickstart (local)
1. Clone repository
   ```git clone https://github.com/Rbuds123/speach_to_text_ai.git```


   ```cd speach_to_text_ai```

3. Set required environment variables (see Configuration below).

4. Build and run in development:
   dotnet build
   dotnet run --project ./text_speech_bot/text_speech_bot

5. Or run the published executable (Windows example):
   - Build release:
     ```dotnet publish -c Release -r win-x64 --self-contained false -o ./publish```
   - Run:
     ````./publish/text_speech_bot.exe````

Configuration

Azure Speech
- Create a Speech resource in Azure and get:
  - ```SPEECH_KEY (subscription key)```
  - ```SPEECH_REGION (resource region, e.g., "eastus")```
- For Text-to-Speech (TTS) also ensure your resource supports neural voices and the chosen region.

Ollama / Llama 3
- Ollama: If you want to run Llama 3 locally, follow Ollama installation instructions: https://ollama.com/docs
- Make sure the model is available and licensed correctly. Llama 3 access may require licensing and is subject to model provider terms.
- Alternatively the project can be configured to call a hosted model endpoint — update the model endpoint and auth credentials accordingly.

Environment variables / important variables
Set these in your environment or a secrets manager (do NOT commit secrets):

- SPEECH_KEY — Azure Speech key
- SPEECH_REGION — Azure Speech region (e.g., eastus)
- OLLAMA_BASE_URL — Ollama server base URL (if using Ollama HTTP API), e.g., http://localhost:11434
- MODEL_NAME — Name of the model to use (e.g., llama-3)

Example (Windows PowerShell)
$env:SPEECH_KEY = "your_azure_key"
$env:SPEECH_REGION = "eastus"
$env:OLLAMA_BASE_URL = "http://localhost:11434"
$env:MODEL_NAME = "llama-3"

Build & run

Development (cross-platform)
- Ensure .NET 9 SDK installed: https://dotnet.microsoft.com/download/dotnet/9.0
- Build:
  dotnet build
- Run:
  dotnet run --project ./text_speech_bot/text_speech_bot

Production (publish)
- Publish self-contained application (Windows example):
  dotnet publish ./text_speech_bot/text_speech_bot -c Release -r win-x64 --self-contained false -o ./publish
- Copy contents of ./publish to your server and run the executable. Set environment variables on the host.

Running the existing executable (as in original project)
If an executable is already generated in the repository:
1. Change directory:
   cd .\text_speech_bot\text_speech_bot\bin\Debug\net9.0\
2. Run:
   text_speech_bot.exe
Note: This is typically a debug build; create a Release publish for production.

Usage examples
- Live conversation:
  - Start app and speak into your microphone.
  - The app transcribes and forwards text to the model, then prints/speaks the response.

- File-based transcription:
  - Supply path to an audio file as a command-line argument or from the UI if supported.

Troubleshooting
- Microphone not detected:
  - Ensure OS privacy settings allow microphone access.
  - Test with another application to verify hardware.

- Azure authentication errors:
  - Verify SPEECH_KEY and SPEECH_REGION are correct.
  - Check Azure resource status in the portal.

- Model connection errors:
  - If using Ollama, confirm Ollama daemon is running and OLLAMA_BASE_URL is reachable.
  - Check model name and licensing; Llama 3 access may require proper credentials.

- Audio format errors:
  - Install ffmpeg for conversions and ensure input sample rates are supported.

Security & privacy
- Do NOT commit API keys or secrets to the repository.
- Consider using a secrets manager or environment-specific configuration (Azure Key Vault, GitHub Secrets).
- Be transparent to users if audio or transcripts are sent to external services (Azure or hosted LLMs).
- Review model terms (Llama 3 and Ollama) and Azure policies before deploying a public service.

Contributing
- Contributions are welcome. Please:
  - Open issues for bugs or feature requests.
  - Fork the repository, create a feature branch, and open a pull request with a clear description.
  - Follow coding and documentation conventions used in the repo.
  - Add tests for new behaviors.

Recommended next steps for repo maintainers
- Add CI (GitHub Actions) to build and smoke-test the app on push.
- Add a LICENSE file and CODE_OF_CONDUCT.md if you plan to open-source.
- Add example environment files and scripts (e.g., .env.example) without secrets.
- Add runtime config for Docker or container orchestration if you plan to deploy.

License
Add a LICENSE file to the repo. If you want a permissive license, consider MIT. If unsure, add LICENSE with your chosen license and update this README.

Support / Contact
For questions about this repository, open an issue or contact the maintainer: @Rbuds123 on GitHub.

Acknowledgements
- Azure Speech SDK
- Ollama and model providers for Llama 3 (observe their license and usage terms)
