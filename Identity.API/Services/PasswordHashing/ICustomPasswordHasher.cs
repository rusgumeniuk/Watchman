namespace Identity.API.Services.PasswordHashing
{
    public interface ICustomPasswordHasher
    {
        string Hash(string password);
        bool Verify(string hash, string password);
    }
}
