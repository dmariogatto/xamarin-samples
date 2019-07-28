using System;
using System.Threading.Tasks;

namespace AccountAuthenticator.Services
{
    public interface IAuthenticationService
    {
        Task<string> GetAuthToken(string username, string password);
    }
}