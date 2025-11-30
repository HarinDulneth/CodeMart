namespace CodeMart.Server.Interfaces
{
    public interface IAuthenticateService
    {
        Task<string?> Login(string email, string password);
    }
}
