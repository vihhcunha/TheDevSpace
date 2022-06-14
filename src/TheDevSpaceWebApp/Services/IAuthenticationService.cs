namespace TheDevSpaceWebApp.Services
{
    public interface IAuthenticationService
    {
        public Task LoginAsync(Guid userId, string email, string name, Guid? writerId = null);
        public Task LogoutAsync();
    }
}
