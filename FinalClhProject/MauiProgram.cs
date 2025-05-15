#if WINDOWS
using FinalClhProject.Platforms.Windows;
#endif
using GroqSharp;
using GroqSharp.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
//using FinalClhProject.View;
//using FinalClhProject.Data;
using FinalClhProject.Repository;
using FinalClhProject.Service;
using FinalClhProject.ViewModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using FinalClhProject.Views;
using FinalClhProject.DataAccess.Data;

namespace FinalClhProject;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddSingleton<IGroqAiService, GroqAiService>();
        builder.Services.AddScoped<IGoogleSignInRepository, GoogleSignInRepository>();
        #if WINDOWS
        builder.Services.AddSingleton<ISpeechRecognitionService, WindowsSpeechRecognitionService>();
        #endif
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<GoogleSignInPage>();
        builder.Services.AddTransient<WindowsSpeechToTextViewModel>();
        builder.Services.AddTransient<SpeechToTextPage>();
        builder.Services.AddTransient<RegisterViewModel>();
        builder.Services.AddTransient<RegisterPage>();
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<GetAllUsersPage>();
        builder.Services.AddSingleton<Ioc>();
        builder.Services.AddTransient<DeleteUserViewModel>();
        builder.Services.AddTransient<DeleteUserPage>();
        builder.Services.AddTransient<AiRizzPage>();
        builder.Services.AddTransient<DirtyRizzPage>();
        builder.Services.AddTransient<DirtyPickUpPage>();
        builder.Services.AddTransient<AiRizzViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
