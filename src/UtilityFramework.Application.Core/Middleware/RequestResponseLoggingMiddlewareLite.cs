using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace UtilityFramework.Application.Core.Middleware
{
    public class RequestResponseLoggingMiddlewareLite
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly string _path;
        private readonly bool _allHeaders;
        private readonly string[] _headers;
        private readonly List<string> _ignoredApis;
        private readonly List<string> _censorWords;
        private readonly bool _delimiterStartAndEnd;

        public RequestResponseLoggingMiddlewareLite(RequestDelegate next, ILoggerFactory loggerFactory, string path = "api/", bool allHeaders = false, bool delimiterStartAndEnd = false, params string[] headers)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestResponseLoggingMiddlewareLite>();
            _path = path;
            _allHeaders = allHeaders;
            _headers = headers;
            _ignoredApis = Utilities.GetConfigurationRoot().GetSection("ignoredLogApis").Get<List<string>>() ?? [];
            _censorWords = Utilities.GetConfigurationRoot().GetSection("censorWords").Get<List<string>>() ?? [];
            _delimiterStartAndEnd = delimiterStartAndEnd;
        }
        public async Task Invoke(HttpContext context)
        {
            var originalBody = context.Response.Body;


            try
            {
                var currentPath = context.Request.Path.Value ?? string.Empty;

                if (string.IsNullOrEmpty(currentPath) || !currentPath.ContainsIgnoreCase(_path) ||
                    (_ignoredApis?.Any(currentPath.ContainsIgnoreCase) ?? false))
                {
                    await _next(context);
                    return;
                }

                var requestId = context.TraceIdentifier;
                var stopwatch = Stopwatch.StartNew();

                var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "N/A";
                context.Request.Headers.TryGetValue("Authorization", out var token);
                context.Request.Headers.TryGetValue("User-Agent", out var userAgent);

                var log = new StringBuilder(512);
                if (_delimiterStartAndEnd)
                    log.AppendLine("########################## START #############################");

                log.AppendFormat("Request ID: {0}\n", requestId);
                log.AppendFormat("{0} | IP: {1} | {2} {3}://{4}{5}{6}\n",
                                 context.Request.Protocol,
                                 ipAddress,
                                 context.Request.Method,
                                 context.Request.Scheme,
                                 context.Request.Host,
                                 context.Request.Path,
                                 context.Request.QueryString);

                log.Append("HEADERS: {");
                if (_allHeaders)
                {
                    foreach (var header in context.Request.Headers)
                        log.AppendFormat("\"{0}\": \"{1}\", ", header.Key, header.Value);
                }
                else if (_headers?.Length > 0)
                {
                    for (int i = 0; i < _headers.Length; i++)
                    {
                        string headerKey = _headers[i];

                        if (context.Request.Headers.TryGetValue(headerKey, out var valueHeader))
                            log.AppendFormat("\"{0}\": \"{1}\", ", headerKey, valueHeader);
                    }
                }
                else
                {
                    log.AppendFormat("\"Authorization\": \"{0}\", \"User-Agent\": \"{1}\"", token, userAgent);
                }
                log.AppendLine("}");

                if (context.Request.Method is "POST" or "PATCH" or "PUT")
                {
                    var requestBody = await GetBodyRequest(context.Request);

                    if (_censorWords?.Count > 0)
                    {
                        requestBody = requestBody.CensorJson(_censorWords);
                    }

                    log.AppendFormat("BODY: {0}\n", requestBody);
                }

                await FormatResponse(context, originalBody, log, stopwatch);
            }
            catch (Exception e)
            {
                _logger.LogCritical("LOG EXCEPTION: {0}", e);
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
                return bodyAsText.UnprettyJson();
            }
        }

        /// <summary>
        /// FORMATAR RESPONSE
        /// </summary>
        /// <param name="context"></param>
        /// <param name="originalBody"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        private async Task FormatResponse(HttpContext context, Stream originalBody, StringBuilder log, Stopwatch stopwatch)
        {
            try
            {
                using var memStream = new MemoryStream();
                context.Response.Body = memStream;

                await _next(context);

                stopwatch.Stop();
                var requestDuration = stopwatch.ElapsedMilliseconds;

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
                    logBody = isJson ? Regex.Unescape(bodyResponse.UnprettyJson()) : bodyResponse;
                }

                memStream.Position = 0;
                await memStream.CopyToAsync(originalBody);

                if (_censorWords?.Count > 0)
                {
                    logBody = logBody.CensorJson(_censorWords);
                }

                log.AppendFormat("RESPONSE: {0}\n", logBody);
                log.AppendFormat("DURATION: {0} ms | STATUS: {1}\n", requestDuration, context.Response.StatusCode);

                if (_delimiterStartAndEnd)
                    log.AppendLine("########################### END ##############################");

                _logger.LogWarning(log.ToString());
            }
            catch (Exception e)
            {
                _logger.LogCritical("LOG EXCEPTION: IN GET RESPONSE - {0}", e);
                throw;
            }
        }
    }
}