using Android.App;
using Android.Content;
using Android.Speech;
using FinalClhProject.Service;
using Java.Util;
using Microsoft.Maui.Controls;
using System;
using System.Threading;
using System.Threading.Tasks;
using Locale = Java.Util.Locale;

[assembly: Dependency(typeof(FinalClhProject.Platforms.Android.SpeechRecognitionService_Android))]

namespace FinalClhProject.Platforms.Android
{
    public class SpeechRecognitionService_Android : ISpeechRecognitionService
    {
        public TaskCompletionSource<string> RecognitionTaskCompletionSource;
        private CancellationTokenSource _cancellationTokenSource;

        public Task<string> RecognizeSpeechAsync()
        {
            return RecognizeSpeechInternalAsync();
        }

        public Task<string> RecognizeSpeechAsync(CancellationToken cancellationToken)
        {
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            return RecognizeSpeechInternalAsync();
        }

        private Task<string> RecognizeSpeechInternalAsync()
        {
            var activity = Platform.CurrentActivity ?? throw new Exception("No Current Activity");

            RecognitionTaskCompletionSource = new TaskCompletionSource<string>();

            var intent = new Intent(RecognizerIntent.ActionRecognizeSpeech);
            intent.PutExtra(RecognizerIntent.ExtraLanguageModel, RecognizerIntent.LanguageModelFreeForm);
            intent.PutExtra(RecognizerIntent.ExtraLanguage, Locale.Default);
            intent.PutExtra(RecognizerIntent.ExtraPrompt, "Speak now...");

            activity.StartActivityForResult(intent, 10);

            return RecognitionTaskCompletionSource.Task;
        }

        public void StartRecognition()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;

            Task.Run(() => RecognizeSpeechAsync(token));
        }

        public void StopRecognition()
        {
            _cancellationTokenSource?.Cancel();
        }

        public void PauseRecognition()
        {
            StopRecognition();
        }

        public void ResumeRecognition()
        {
            StartRecognition();
        }
    }
}
