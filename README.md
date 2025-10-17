# Speech to Text AI

A speech-to-text AI project. This repository contains the application code and configuration to run a local speech-to-text service that uses a local LLM via Ollama (for orchestration of Llama 3) and your project's C# backend.

> Note: This README assumes the project is a C#/.NET application. Adjust the dotnet commands or project paths to match your repository layout.

---

## Table of Contents

- Project overview
- Requirements
- Local development setup
  - Clone, build, run
  - Environment configuration
- Ollama + Llama 3: installation and setup
  - Install Ollama
  - Pull Llama 3 model via Ollama
  - Verify Ollama and model are running
- Running the application (local)
- Production considerations
- Troubleshooting
- Contributing
- License

---

## Project overview

This application converts speech input into text and augments or processes it using a local LLM via Ollama. The app communicates with Ollama's local API to call the LLM model (Llama 3). Replace model names and project paths as needed if your repo structure differs.

---

## Requirements

- Git
- .NET SDK 6.0 or later (recommended: .NET 7 / .NET 8)
  - Check: `dotnet --version`
- Ollama (see below) installed and running locally
- Sufficient machine resources for the chosen Llama 3 variant (RAM/VRAM)
- (Optional) Docker if you prefer containerized deployment

---

## Local development setup

1. Clone the repo:
   ```bash
   git clone https://github.com/Rbuds123/speach_to_text_ai.git
   cd speach_to_text_ai
   ```

2. Restore and build:
   ```bash
   dotnet restore
   dotnet build --configuration Release
   ```

3. Run the application:
   - If your project is at the repo root and has a single .csproj:
     ```bash
     dotnet run --configuration Release --project ./YourProject.csproj
     ```
   - Or navigate to the project folder and run:
     ```bash
     cd ./src/YourProjectFolder
     dotnet run
     ```

---

## Environment configuration

Create a `.env` or provide environment variables. Example `.env`:

```env
# Ollama local API URL
OLLAMA_URL=http://localhost:11434

# Model to use in Ollama
OLLAMA_MODEL=llama3

# App settings
ASPNETCORE_ENVIRONMENT=Development
PORT=5000
```

Your app should read `OLLAMA_URL` and `OLLAMA_MODEL` (or equivalent settings in appsettings.json) to call the local Ollama API.

---

## Ollama + Llama 3: installation and setup

The app expects Ollama to be installed locally and a Llama 3 model pulled/available in Ollama. Follow the official Ollama docs for current installation instructions: https://ollama.com/docs

Example (common approaches):

- macOS (Homebrew):
  ```bash
  brew install ollama
  ```

- Linux / other: visit the docs or use their install script if provided by Ollama:
  ```bash
  # Example — check the official site for exact command
  curl -sSf https://ollama.com/install.sh | sh
  ```

After installing Ollama, start the Ollama daemon/service:

```bash
# Start the local Ollama daemon (keeps Ollama running)
ollama daemon
```

Pull a Llama 3 model to your local Ollama instance. Replace `<model-name>` with the exact model identifier from Ollama Hub (names may vary):

```bash
# Example - use the actual model name available in Ollama Hub
ollama pull <model-name>      # e.g. ollama pull llama3 or ollama pull meta/llama-3-7b
```

Verify installed models and status:

```bash
ollama list
```

Quick sanity test:

```bash
# Run an interactive run (example)
ollama run <model-name> --prompt "Hello from Ollama!"
```

Notes:
- Model identifiers in Ollama may include owner/namespace (e.g., `meta/llama-3-7b`) — use the exact name reported by Ollama.
- Llama 3 models come in variants (7B, 13B, etc.). Larger models require more memory/VRAM.

---

## How the app talks to Ollama (example)

By default Ollama exposes a local API at `http://localhost:11434`. Your app should send requests to the `OLLAMA_URL` with the proper API endpoints Ollama provides.

Example (pseudo / curl call for a text generation request — check Ollama docs for exact endpoints):

```bash
curl -X POST "$OLLAMA_URL/api/generate" \
  -H "Content-Type: application/json" \
  -d '{
    "model": "'"$OLLAMA_MODEL"'",
    "prompt": "Transcribe the audio and return the text."
  }'
```

Adjust your C# client code to perform HTTP calls to Ollama (HttpClient, typed client, etc.) and parse the JSON responses.

---

## Running in production

- Run Ollama as a system service (systemd) or ensure daemon mode is started on boot.
- Choose the correct model size for the hardware — consider GPU acceleration if available.
- Use a reverse proxy (nginx) to expose your service securely; add HTTPS and authentication or API keys.
- Rate-limit and monitor usage (Prometheus / Grafana).
- Use health checks for both the app and Ollama service.
- Consider containerization:
  - Your application can be Dockerized easily.
  - Ollama is a separate process and may need to run on the host or a separate host/container with GPU passthrough.

---

## Troubleshooting

- "Model not found" — run `ollama list` and confirm the model name; re-run `ollama pull <model-name>`.
- "Ollama daemon not running" — start with `ollama daemon`.
- Low memory / OOM — use a smaller model or add more RAM/GPU resources.
- App can't connect to Ollama — ensure `OLLAMA_URL` is correct and that the daemon is listening on that address and port (default: `http://localhost:11434`).

---

## Security considerations

- Do not expose Ollama's local API to the public internet without proper authentication and firewall rules.
- Sanitize user-supplied inputs before sending to the LLM.
- Manage access to model files and logs containing sensitive data.

---

## Contributing

- Fork the repo and open a pull request.
- Add tests for new features and ensure builds pass.
- Update this README with any changes to the architecture or setup.

---

## License

Specify your license here (e.g., MIT). If no license file exists in the repo, add one or state the intended license.

---

If you want, I can now:
1. Create these two files in your repository (commit to main or create a branch + pull request), or
2. Create a PR with the changes so you can review before merging.

Tell me which you'd prefer and which branch to use (default: main). If you'd like me to push now, I'll proceed.
```
