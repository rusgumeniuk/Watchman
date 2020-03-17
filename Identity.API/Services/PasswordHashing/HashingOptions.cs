namespace Identity.API.Services.PasswordHashing
{
    public sealed class HashingOptions
    {
        public int CountOfIterations { get; set; } = 10000;
    }
}
