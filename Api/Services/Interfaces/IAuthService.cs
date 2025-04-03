namespace Api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string?> AuthenticateUser(string username);
    }

}
