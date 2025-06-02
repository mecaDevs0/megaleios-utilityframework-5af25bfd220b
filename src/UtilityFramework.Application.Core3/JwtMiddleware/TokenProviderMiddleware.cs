using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace UtilityFramework.Application.Core3.JwtMiddleware
{
    /// <summary>
    /// Token generator middleware component which is added to an HTTP pipeline.
    /// This class is not created by application code directly,
    /// instead it is added by calling the <see cref="TokenProviderAppBuilderExtensions.UseSimpleTokenProvider"/>
    /// extension method.
    /// </summary>
    public class TokenProviderMiddleware
    {
        private readonly RequestDelegate _next;

        private static TokenProviderOptions _options;
        public TokenProviderMiddleware(IOptions<TokenProviderOptions> options, RequestDelegate next)
        {
            _next = next;
            _options = options.Value;
            ThrowIfInvalidOptions(_options);
        }

        /// <summary>
        /// MIDDLEWARE REQUEST
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext context)
        {
            return _next(context);
        }

        /// <summary>
        /// GERATE TOKEN AND REFRESH TOKEN PROFILE
        /// </summary>
        /// <param name="profileId">IDENTIFIER PROFILE</param>
        /// <param name="refreshToken">DEFAULT FALSE</param>
        /// <param name="customClaim"></param>
        /// <returns></returns>
        public static object GenerateToken(string profileId, bool refreshToken = false, params Claim[] customClaim)
        {
            try
            {

                if (BaseConfig.Encrypted)
                    profileId = Utilities.EncryptString(BaseConfig.SecretKey, profileId);

                var now = DateTime.Now;

                var claims = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.Sub, profileId),
                    new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(now).ToString(), ClaimValueTypes.Integer64)
                };

                if (customClaim != null && customClaim.Length > 0)
                    claims.AddRange(customClaim.Where(x => claims.Count(y => y.Type == x.Type) == 0));


                var expire = !refreshToken
                    ? now.Add(_options.Expiration)
                    : now.Add(_options.Expiration).Add(TimeSpan.FromHours(2));

                // Create the JWT and write it to a string
                var jwt = new JwtSecurityToken(
                    issuer: _options.Issuer,
                    audience: _options.Audience,
                    claims: claims,
                    notBefore: now,
                    expires: expire,
                    signingCredentials: _options.SigningCredentials);

                // Create the JWT and write it to a string
                var jwtRefresh = new JwtSecurityToken(
                    issuer: _options.Issuer,
                    audience: _options.Audience,
                    claims: claims,
                    notBefore: now,
                    expires: now.Add(_options.Expiration.Add(TimeSpan.FromHours(2))),
                    signingCredentials: _options.SigningCredentials);

                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                var encodedJwtRefresh = new JwtSecurityTokenHandler().WriteToken(jwtRefresh);

                var response = new
                {
                    access_token = encodedJwt,
                    refresh_token = encodedJwtRefresh,
                    expires_in = (long)_options.Expiration.TotalSeconds,
                    expires = $"{expire:dd/MM/yyyy HH:mm:ss}",
                    expires_type = "seconds",
                };

                // Serialize and return the response
                return response;

            }
            catch (Exception)
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.Path));

            }
        }

        /// <summary>
        /// GERATE TOKEN AND REFRESH TOKEN PROFILE
        /// </summary>
        /// <param name="profileId">IDENTIFIER PROFILE</param>
        /// <param name="refreshToken">DEFAULT FALSE</param>
        /// <param name="expireRefreshToken">DEFAULT FALSE</param>
        /// <param name="customClaim"></param>
        /// <returns></returns>
        public static object GenerateToken(string profileId, TimeSpan expireRefreshToken, bool refreshToken = false, params Claim[] customClaim)
        {
            try
            {

                if (BaseConfig.Encrypted)
                    profileId = Utilities.EncryptString(BaseConfig.SecretKey, profileId);

                var now = DateTime.Now;

                var claims = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.Sub, profileId),
                    new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(now).ToString(), ClaimValueTypes.Integer64)
                };

                if (customClaim != null && customClaim.Length > 0)
                    claims.AddRange(customClaim.Where(x => claims.Count(y => y.Type == x.Type) == 0));


                var expire = now.Add(_options.Expiration);

                // Create the JWT and write it to a string
                var jwt = new JwtSecurityToken(
                    issuer: _options.Issuer,
                    audience: _options.Audience,
                    claims: claims,
                    notBefore: now,
                    expires: expire,
                    signingCredentials: _options.SigningCredentials);

                // Create the JWT and write it to a string
                var jwtRefresh = new JwtSecurityToken(
                    issuer: _options.Issuer,
                    audience: _options.Audience,
                    claims: claims,
                    notBefore: now,
                    expires: expire.Add(expireRefreshToken),
                    signingCredentials: _options.SigningCredentials);

                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                var encodedJwtRefresh = new JwtSecurityTokenHandler().WriteToken(jwtRefresh);

                var response = new
                {
                    access_token = encodedJwt,
                    refresh_token = encodedJwtRefresh,
                    expires_in = (long)_options.Expiration.TotalSeconds,
                    expires = $"{expire:dd/MM/yyyy HH:mm:ss}",
                    expires_type = "seconds",
                };

                // Serialize and return the response
                return response;

            }
            catch (Exception)
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.Path));

            }
        }


        /// <summary>
        /// GERATE CUSTOM TOKEN
        /// </summary>
        /// <param name="profileId">IDENTIFIER PROFILE</param>
        /// <param name="path">PATH API OFF TOKEN</param>
        /// <param name="expiration"></param>
        /// <param name="refreshToken">DEFAULT FALSE</param>
        /// <param name="customClaim"></param>
        /// <returns></returns>
        public static object GenerateTokenCustom(string profileId, TimeSpan? expiration = null, bool refreshToken = false, params Claim[] customClaim)
        {
            try
            {

                if (BaseConfig.Encrypted)
                    profileId = Utilities.EncryptString(BaseConfig.SecretKey, profileId);

                var now = DateTime.Now;

                if (expiration == null)
                    expiration = _options.Expiration;


                var claims = new List<Claim>()
                {
                        new Claim(JwtRegisteredClaimNames.Sub, profileId),
                        new Claim(JwtRegisteredClaimNames.Jti, new Guid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(now).ToString(), ClaimValueTypes.Integer64)
                };


                if (customClaim != null && customClaim.Length > 0)
                    claims.AddRange(customClaim);

                var expire = !refreshToken
                    ? now.Add(expiration.GetValueOrDefault())
                    : now.Add(expiration.GetValueOrDefault()).Add(TimeSpan.FromHours(2));

                // Create the JWT and write it to a string
                var jwt = new JwtSecurityToken(
                    issuer: _options.Issuer,
                    audience: _options.Audience,
                    claims: claims,
                    notBefore: now,
                    expires: expire,
                    signingCredentials: _options.SigningCredentials);

                // Create the JWT and write it to a string
                var jwtRefresh = new JwtSecurityToken(
                    issuer: _options.Issuer,
                    audience: _options.Audience,
                    claims: claims,
                    notBefore: now,
                    expires: expire.Add(expiration.GetValueOrDefault().Add(TimeSpan.FromHours(2))),
                    signingCredentials: _options.SigningCredentials);

                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                var encodedJwtRefresh = new JwtSecurityTokenHandler().WriteToken(jwtRefresh);

                var response = new
                {
                    access_token = encodedJwt,
                    refresh_token = encodedJwtRefresh,
                    expires_in = (long)expiration.GetValueOrDefault().TotalSeconds,
                    expires = $"{expire:dd/MM/yyyy HH:mm:ss}",
                    expires_type = "seconds",
                };

                // Serialize and return the response
                return response;

            }
            catch (Exception)
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.Path));

            }
        }

        private static void ThrowIfInvalidOptions(TokenProviderOptions options)
        {
            if (string.IsNullOrEmpty(options.Path))
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.Path));
            }

            if (string.IsNullOrEmpty(options.Issuer))
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.Issuer));
            }

            if (string.IsNullOrEmpty(options.Audience))
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.Audience));
            }

            if (options.Expiration == TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(TokenProviderOptions.Expiration));
            }

            if (options.IdentityResolver == null)
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.IdentityResolver));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.SigningCredentials));
            }

            if (options.NonceGenerator == null)
            {
                throw new ArgumentNullException(nameof(TokenProviderOptions.NonceGenerator));
            }
        }

        /// <summary>
        /// Get this datetime as a Unix epoch timestamp (seconds since Jan 1, 1970, midnight UTC).
        /// </summary>
        /// <param name="date">The date to convert.</param>
        /// <returns>Seconds since Unix epoch.</returns>
        private static long ToUnixEpochDate(DateTime date) => new DateTimeOffset(date).ToUniversalTime().ToUnixTimeSeconds();

        /// <summary>
        /// METODO PARA VALIDAR TOKEN
        /// </summary>
        /// <param name="authToken"></param>
        /// <param name="validExpiration"></param>
        /// <returns></returns>
        public static bool ValidateToken(string authToken, bool validExpiration = true)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters(validExpiration);

            SecurityToken validatedToken;
            IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
            return true;
        }

        /// <summary>
        /// METODO PARA RENOVAR TOKEN
        /// </summary>
        /// <param name="authToken"></param>
        /// <param name="refreshToken"></param>
        /// <param name="customClaim"></param>
        /// <returns></returns>
        public static IActionResult RefreshToken(string authToken)
        {
            var ignored = new List<string>() { "jti", "lat", "nbf", "exp", "iss", "aud" };
            var customClaim = new Claim[] { };

            try
            {
                authToken = string.IsNullOrEmpty(authToken) == false ? Regex.Replace(authToken, "bearer", "", RegexOptions.IgnoreCase).Trim() : null;
                if (string.IsNullOrEmpty(authToken))
                    return new BadRequestObjectResult(Utilities.ReturnErro(ValidationMessageBase.InvalidCredentials));

                ValidateToken(authToken);

                var token = new JwtSecurityToken(authToken);

                var fromToken = token.Claims.ToList();
                var addInNewToken = customClaim.ToList();

                addInNewToken.AddRange(fromToken.Where(x => ignored.Count(y => y == x.Type) == 0));

                customClaim = addInNewToken.ToArray();

                if (string.IsNullOrEmpty(token?.Subject) == false)
                    return new OkObjectResult(Utilities.ReturnSuccess(data: TokenProviderMiddleware.GenerateToken(token?.Subject, false, customClaim)));

                return new BadRequestObjectResult(Utilities.ReturnErro(ValidationMessageBase.InvalidCredentials));

            }
            catch (SecurityTokenExpiredException ex)
            {
                return new BadRequestObjectResult(ex.ReturnErro(ValidationMessageBase.TokenExpired));

            }
            catch (SecurityTokenInvalidSignatureException ex)
            {
                return new BadRequestObjectResult(ex.ReturnErro(ValidationMessageBase.TokenSigntureInvalid));

            }
            catch (Exception ex)
            {

                return new BadRequestObjectResult(ex.ReturnErro(ValidationMessageBase.TokenInvalid));
            }

        }
        /// <summary>
        /// METODO PARA OBTER METODO DE VALIDAÇÃO DO TOKEN
        /// </summary>
        /// <param name="validExpiration"></param>
        /// <returns></returns>
        private static TokenValidationParameters GetValidationParameters(bool validExpiration = true)
        {

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(BaseConfig.SecretKey));

            return new TokenValidationParameters
            {
                // The signing key must match!
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                // Validate the JWT Issuer (iss) claim
                ValidateIssuer = true,
                ValidIssuer = BaseConfig.Issuer,

                // Validate the JWT Audience (aud) claim
                ValidateAudience = true,
                ValidAudience = BaseConfig.Audience,

                // Validate the token expiry
                ValidateLifetime = validExpiration,

                // If you want to allow a certain amount of clock drift, set that here:
                ClockSkew = TimeSpan.Zero,

            };
        }
    }
}
