//using FinalClhProject.Data;
using FinalClhProject.Resources.Styles;
using FinalClhProject.Service;
using FinalClhProject.DataAccess.Data;

//using FinalClhProject.View;
using FinalClhProject.Views;
using System.Diagnostics;

namespace FinalClhProject;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        bool isDark = Preferences.Get("IsDarkMode", false);
        SetTheme(isDark);
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        Routing.RegisterRoute(nameof(DirtyRizzPage), typeof(DirtyRizzPage));
        Routing.RegisterRoute(nameof(DirtyPickUpPage), typeof(DirtyPickUpPage));
        Routing.RegisterRoute(nameof(AiRizzPage), typeof(AiRizzPage));
        Routing.RegisterRoute(nameof(DeleteUserPage), typeof(DeleteUserPage));
        Routing.RegisterRoute(nameof(UpdateUserPage), typeof(UpdateUserPage));
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        MainPage = new AppShell();

        //var migrationService = new MigrationService(DependencyService.Get<FinalClhProjDbContext>());
        //migrationService.MigrateDatabaseAsync().ConfigureAwait(false);
    }

    public void SetTheme(bool isDarkMode)
    {

        if (isDarkMode)
        {
            Debug.WriteLine("🌙 Dark mode applied");
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(new DarkColors());
        }
        else
        {
            Debug.WriteLine("☀️ Light mode applied");
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(new Colorss());
        }
    }
}
