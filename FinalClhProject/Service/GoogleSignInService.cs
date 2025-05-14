//using FinalClhProject.Model;
using FinalClhProject.DataAccess;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FinalClhProject.Service
{
    public class GoogleSignInService : IGoogleSignInService
    {
        private readonly string _clientId ;
        private readonly string _clientSecret;
        private readonly string _redirectUri  ;

        public GoogleSignInService(string clientId, string clientSecret, string redirectUri)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _redirectUri = redirectUri;
        }
        public async Task<string> GetAuthorizationUrlAsync()
        {
            string[] scopes = new[]
            {
                "openid",
                "profile",
                "email"
            };

            string authorizationUrl = $"https://accounts.google.com/o/oauth2/v2/auth?" +
                $"client_id={_clientId}" +
                $"&redirect_uri={_redirectUri}" +
                $"&response_type=code" +
                $"&scope={string.Join(" ", scopes)}" +
                $"&access_type=offline" +
                $"&prompt=consent";

            return authorizationUrl;
        }


        public async Task<string> GetAccessTokenAsync(string code)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var values = new Dictionary<string, string>
            {
                { "code", code },
                { "client_id", _clientId },
                { "client_secret", _clientSecret },
                { "redirect_uri", _redirectUri },
                { "grant_type", "authorization_code" }
            };

                    var content = new FormUrlEncodedContent(values);

                    Console.WriteLine("Sending request to token endpoint...");
                    var response = await client.PostAsync("https://oauth2.googleapis.com/token", content);
                    var responseString = await response.Content.ReadAsStringAsync();

                    Console.WriteLine($"Response from token endpoint: {responseString}");

                    var json = JObject.Parse(responseString);

                    if (json.ContainsKey("access_token"))
                    {
                        return json["access_token"].ToString();
                    }
                    else
                    {
                        Console.WriteLine("Error: Access token not found.");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                return null;
            }
        }

        public async Task<User> GetUserProfileAsync(string accessToken)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    Console.WriteLine("Sending request to user info endpoint...");
                    var response = await client.GetAsync("https://www.googleapis.com/oauth2/v3/userinfo");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Response from user info endpoint: {responseString}");

                        var json = JObject.Parse(responseString);

                        return new User
                        {
                            Email = json["email"]?.ToString(),
                            FirstName = json["given_name"]?.ToString(),
                            LastName = json["family_name"]?.ToString()
                        };
                    }
                    else
                    {
                        Console.WriteLine("Error: Unable to retrieve user profile.");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex.Message}");
                return null;
            }
        }

       

    }
}
