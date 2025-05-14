//using FinalClhProject.Data;
using FinalClhProject.DataAccess.Data;
//using FinalClhProject.Model;
using FinalClhProject.DataAccess;
using FinalClhProject.Repository;
using FinalClhProject.Service;
using FinalClhProject.ViewModel;
using System.Net;
using System.Text;
using System.Web;

namespace FinalClhProject.Views;

public partial class LoginPage : ContentPage
{
    private readonly IGoogleSignInService _googleSignInService;
    private readonly FinalClhProjDbContext _dbContext;
    private readonly IGoogleSignInRepository _googleSignInRepository;

    private HttpListener _httpListener;
    private const string RedirectUri = "http://localhost:5000/";
    public LoginPage(LoginViewModel viewmodel, IUserService userService, IGoogleSignInService googleSignInService, FinalClhProjDbContext dbContext, IGoogleSignInRepository googleSignInRepository)
    {
        InitializeComponent();
        _googleSignInService = googleSignInService;
        _dbContext = dbContext;
        _googleSignInRepository = googleSignInRepository;
        BindingContext = viewmodel;
    }

    private async void OnGoogleSignInButtonClicked(object sender, EventArgs e)
    {
        await StartOAuthFlow();
    }
    private async void OnHomePageButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("///MainPage");
    }

    private async Task StartOAuthFlow()
    {
        try
        {
            string authorizationUrl = await _googleSignInService.GetAuthorizationUrlAsync();

            await Launcher.Default.OpenAsync(authorizationUrl);

            await ListenForRedirectAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred during the sign-in process: {ex.Message}", "OK");
        }
    }

    private async Task ListenForRedirectAsync()
    {
        try
        {
            _httpListener = new HttpListener();
            _httpListener.Prefixes.Add(RedirectUri);
            _httpListener.Start();

            var context = await _httpListener.GetContextAsync();
            var request = context.Request;
            var response = context.Response;

            var query = HttpUtility.ParseQueryString(request.Url.Query);
            var code = query.Get("code");

            string responseString = "<html><body><h1>Login Successful! You can close this window.</h1></body></html>";
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);

            response.ContentLength64 = buffer.Length;
            var responseOutput = response.OutputStream;
            await responseOutput.WriteAsync(buffer);
            responseOutput.Close();

            _httpListener.Stop();

            if (!string.IsNullOrEmpty(code))
            {
                string accessToken = await _googleSignInService.GetAccessTokenAsync(code);
                User user = await _googleSignInService.GetUserProfileAsync(accessToken);
                await SecureStorage.SetAsync("user_id", user.Id.ToString());

                await SaveUserData(user);
                await Shell.Current.GoToAsync("///DirtyPickUpPage");
            }
            else
            {
                await DisplayAlert("Error", "Failed to retrieve the authorization code.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred while listening for the redirect: {ex.Message}", "OK");
        }
        finally
        {
            _httpListener?.Stop();
        }
    }

    private async Task SaveUserData(User user)
    {
        try
        {
            await _googleSignInRepository.SaveUserAsync(user);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred while saving user data: {ex.Message}", "OK");
        }
    }
}
