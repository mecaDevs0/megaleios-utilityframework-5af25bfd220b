using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AspNetCore.Firebase.Authentication.Extensions;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Events;
using UtilityFramework.Application.Core3.JwtMiddleware;
using UtilityFramework.Application.Core3.Middleware;
using UtilityFramework.Application.Core3.ViewModels;
using IHostingEnvironment = Microsoft.Extensions.Hosting.IHostingEnvironment;

namespace UtilityFramework.Application.Core3
{
    public static class UtilityFrameworkMiddlewareExtensions
    {
        /// <summary>
        /// ADICIONAR SWAGGER DOCUMENTAÇÃO
        /// </summary>
        /// <param name="services"></param>
        /// <param name="title"></param>
        /// <param name="name"></param>
        /// <param name="version"></param>
        /// <param name="xmlName"></param>
        /// <param name="enumAsString"></param>
        /// <param name="removeNull"></param>
        /// <param name="responseCompresion"></param>
        /// <param name="levelCompression"></param>
        /// <param name="enablePut"></param>
        /// <param name="enableSwaggerAuth"></param>
        /// <returns></returns>

        public static IServiceCollection AddJwtSwagger(this IServiceCollection services, string title,
            string name = "v1", string version = "v1", string xmlName = "XmlDocument.xml", bool enumAsString = true,
            bool removeNull = false, bool responseCompresion = true, CompressionLevel levelCompression = CompressionLevel.Optimal, bool enablePut = true, bool enableSwaggerAuth = false)
        {

            var baseConfig = new BaseConfig();

            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

            configuration.GetSection("Jwt").Bind(baseConfig);

            if (string.IsNullOrEmpty(BaseConfig.SecretKey))
            {
                throw new ArgumentNullException(nameof(BaseConfig.SecretKey), "Informe o Jwt.SecretKey no appsettins.json");
            }

            var _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(BaseConfig.SecretKey));

            if (enablePut)
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = removeNull ? NullValueHandling.Ignore : NullValueHandling.Include
            };

            services.AddSwaggerGen((options) =>
            {
                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                options.SwaggerDoc("v1", new OpenApiInfo { Title = title, Version = version });

                if (enumAsString)
                    options.SchemaFilter<EnumSchemaFilter>();

                if (enableSwaggerAuth)
                {
                    options.AddSecurityDefinition("JWT", new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.ApiKey,
                        Description = "Bearer {{access_token}}",
                        In = ParameterLocation.Header,
                        Name = "Authorization"
                    });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "JWT",
                                },
                                Type = SecuritySchemeType.ApiKey,
                                Description = "Bearer {{access_token}}",
                                In = ParameterLocation.Header,
                                Name = "Authorization",

                            }, new List<string>() }
                    });
                }

                var basePath = AppContext.BaseDirectory;

                var allDocumentation = Directory.GetFiles(basePath, "*.xml", SearchOption.AllDirectories).Distinct().ToList();

                for (int i = 0; i < allDocumentation.Count; i++)
                {
                    options.IncludeXmlComments(allDocumentation[i], true);
                }

                var defaultPath = Path.Combine(basePath, xmlName);

                if (File.Exists(defaultPath))
                    options.IncludeXmlComments(defaultPath, true);

                options.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.HttpMethod}");
            });

            services.AddSwaggerGenNewtonsoftSupport();

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;

                paramsValidation.IssuerSigningKey = _symmetricSecurityKey;
                paramsValidation.ValidAudience = BaseConfig.Audience;
                paramsValidation.ValidIssuer = BaseConfig.Issuer;
                paramsValidation.RequireExpirationTime = true;

                // Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda é válido
                paramsValidation.ValidateLifetime = true;

                // Tempo de tolerância para a expiraÃOo de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy(JwtBearerDefaults.AuthenticationScheme, new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build());
            });

            if (responseCompresion)
            {
                services.Configure<GzipCompressionProviderOptions>(options => options.Level = levelCompression);
                services.AddResponseCompression(options =>
                {
                    options.Providers.Add<GzipCompressionProvider>();
                });
            }
            return services;
        }

        /// <summary>
        /// ADD MEMORY IN CACHE FOR CROP
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddImageResizer(this IServiceCollection services)
        {
            return services.AddMemoryCache();
        }
        /// <summary>
        /// ADD DINKTOPDF INJECT DEPENDENCE AND LOAD DLL
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDinkToPdf(this IServiceCollection services)
        {

            var architectureFolder = (IntPtr.Size == 8) ? "64 bit" : "32 bit";

            var wkHtmlToPdfPath = Path.Combine(Directory.GetCurrentDirectory(), $"wkhtmltox\\v0.12.4\\{architectureFolder}\\libwkhtmltox");

            if (!File.Exists($"{wkHtmlToPdfPath}.dll"))
                throw new Exception("Para usar DinkToPdf é necessário baixar e copiar a pasta v0.12.4 do repositório: https://github.com/rdvojmoc/DinkToPdf para uma pasta na raiz do projeto com nome \"wkhtmltox\".");

            CustomAssemblyLoadContext context = new();
            context.LoadUnmanagedLibrary(wkHtmlToPdfPath);

            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            return services;
        }

        /// <summary>
        ///     CROP DE IMAGEM
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseImageResizer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ImageResizerMiddleware>();
        }

        /// <summary>
        ///     USAR ANTES DO USEMVC
        /// </summary>
        /// <param name="app"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseResponseShowInternalServerError(this IApplicationBuilder app,
            string path = "api/")
        {
            app.UseExceptionHandler(
                applicationBuilder => applicationBuilder.Run(
                    async context =>
                    {
                        if (context.Request.Path.HasValue && context.Request.Path.Value.Contains(path))
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            context.Response.ContentType = "application/json";
                            var ex = context.Features.Get<IExceptionHandlerFeature>();
                            if (ex != null)
                            {
                                var message = "Serviço temporariamente indisponível";

                                if (ex.Error is TimeoutException)
                                    message += ". ErroCode: 1002";
                                else
                                    message += ". ErroCode: 1001";

                                var err = JsonConvert.SerializeObject(new
                                {
                                    data = (string)null,
                                    erro = true,
                                    message,
                                    messageEx = $"{ex.Error.InnerException} {ex.Error.Message}"?.Trim(),
                                    stacktrace = ex.Error.StackTrace?.Trim()
                                });

                                await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(err), 0, err.Length)
                                                           .ConfigureAwait(false);
                            }
                        }

                    }));

            app.UseStatusCodePages(async context =>
            {
                if (context.HttpContext.Request.Path.Value.Contains(path) &&
                context.HttpContext.Response.StatusCode == 401)
                {
                    context.HttpContext.Response.ContentType = "application/json";
                    await context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(new ReturnViewModel
                    {
                        Message = "Authorization has been denied for this request.",
                        Erro = true
                    }, Formatting.Indented), Encoding.UTF8);
                }
            });

            return app;
        }

        /// <summary>
        /// USE JWT TOKEN WITH FIREBASE AUTH
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <param name="expirationToken"></param>
        /// <param name="validExpiration"></param>
        /// <param name="authenticationScheme"></param>
        /// <param name="path"></param>
        /// <param name="useLocale"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseJwtTokenApiAuth(this IApplicationBuilder app,
            IConfigurationRoot configuration, TimeSpan? expirationToken = null, bool validExpiration = true,
            string authenticationScheme = "Bearer", string path = "/api/v1/User/Token", bool useLocale = true)
        {

            var baseConfig = new BaseConfig();
            configuration.GetSection("Jwt").Bind(baseConfig);
            configuration.GetSection("Config").Bind(baseConfig);

            try
            {
                BaseConfig.ApplicationName ??= configuration.GetSection("ApplicationName").Get<string>() ?? Assembly.GetEntryAssembly().GetName().Name?.Split('.')[0];
                BaseConfig.EnableSwagger = configuration.GetSection("EnableSwagger").Get<bool>();
                ;
            }
            catch (Exception)
            { /*unused*/
            }


            if (string.IsNullOrEmpty(BaseConfig.Audience))
                throw new Exception("Informe o campo Audience do token");
            if (string.IsNullOrEmpty(BaseConfig.Issuer))
                throw new Exception("Informe o campo Issuer do token");

            if (string.IsNullOrEmpty(BaseConfig.SecretKey))
                throw new Exception("Informe o campo SecretKey do token");

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(BaseConfig.SecretKey));

            if (expirationToken == null && (string.IsNullOrEmpty(BaseConfig.TokenFrom) || Math.Abs(BaseConfig.TokenValue) <= 0))
                throw new Exception("Informe o um ciclo de vida valido pra o token");

            if (!string.IsNullOrEmpty(BaseConfig.TokenFrom) && expirationToken == null)
            {
                if (Math.Abs(BaseConfig.TokenValue) <= 0)
                    throw new Exception("Informe o tempo de validade do token");

                expirationToken = BaseConfig.TokenFrom.GerateTimeSpan(BaseConfig.TokenValue);
            }
            if (useLocale)
                app.UseLocaleCustom(configuration);

            app.UseSimpleTokenProvider(new TokenProviderOptions
            {
                Path = path,
                Audience = BaseConfig.Audience,
                Issuer = BaseConfig.Issuer,
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                Expiration = expirationToken.GetValueOrDefault(),
                IdentityResolver = GetIdentity,
            });

            app.UseResponseShowInternalServerError();

            return app;
        }

        /// <summary>
        /// USE JWT AUTH WITH FIREBASE AUTHENTICATION
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <param name="firebaseProjectName"></param>
        /// <param name="useLocale"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseJwtTokenApiAuthFirebase(this IApplicationBuilder app,
            IConfigurationRoot configuration,
            string firebaseProjectName,
            bool useLocale = true)
        {
            if (useLocale)
                app.UseLocaleCustom(configuration);

            app.UseResponseShowInternalServerError();

            var baseConfig = new BaseConfig();

            configuration.GetSection("Jwt").Bind(baseConfig);
            configuration.GetSection("Config").Bind(baseConfig);

            try
            {
                BaseConfig.ApplicationName ??= configuration.GetSection("ApplicationName").Get<string>() ?? Assembly.GetEntryAssembly().GetName().Name?.Split('.')[0];
                BaseConfig.EnableSwagger = configuration.GetSection("EnableSwagger").Get<bool>();
                ;
            }
            catch (Exception)
            { /*unused*/
            }

            app.UseFirebaseAuthentication($"https://securetoken.google.com/{firebaseProjectName}", firebaseProjectName);


            return app;
        }

        /// <summary>
        ///     USE COOKIE AUTH
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <param name="expirationCookie"></param>
        /// <param name="validExpiration"></param>
        /// <param name="slidingExpiration"></param>
        /// <param name="loginPath"></param>
        /// <param name="accessDeniedPath"></param>
        /// <param name="authenticationScheme"></param>
        /// <param name="cookieName"></param>
        /// <param name="useLocale"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCookieAuthCustom(this IApplicationBuilder app,
            IConfigurationRoot configuration,
            TimeSpan? expirationCookie,
            bool validExpiration = true,
            bool slidingExpiration = true,
            string loginPath = "/Account/Login/",
            string accessDeniedPath = "/Account/Login/",
            string authenticationScheme = "Cookie",
            string cookieName = "access_token",
            bool useLocale = true)
        {
            var baseConfig = new BaseConfig();
            configuration.GetSection("Jwt").Bind(baseConfig);
            configuration.GetSection("Config").Bind(baseConfig);

            try
            {
                BaseConfig.ApplicationName ??= configuration.GetSection("ApplicationName").Get<string>() ?? Assembly.GetEntryAssembly().GetName().Name?.Split('.')[0];
                BaseConfig.EnableSwagger = configuration.GetSection("EnableSwagger").Get<bool>();
                ;
            }
            catch (Exception) { /*unused*/ }


            if (string.IsNullOrEmpty(BaseConfig.Audience))
                throw new Exception("Informe o campo Audience do token");
            if (string.IsNullOrEmpty(BaseConfig.Issuer))
                throw new Exception("Informe o campo Issuer do token");

            if (string.IsNullOrEmpty(BaseConfig.SecretKey))
                throw new Exception("Informe o campo SecretKey do token");


            if (expirationCookie == null && (string.IsNullOrEmpty(BaseConfig.TokenFrom) || Math.Abs(BaseConfig.TokenValue) <= 0))
                throw new Exception("Informe o um ciclo de vida valido pra o token");

            if (!string.IsNullOrEmpty(BaseConfig.TokenFrom) && expirationCookie == null)
            {
                if (Math.Abs(BaseConfig.TokenValue) <= 0)
                    throw new Exception("Informe o tempo de validade do token");

                expirationCookie = BaseConfig.TokenFrom.GerateTimeSpan(BaseConfig.TokenValue);
            }

            if (useLocale)
                app.UseLocaleCustom(configuration);

            /**
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                AuthenticationScheme = authenticationScheme,
                CookieName = cookieName,
                ExpireTimeSpan = expirationCookie.GetValueOrDefault(),
                SlidingExpiration = slidingExpiration,
            });
            **/

            return app;
        }

        /// <summary>
        ///     SET LOCALE PT-BR
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseLocaleCustom(this IApplicationBuilder app, IConfiguration configuration)
        {
            var locale = configuration.GetSection("Language").Get<string>();
            if (string.IsNullOrEmpty(locale) == false)
            {
                var localizationOptions = new RequestLocalizationOptions
                {
                    SupportedCultures = new List<CultureInfo> { new CultureInfo(locale), new CultureInfo("en-GB") },
                    SupportedUICultures = new List<CultureInfo> { new CultureInfo(locale), new CultureInfo("en-GB") },
                    DefaultRequestCulture = new RequestCulture(locale)
                };
                app.UseRequestLocalization(localizationOptions);
            }

            return app;
        }

        /// <summary>
        ///     LOG BASICO
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="path"></param>
        /// <param name="all"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseRequestResponseLoggingLite(this IApplicationBuilder builder,
            string path = "api/", bool all = false, params string[] headers)
        {
            builder.Use(async (context, next) =>
            {
                var remoteIp = context.Connection.RemoteIpAddress.ToString();
                var acceptedIps = new HashSet<string>() { "127.0.0.1", "::1" };

                if (context.Request.Path == "/" && acceptedIps.Contains(remoteIp))
                {
                    context.Response.Redirect("/health");
                }
                else
                {
                    context.Request.EnableBuffering();
                    await next();
                }
            });

            return builder.UseMiddleware<RequestResponseLoggingMiddlewareLite>(path, all, headers);
        }

        /// <summary>
        ///     LOG FORMATADO
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="path"></param>
        /// <param name="all"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseRequestResponseLoggingFormated(this IApplicationBuilder builder,
            string path = "api/", bool all = false, params string[] headers)
        {
            builder.Use(async (context, next) =>
            {
                var remoteIp = context.Connection.RemoteIpAddress.ToString();
                var acceptedIps = new HashSet<string>() { "127.0.0.1", "::1" };

                if (context.Request.Path == "/" && acceptedIps.Contains(remoteIp))
                {
                    context.Response.Redirect("/health");
                }
                else
                {
                    context.Request.EnableBuffering();
                    await next();
                }
            });

            return builder.UseMiddleware<RequestResponseLoggingMiddlewareFormated>(path, all, headers);
        }

        /// <summary>
        /// ADD CUSTOM LOG IN PATH LOG/DATE.TXT
        /// </summary>
        /// <param name="loggerFactory"></param>
        /// <param name="configuration"></param>
        /// <param name="limitFiles"></param>
        /// <returns></returns>
        public static ILoggerFactory UseCustomLog(this ILoggerFactory loggerFactory,
                                                  IConfigurationRoot configuration,
                                                  string path = "Log/-.txt",
                                                  LogEventLevel restrictedToMinimumLevel = LogEventLevel.Warning,
                                                  int? retainedFileCountLimit = 7,
                                                  RollingInterval rollingInterval = RollingInterval.Day)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Warning()
            .WriteTo.File(
                path,
                restrictedToMinimumLevel,
                retainedFileCountLimit: retainedFileCountLimit,
                rollingInterval: rollingInterval
            )
            .CreateLogger();

            loggerFactory.AddSerilog();

            return loggerFactory;
        }

        private static Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            // Credentials are invalid, or account doesn't exist
            return Task.FromResult<ClaimsIdentity>(null);
        }
    }
}