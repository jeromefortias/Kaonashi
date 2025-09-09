using System;
using System.IO;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Threading.Channels;
using static System.Runtime.InteropServices.JavaScript.JSType;

import vosk
import pyaudio
import json
import requests
import urllib3

# En cas de HTTPS
urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)

# Fonction d'appel d'OLLAMA
def call_ollama_chat_mistral(prompt):
    url = "http://localhost:11434/v1/chat/completions"  # indiquez le endpoint que vous utilisez
    headers = { "Content-Type": "application/json"}
payload = {
    "model": "mistral",
        "messages": [
            { "role": "user", "content": prompt}
        ]
    }

try:
        response = requests.post(url, json = payload, headers = headers, verify = False)
        response.raise_for_status()
        data = response.json()


        if "choices" in data and len(data["choices"]) > 0:
            content = data["choices"][0]["message"]["content"]
            print("Ollama response:", content)
            return content
        else:
            print("Unexpected response format:", data)
            return None
    except requests.RequestException as e:
        print("Error communicating with HTTPS endpoint:", e)
        return None

model_path = "vosk-model-fr-0.22"
model = vosk.Model(model_path)

rec = vosk.KaldiRecognizer(model, 16000)

p = pyaudio.PyAudio()
stream = p.open(format = pyaudio.paInt16,
            channels = 1,
            rate = 16000,
            input = True,
            frames_per_buffer = 8192)


output_file_path = "recognized_text.txt"


with open(output_file_path, "w") as output_file:
    print("Listening for speech. Say 'Terminé' to stop.")
    # Start streaming and recognize speech
    while True:
        data = stream.read(4096, exception_on_overflow = False) #ATENTION a cette option
        if rec.AcceptWaveform(data):
            result = json.loads(rec.Result())
            recognized_text = result['text']

            if recognized_text.strip():  
                output_file.write(recognized_text + "\n")
                print("Recognized:", recognized_text)



                call_ollama_chat_mistral(recognized_text)



                if "terminé" in recognized_text.lower():
                    print("Termination keyword detected. Stopping...")
                    break

stream.stop_stream()
stream.close()

p.terminate()
