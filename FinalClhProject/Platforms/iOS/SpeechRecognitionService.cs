using AVFoundation;
using FinalClhProject.Service;
using Foundation;
using Microsoft.Maui.Controls;
using Speech;
using System;
using System.Threading.Tasks;
using UIKit;
using DependencyAttribute = Microsoft.Maui.Controls.DependencyAttribute;

[assembly: Dependency(typeof(FinalClhProject.Platforms.iOS.SpeechRecognitionService))]

namespace FinalClhProject.Platforms.iOS
{
    public class SpeechRecognitionService : ISpeechRecognitionService
    {
        private readonly SFSpeechRecognizer speechRecognizer = new SFSpeechRecognizer();
        private SFSpeechAudioBufferRecognitionRequest recognitionRequest;
        private SFSpeechRecognitionTask recognitionTask;
        private readonly AVAudioEngine audioEngine = new AVAudioEngine();

        public async Task<string> RecognizeSpeechAsync()
        {
            var authStatus = await RequestSpeechAuthorizationAsync();
            if (authStatus != SFSpeechRecognizerAuthorizationStatus.Authorized)
            {
                Console.WriteLine("Speech recognition not authorized.");
                return "Speech recognition not authorized.";
            }

            try
            {
                var node = audioEngine.InputNode;
                var recordingFormat = node.GetBusOutputFormat(0);
                node.InstallTapOnBus(0, 1024, recordingFormat, (buffer, when) =>
                {
                    recognitionRequest?.Append(buffer);
                });

                audioEngine.Prepare();

                NSError error;
                audioEngine.StartAndReturnError(out error);
                if (error != null)
                {
                    Console.WriteLine($"Audio engine error: {error.LocalizedDescription}");
                    return $"Audio engine error: {error.LocalizedDescription}";
                }

                recognitionRequest = new SFSpeechAudioBufferRecognitionRequest();
                recognitionTask = speechRecognizer.GetRecognitionTask(recognitionRequest, (result, err) =>
                {
                    if (result != null && result.Final)
                    {
                        var text = result.BestTranscription.FormattedString;
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            Application.Current?.MainPage?.DisplayAlert("Speech Result", text, "OK");
                        });
                        StopRecording();
                    }
                    else if (err != null)
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            Application.Current?.MainPage?.DisplayAlert("Speech Recognition Error", err.LocalizedDescription, "OK");
                        });
                        StopRecording();
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during speech recognition: {ex.Message}");
                return $"Error: {ex.Message}";
            }

            return "Listening for speech...";
        }

        private Task<SFSpeechRecognizerAuthorizationStatus> RequestSpeechAuthorizationAsync()
        {
            var tcs = new TaskCompletionSource<SFSpeechRecognizerAuthorizationStatus>();
            SFSpeechRecognizer.RequestAuthorization(status => tcs.SetResult(status));
            return tcs.Task;
        }

        private void StopRecording()
        {
            audioEngine.Stop();
            audioEngine.InputNode.RemoveTapOnBus(0);
            recognitionRequest?.EndAudio();
            recognitionRequest = null;
            recognitionTask = null;
        }

        public Task<string> RecognizeSpeechAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void StartRecognition()
        {
            throw new NotImplementedException();
        }

        public void StopRecognition()
        {
            throw new NotImplementedException();
        }

        public void PauseRecognition()
        {
            throw new NotImplementedException();
        }

        public void ResumeRecognition()
        {
            throw new NotImplementedException();
        }
    }
}
