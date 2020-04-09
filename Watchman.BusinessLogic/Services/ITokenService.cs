using System.Threading.Tasks;

namespace Watchman.BusinessLogic.Services
{
    public interface ITokenService
    {
        Task<string> GetTokenAsync(string email, string password);
    }
}
