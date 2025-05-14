using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Speech;
using Android.Content.PM;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using System.Collections.Generic;
using System;

namespace FinalClhProject
{
    [Activity(Label = "FinalClhProject", Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == 10) 
            {
                var service = DependencyService.Get<FinalClhProject.Platforms.Android.SpeechRecognitionService_Android>();
                if (resultCode == Result.Ok && data != null)
                {
                    IList<string> matches = data.GetStringArrayListExtra(RecognizerIntent.ExtraResults);
                    string result = matches?.Count > 0 ? matches[0] : string.Empty;

                    service?.RecognitionTaskCompletionSource?.TrySetResult(result);
                }
                else
                {
                    service?.RecognitionTaskCompletionSource?.TrySetResult(string.Empty); 
                }
            }
        }
    }
}
