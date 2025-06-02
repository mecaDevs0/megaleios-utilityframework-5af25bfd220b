using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UtilityFramework.Services.Vimeo.Core;
using UtilityFramework.Services.Vimeo.Core.Authorization;
using UtilityFramework.Services.Vimeo.Core.Constants;
using UtilityFramework.Services.Vimeo.Core.Enums;
using UtilityFramework.Services.Vimeo.Core.Exceptions;
using UtilityFramework.Services.Vimeo.Core.Models;
using UtilityFramework.Services.Vimeo.Core.Net;

namespace UtilityFramework.Services.Vimeo
{
    public partial class VimeoClient : IVimeoClient
    {
        #region Constants

        internal const int DefaultUploadChunkSize = 1048576; // 1MB

        /// <summary>
        /// Range regex
        /// </summary>
        private static readonly Regex RangeRegex = new Regex(@"bytes\s*=\s*(?<start>\d+)-(?<end>\d+)",
            RegexOptions.IgnoreCase);

        #endregion

        #region Fields

        /// <summary>
        /// Api request factory
        /// </summary>
        private readonly IApiRequestFactory _apiRequestFactory;

        /// <summary>
        /// Auth client factory
        /// </summary>
        private readonly IAuthorizationClientFactory _authClientFactory;

        #endregion

        #region Properties

        /// <summary>
        /// ClientId
        /// </summary>
        private string ClientId { get; }

        /// <summary>
        /// ClientSecret
        /// </summary>
        private string ClientSecret { get; }

        /// <summary>
        /// AccessToken
        /// </summary>
        private string AccessToken { get; }

        /// <summary>
        /// OAuth2Client
        /// </summary>
        private IAuthorizationClient OAuth2Client { get; set; }

        

        #endregion

        #region Constructors

        private VimeoClient()
        {
           // _authClientFactory = new AuthorizationClientFactory();
            _apiRequestFactory = new ApiRequestFactory();
            RateLimit = 0;
            RateLimitRemaining = 0;
            RateLimitReset = DateTime.UtcNow;
        }
        /**
        /// <summary>
        /// Multi-user application constructor, using user-level OAuth2
        /// </summary>
        /// <param name="clientId">ClientId</param>
        /// <param name="clientSecret">ClientSecret</param>
        [PublicAPI]
        public VimeoClient(string clientId, string clientSecret)
            : this()
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            OAuth2Client = new AuthorizationClient(ClientId, ClientSecret);
        }
        **/

        /// <summary>
        /// Single-user application constructor, using account OAuth2 access token
        /// </summary>
        /// <param name="accessToken">Your Vimeo API Access Token</param>
        public VimeoClient(string accessToken)
            : this()
        {
            //_apiRequestFactory = new ApiRequestFactory();
            AccessToken = accessToken;
        }


        /**
        /// <inheritdoc />
        internal VimeoClient(IAuthorizationClientFactory authClientFactory, IApiRequestFactory apiRequestFactory,
            string clientId, string clientSecret)
            : this(clientId, clientSecret)
        {
            _authClientFactory = authClientFactory;
            _apiRequestFactory = apiRequestFactory;
        }

        /// <inheritdoc />
        internal VimeoClient(IAuthorizationClientFactory authClientFactory, IApiRequestFactory apiRequestFactory,
            string accessToken)
            : this(accessToken)
        {
            _authClientFactory = authClientFactory;
            _apiRequestFactory = apiRequestFactory;
        }
        **/
        #endregion

        #region Authorization

        /// <inheritdoc />
        public string GetOauthUrl(string redirectUri, IEnumerable<string> scope, string state)
        {
            PrepAuthorizationClient();
            return OAuth2Client.GetAuthorizationEndpoint(redirectUri, scope, state);
        }

        /// <inheritdoc />
        public async Task<AccessTokenResponse> GetAccessTokenAsync(string authorizationCode, string redirectUrl)
        {
            PrepAuthorizationClient();
            return await OAuth2Client.GetAccessTokenAsync(authorizationCode, redirectUrl).ConfigureAwait(false);
        }

        private void PrepAuthorizationClient()
        {
            if (OAuth2Client == null)
            {
                OAuth2Client = _authClientFactory.GetAuthorizationClient(ClientId, ClientSecret);
            }
        }

        #endregion

        #region Account

        /// <inheritdoc />
        public async Task<User> GetAccountInformationAsync()
        {
            var request = _apiRequestFactory.AuthorizedRequest(
                AccessToken,
                HttpMethod.Get,
                Endpoints.GetCurrentUserEndpoint(Endpoints.User)
            );

            return await ExecuteApiRequest<User>(request).ConfigureAwait(false);
        }

        /**
        /// <inheritdoc />
        /// 
        public async Task<User> UpdateAccountInformationAsync(EditUserParameters parameters)
        {
            var request = _apiRequestFactory.AuthorizedRequest(
                AccessToken,
                new HttpMethod("PATCH"),
                Endpoints.GetCurrentUserEndpoint(Endpoints.User),
                null,
                parameters
            );

            return await ExecuteApiRequest<User>(request).ConfigureAwait(false);
        }
        **/


        /// <inheritdoc />
        public async Task<User> GetUserInformationAsync(long userId)
        {
            var request = _apiRequestFactory.AuthorizedRequest(
                AccessToken,
                HttpMethod.Get,
                Endpoints.User,
                new Dictionary<string, string>
                {
                    {"userId", userId.ToString()}
                }
            );

            return await ExecuteApiRequest<User>(request).ConfigureAwait(false);
        }

        #endregion

        #region Utility

        /// <summary>
        /// Utility method for calling ExecuteApiRequest with the most common use case (returning
        /// null for NotFound responses).
        /// </summary>
        /// <typeparam name="T">Type of the expected response data.</typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<T> ExecuteApiRequest<T>(IApiRequest request) where T : new()
        {
            return await ExecuteApiRequest(request, statusCode => default(T), HttpStatusCode.NotFound)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Utility method for performing API requests that retrieve data in a consistent manner.
        ///
        /// The given request will be performed, and if the response is an outright success then
        /// the response data will be unwrapped and returned.
        ///
        /// If the call is not an outright success, but the status code is among the other acceptable
        /// results (provided via validStatusCodes), the getValueForStatusCode method will be called
        /// to generate a return value. This allows the caller to return null or an empty list as
        /// desired.
        ///
        /// If neither of the above is possible, an exception will be thrown.
        /// </summary>
        /// <typeparam name="T">Type of the expected response data.</typeparam>
        /// <param name="request"></param>
        /// <param name="getValueForStatusCode"></param>
        /// <param name="validStatusCodes"></param>
        /// <returns></returns>
        private async Task<T> ExecuteApiRequest<T>(IApiRequest request, Func<HttpStatusCode, T> getValueForStatusCode,
            params HttpStatusCode[] validStatusCodes) where T : new()
        {
            try
            {
                var response = await request.ExecuteRequestAsync<T>().ConfigureAwait(false);
                UpdateRateLimit(response);

                // if request was successful, return immediately...
                if (IsSuccessStatusCode(response.StatusCode))
                {
                    return response.Content;
                }

                // if request is among other accepted status codes, return the corresponding value...
                if (validStatusCodes != null && validStatusCodes.Contains(response.StatusCode))
                {
                    return getValueForStatusCode(response.StatusCode);
                }

                // at this point, we've eliminated all acceptable responses, throw an exception...
                throw new VimeoApiException(string.Format("{1}{0}Code: {2}{0}Message: {3}",
                    Environment.NewLine,
                    "Error retrieving information from Vimeo API.",
                    response.StatusCode,
                    response.Text
                ));
            }
            catch (Exception ex)
            {
                if (ex is VimeoApiException)
                {
                    throw;
                }

                throw new VimeoApiException("Error retrieving information from Vimeo API.", ex);
            }
        }

        /**
        private async Task<bool> ExecuteApiRequest(IApiRequest request, params HttpStatusCode[] validStatusCodes)
        {
            try
            {
                var response = await request.ExecuteRequestAsync().ConfigureAwait(false);
                UpdateRateLimit(response);
                // if request was successful, return immediately...
                if (IsSuccessStatusCode(response.StatusCode))
                {
                    return true;
                }

                // if request is among other accepted status codes, return the corresponding value...
                if (validStatusCodes != null && validStatusCodes.Contains(response.StatusCode))
                {
                    return true;
                }

                // at this point, we've eliminated all acceptable responses, throw an exception...
                throw new VimeoApiException(string.Format("{1}{0}Code: {2}{0}Message: {3}",
                    Environment.NewLine,
                    "Error retrieving information from Vimeo API.",
                    response.StatusCode,
                    response.Text
                ));
            }
            catch (Exception ex)
            {
                if (ex is VimeoApiException)
                {
                    throw;
                }

                throw new VimeoApiException("Error retrieving information from Vimeo API.", ex);
            }
        }
        **/
        #endregion

        #region Helper Functions

        private void ThrowIfUnauthorized()
        {
            if (string.IsNullOrWhiteSpace(AccessToken))
            {
                throw new InvalidOperationException("Please authenticate via OAuth to obtain an access token.");
            }
        }

        private static void CheckStatusCodeError(IUploadRequest request, IApiResponse response, string message,
            params HttpStatusCode[] validStatusCodes)
        {
            if (!IsSuccessStatusCode(response.StatusCode) && validStatusCodes != null &&
                !validStatusCodes.Contains(response.StatusCode))
            {
                throw new VimeoUploadException(string.Format("{1}{0}Code: {2}{0}Message: {3}",
                        Environment.NewLine, message, response.StatusCode, response.Text),
                    request);
            }
        }

        private static void CheckStatusCodeError(IApiResponse response, string message,
            params HttpStatusCode[] validStatusCodes)
        {
            if (!IsSuccessStatusCode(response.StatusCode) && validStatusCodes != null &&
                !validStatusCodes.Contains(response.StatusCode))
            {
                throw new VimeoApiException(string.Format("{1}{0}Code: {2}{0}Message: {3}",
                    Environment.NewLine, message, response.StatusCode, response.Text));
            }
        }

        private static bool IsSuccessStatusCode(HttpStatusCode statusCode)
        {
            var code = (int) statusCode;
            return code >= 200 && code < 300;
        }

        public Task DisallowEmbedVideoOnDomainAsync(long clipId, string domain)
        {
            throw new NotImplementedException();
        }

        #endregion


       
        /// <summary>
        /// Start upload file asynchronously
        /// </summary>
        /// <param name="fileContent">FileContent</param>
        /// <param name="chunkSize">ChunkSize</param>
        /// <param name="replaceVideoId">ReplaceVideoId</param>
        /// <returns></returns>
        private async Task<IUploadRequest> StartUploadFileAsync(IBinaryContent fileContent,
            int chunkSize = DefaultUploadChunkSize,
            long? replaceVideoId = null)
        {
            if (!fileContent.Data.CanRead)
            {
                throw new ArgumentException("fileContent should be readable");
            }

            if (fileContent.Data.CanSeek && fileContent.Data.Position > 0)
            {
                fileContent.Data.Position = 0;
            }

            var ticket = replaceVideoId.HasValue
                ? await GetReplaceVideoUploadTicketAsync(replaceVideoId.Value).ConfigureAwait(false)
                : await GetUploadTicketAsync().ConfigureAwait(false);

            var uploadRequest = new UploadRequest
            {
                Ticket = ticket,
                File = fileContent,
                ChunkSize = chunkSize
            };

            return uploadRequest;
        }
        

        /// <summary>
        /// Continue upload file asynchronously
        /// </summary>
        /// <param name="uploadRequest">UploadRequest</param>
        /// <returns>Verification upload response</returns>
        private async Task<VerifyUploadResponse> ContinueUploadFileAsync(IUploadRequest uploadRequest)
        {
            if (uploadRequest.FileLength == uploadRequest.BytesWritten)
            {
                // Already done, there's nothing to do.
                return new VerifyUploadResponse
                {
                    Status = UploadStatusEnum.InProgress,
                    BytesWritten = uploadRequest.BytesWritten
                };
            }

            try
            {
                var request = await GenerateFileStreamRequest(uploadRequest.File, uploadRequest.Ticket,
                    chunkSize: uploadRequest.ChunkSize, written: uploadRequest.BytesWritten).ConfigureAwait(false);
                var response = await request.ExecuteRequestAsync().ConfigureAwait(false);
                CheckStatusCodeError(uploadRequest, response, "Error uploading file chunk.", HttpStatusCode.BadRequest);

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    // something went wrong, figure out where we need to start over
                    return await VerifyUploadFileAsync(uploadRequest).ConfigureAwait(false);
                }

                // Success, update total written
                uploadRequest.BytesWritten += uploadRequest.ChunkSize;
                uploadRequest.BytesWritten = Math.Min(uploadRequest.BytesWritten, uploadRequest.FileLength);

                return new VerifyUploadResponse
                {
                    Status = uploadRequest.FileLength == uploadRequest.BytesWritten
                        ? UploadStatusEnum.Completed
                        : UploadStatusEnum.InProgress,
                    BytesWritten = uploadRequest.BytesWritten
                };
            }
            catch (Exception ex)
            {
                if (ex is VimeoApiException)
                {
                    throw;
                }

                throw new VimeoUploadException("Error uploading file chunk", uploadRequest, ex);
            }
        }


        /// <inheritdoc />
        public async Task<IUploadRequest> UploadEntireFileAsync(IBinaryContent fileContent,
            int chunkSize = DefaultUploadChunkSize,
            long? replaceVideoId = null,
            Action<double> statusCallback = null)
        {
            var uploadRequest = await StartUploadFileAsync(fileContent, chunkSize, replaceVideoId).ConfigureAwait(false);

            while (!uploadRequest.IsVerifiedComplete)
            {
                var uploadStatus = await ContinueUploadFileAsync(uploadRequest).ConfigureAwait(false);

                statusCallback?.Invoke(Math.Round(((double)uploadStatus.BytesWritten / uploadRequest.FileLength) * 100));

                if (uploadStatus.Status == UploadStatusEnum.InProgress)
                    continue;
                // We presumably wrote all the bytes in the file, so verify with Vimeo that it
                // is completed
                uploadStatus = await VerifyUploadFileAsync(uploadRequest).ConfigureAwait(false);
                if (uploadStatus.Status == UploadStatusEnum.Completed)
                {
                    // If completed, mark file as complete
                    await CompleteFileUploadAsync(uploadRequest).ConfigureAwait(false);
                    uploadRequest.IsVerifiedComplete = true;
                }
                else if (uploadStatus.BytesWritten == uploadRequest.FileLength)
                {
                    // Supposedly all bytes are written, but Vimeo doesn't think so, so just
                    // bail out
                    throw new VimeoUploadException(
                        $"Vimeo failed to mark file as completed, Bytes Written: {uploadStatus.BytesWritten:N0}, Expected: {uploadRequest.FileLength:N0}.",
                        uploadRequest);
                }
            }

            return uploadRequest;
        }
    }
}