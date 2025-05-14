using Windows.Media.SpeechRecognition;
using System;
using System.Threading.Tasks;
using System.Threading;
using FinalClhProject.Service;

namespace FinalClhProject.Platforms.Windows;

public class WindowsSpeechRecognitionService : ISpeechRecognitionService
{
    private SpeechRecognizer _speechRecognizer;
    private CancellationTokenSource _cancellationTokenSource;

    public WindowsSpeechRecognitionService()
    {
        _speechRecognizer = new SpeechRecognizer();
    }

    public async Task<string> RecognizeSpeechAsync(CancellationToken cancellationToken)
    {
        try
        {
            var speechRecognizer = new SpeechRecognizer();
            await speechRecognizer.CompileConstraintsAsync();

            var result = await speechRecognizer.RecognizeAsync().AsTask(cancellationToken);

            if (result.Status == SpeechRecognitionResultStatus.Success)
            {
                return result.Text;
            }
            else
            {
                return $"Recognition failed: {result.Status}";
            }
        }
        catch (OperationCanceledException)
        {
            return "Speech recognition was canceled.";
        }
        catch (Exception ex)
        {
            return $"Exception: {ex.Message}";
        }
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
