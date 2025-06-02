using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace UtilityFramework.Application.Core3.Middleware
{
    public class RequestResponseLoggingMiddlewareFormated
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly bool _allHeaders;
        private readonly string[] _headers;
        private readonly string _path;
        private readonly List<string> _ignoredApis;
        private readonly List<string> _censorWords;

        public RequestResponseLoggingMiddlewareFormated(RequestDelegate next, ILoggerFactory loggerFactory, string path = "api/", bool allHeaders = false, params string[] headers)
        {
            _next = next;
            _headers = headers;
            _allHeaders = allHeaders;
            _path = path;
            _ignoredApis = Utilities.GetConfigurationRoot().GetSection("ignoredLogApis").Get<List<string>>() ?? [];
            _censorWords = Utilities.GetConfigurationRoot().GetSection("censorWords").Get<List<string>>() ?? [];
            _logger = loggerFactory.CreateLogger<RequestResponseLoggingMiddlewareFormated>();
        }
        public async Task Invoke(HttpContext context)
        {
            var originalBody = context.Response.Body;
            var log = new StringBuilder();

            try
            {
                var currentPath = context.Request.Path.Value ?? "";

                if (string.IsNullOrEmpty(context.Request.Path.Value) || currentPath.ContainsIgnoreCase(_path) == false || (_ignoredApis != null && _ignoredApis.Count(ignorePath => currentPath.ContainsIgnoreCase(ignorePath)) > 0))
                {
                    await _next(context);
                    return;
                }

                var ipAddress = context.Connection.RemoteIpAddress.ToString();

                context.Request.Headers.TryGetValue("Authorization", out var token);
                context.Request.Headers.TryGetValue("User-Agent", out var userAgent);

                var pathBase = context.Request.PathBase.HasValue ? $"/{context.Request.PathBase}" : "";

                log.AppendLine()
                   .AppendLine("########################## START #############################")
                   .AppendLine($"{context.Request.Protocol} IP:{ipAddress} {context.Request.Method} {context.Request.Scheme}://{context.Request.Host.Value}{pathBase}{context.Request.Path}{context.Request.QueryString}".Trim());

                var headers = new StringBuilder();
                headers.Append($"{{");

                if (_allHeaders)
                {
                    foreach (var header in context.Request.Headers)
                        headers.Append($"\"{header.Key}\":\"{header.Value}\",");

                }
                else if (_headers != null && _headers.Length > 0)
                {
                    for (int i = 0; i < _headers.Length; i++)
                    {
                        context.Request.Headers.TryGetValue(_headers[i], out var valueHeader);
                        headers.Append($"\"{_headers[i]}\":\"{valueHeader}\",");
                    }
                }
                else
                {
                    headers.Append($"\"Authorization\":\"{token}\", \"User-Agent\":\"{userAgent}\"");
                }
                headers.Append($"}}");
                log.AppendLine(Regex.Unescape($"HEADERS: {headers.ToString().PrettyJson()}"));


                if (context.Request.Method.Equals("POST") || context.Request.Method.Equals("PATCH") || context.Request.Method.Equals("PUT"))
                {
                    var requestBody = await GetBodyRequest(context.Request);

                    if (_censorWords?.Count > 0)
                    {
                        requestBody = requestBody.CensorJson(_censorWords);
                    }

                    log.AppendLine(Regex.Unescape($"BODY: {requestBody.PrettyJson()}"));
                }

                await FormatResponse(context, originalBody, log);

            }
            catch (Exception e)
            {
                log.AppendLine($"LOG EXCEPTION: {e.InnerException} {e.Message}".Trim())
                   .AppendLine("########################### END ##############################");
                _logger.LogCritical(log.ToString());

                throw;
            }
            finally
            {
                context.Response.Body = originalBody;
            }
        }
        /// <summary>
        /// RETORNA BODY DA SOLICITAÇÃO API
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<string> GetBodyRequest(HttpRequest request)
        {

            if (request.HasFormContentType)
            {
                return request.Form.PrintFormCollection();
            }
            else
            {
                string bodyAsText = "";
                using (StreamReader reader = new StreamReader(request.Body, Encoding.UTF8))
                {
                    bodyAsText = await reader.ReadToEndAsync();
                    byte[] requestData = Encoding.UTF8.GetBytes(bodyAsText);
                    request.Body = new MemoryStream(requestData);
                }
                return bodyAsText;
            }
        }
        /// <summary>
        /// FORMATAR RESPONSE
        /// </summary>
        /// <param name="context"></param>
        /// <param name="originalBody"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        private async Task FormatResponse(HttpContext context, Stream originalBody, StringBuilder log)
        {
            try
            {
                using var memStream = new MemoryStream();

                context.Response.Body = memStream;

                await _next(context);

                memStream.Position = 0;

                context.Response.Headers.TryGetValue("Content-Disposition", out StringValues contentDispositionValues);

                var contentDisposition = contentDispositionValues.ToString();

                string logBody = null;

                if (contentDisposition?.ToLower().StartsWith("attachment") == true)
                {
                    logBody = contentDisposition;
                }
                else
                {
                    string bodyResponse;
                    if (context.Response.Headers.Contains(new KeyValuePair<string, StringValues>("Content-Encoding", "gzip")))
                    {
                        var streamGzip = new GZipStream(memStream, CompressionMode.Decompress);
                        bodyResponse = new StreamReader(streamGzip).ReadToEnd();
                    }
                    else
                    {
                        bodyResponse = new StreamReader(memStream).ReadToEnd();
                    }

                    var isJson = bodyResponse.IsJsonValid();
                    logBody = isJson ? Regex.Unescape(bodyResponse.PrettyJson()) : bodyResponse;
                }

                memStream.Position = 0;

                await memStream.CopyToAsync(originalBody);

                if (_censorWords?.Count > 0)
                {
                    logBody = logBody.CensorJson(_censorWords);
                }

                log.AppendLine($"RESPONSE: {logBody}")
                   .AppendLine("########################### END ##############################");

                _logger.LogWarning(log.ToString());
            }
            catch (Exception e)
            {
                log.AppendLine($"LOG EXCEPTION: IN GET RESPONSE - {e.InnerException} {e.Message}".Trim())
                   .AppendLine("########################### END ##############################");

                _logger.LogCritical(log.ToString());

                throw;
            }
        }
    }
}