using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalClhProject.Platforms.Android
{
    public static class SpeechResultReceiver
    {
        public static Action<string> OnSpeechResult;

        public static void SendResult(string result)
        {
            OnSpeechResult?.Invoke(result);
        }
    }
}