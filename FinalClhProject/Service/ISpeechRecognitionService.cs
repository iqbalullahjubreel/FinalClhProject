using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalClhProject.Service
{
    public interface ISpeechRecognitionService
    {
        Task<string> RecognizeSpeechAsync(CancellationToken cancellationToken);
        void StartRecognition();
        void StopRecognition();
        void PauseRecognition();
        void ResumeRecognition();
    }

}
