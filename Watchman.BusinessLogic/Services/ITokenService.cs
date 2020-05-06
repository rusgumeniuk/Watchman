using System.Threading.Tasks;

namespace Watchman.BusinessLogic.Services
{
    public interface ITokenService
    {
        Task<string> GetTokenAsync(string email, string password);
        Task<string> RefreshToken(string email, string oldToken = null);
    }
}
