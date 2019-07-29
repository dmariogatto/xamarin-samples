using System;
using System.Threading.Tasks;

namespace AccountAuthenticator.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public async Task<string> GetAuthToken(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException(nameof(username));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException(nameof(password));

            // Your amazing account logic
            await Task.Delay(1000);

            return Guid.NewGuid().ToString();
        }
    }
}