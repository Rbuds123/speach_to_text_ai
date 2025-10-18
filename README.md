# 🎙️ Speech-to-Text AI Bot

[![.NET](https://img.shields.io/badge/.NET-9.0-blue?logo=dotnet)](https://dotnet.microsoft.com/)
[![Azure Speech](https://img.shields.io/badge/Azure-Speech_Services-0078D4?logo=microsoftazure&logoColor=white)](https://azure.microsoft.com/en-us/products/ai-services/speech/)
[![Ollama](https://img.shields.io/badge/Powered_by-Ollama-4D4D4D?logo=ollama&logoColor=white)](https://ollama.com)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

A **voice assistant** that connects your **microphone** to **Azure Speech** for transcription and **Llama 3 (Ollama)** for conversation.  
The app transcribes what you say, processes it with an AI model, and responds with text or speech.

---

## 🚀 Features

- 🎤 Real-time **microphone input**
- 🧠 **Azure Speech SDK** for speech recognition
- 🤖 **Llama 3** (via Ollama) for generative AI responses
- 🔊 Optional **text-to-speech** output
- 🧩 Easily configurable via environment variables

---

## 🧰 Requirements

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Ollama](https://ollama.com/docs) (for local Llama 3)
- Azure Speech resource and API key
- Microphone (for live mode) or audio file input
- Access to **Llama 3** model (local or hosted)

---

## ⚡ Quickstart

```bash
# Clone the repository
git clone https://github.com/Rbuds123/speach_to_text_ai.git
cd speach_to_text_ai

# Set environment variables
$env:SPEECH_KEY="your_azure_key"
$env:SPEECH_REGION="eastus"

# Build & run in development
dotnet build
run text_speech_bot.exe


```
### Plan is to add NetCord to this project so it can join a discord call and can be talked too
