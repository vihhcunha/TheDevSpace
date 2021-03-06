namespace TheDevSpaceWebApp.Services
{
    public interface IAuthenticationService
    {
        public Guid? UserId { get; }
        public Guid? WriterId { get; }
        public bool IsAuthenticated { get; }
        public bool IsWriter { get; }
        public Task LoginAsync(Guid userId, string email, string name, Guid? writerId = null);
        public Task LogoutAsync();
        public void AddClaim(string key, string value);
    }
}
