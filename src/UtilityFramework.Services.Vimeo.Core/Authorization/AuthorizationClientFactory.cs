namespace UtilityFramework.Services.Vimeo.Core.Authorization
{
    /// <inheritdoc />
    public class AuthorizationClientFactory : IAuthorizationClientFactory
    {
        /// <inheritdoc />
        public IAuthorizationClient GetAuthorizationClient(string clientId, string clientSecret)
        {
            return new AuthorizationClient(clientId, clientSecret);
        }
    }
}