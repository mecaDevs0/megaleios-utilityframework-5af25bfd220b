using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace UtilityFramework.Application.Core3.JwtMiddleware
{
    /// <summary>
    /// Provides options for <see cref="TokenProviderMiddleware"/>.
    /// </summary>
    public class TokenProviderOptions
    {
        /// <summary>
        /// The relative request path to listen on.
        /// </summary>
        /// <remarks>The default path is <c>/token</c>.</remarks>
        public string Path { get; set; } = "/token";

        /// <summary>
        ///  The Issuer (iss) claim for generated tokens.
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// The Audience (aud) claim for the generated tokens.
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// The expiration time for the generated tokens.
        /// </summary>
        /// <remarks>The default is five minutes (300 seconds).</remarks>
        public TimeSpan Expiration { get; set; } = TimeSpan.FromDays(1);

        /// <summary>
        /// The signing key to use when generating tokens.
        /// </summary>
        public SigningCredentials SigningCredentials { get; set; }

        /// <summary>
        /// Resolves a user identity given a username and pass.
        /// </summary>
        public Func<string, string, Task<ClaimsIdentity>> IdentityResolver { get; set; }

        /// <summary>
        /// Generates a random value (nonce) for each generated token.
        /// </summary>
        /// <remarks>The default nonce is a random GUID.</remarks>
        public Func<Task<string>> NonceGenerator { get; set; }
            = () => Task.FromResult(Guid.NewGuid().ToString());
    }
}