namespace Identity.API.Services
{
    public interface ICustomPasswordHasher
    {
        string Hash(string password);
        bool Verify(string hash, string password);
    }
}
