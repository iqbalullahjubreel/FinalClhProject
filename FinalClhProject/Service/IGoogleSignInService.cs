//using FinalClhProject.Model;
using System.Threading.Tasks;
using FinalClhProject.DataAccess;

namespace FinalClhProject.Service
{
    public interface IGoogleSignInService
    {
        Task<string> GetAuthorizationUrlAsync( );
        Task<string> GetAccessTokenAsync(string code);
        Task<User> GetUserProfileAsync(string accessToken);
    }
}
