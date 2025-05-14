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

        var apiKey = "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";
        var model = "gemma2-9b-it";

        builder.Services.AddSingleton<IGroqClient>(_ =>
            new GroqClient(apiKey, model)
                .SetTemperature(0.5)
                .SetMaxTokens(512)
                .SetTopP(1)
                .SetStop("NONE")
                .SetStructuredRetryPolicy(5));

        var connectionString = "server=localhost;database=FinalClhProjDbContext;user=root;password=07065061918@iqbal";

        builder.Services.AddDbContext<FinalClhProjDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddSingleton<IGroqAiService, GroqAiService>();
        builder.Services.AddScoped<IGoogleSignInRepository, GoogleSignInRepository>();
        #if WINDOWS
        builder.Services.AddSingleton<ISpeechRecognitionService, WindowsSpeechRecognitionService>();
        #endif
        var clientId = "336898204045-mb2etvtrpfoqbc78s1bevbbgucvoko82.apps.googleusercontent.com";
        var clientSecret = "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";
        var redirectUri = "http://localhost:5000/";

        builder.Services.AddSingleton<IGoogleSignInService>(_ =>
     new GoogleSignInService(clientId, clientSecret, redirectUri));


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
