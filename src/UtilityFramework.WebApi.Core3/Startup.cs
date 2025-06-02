using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UtilityFramework.Application.Core3;
using UtilityFramework.Services.Stripe.Core3;
using UtilityFramework.Services.Stripe.Core3.Interfaces;

namespace UtilityFramework.WebApi.Core3
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            BaseConfig.ApplicationName = ApplicationName =
            Configuration.GetSection("ApplicationName").Get<string>() ?? Assembly.GetEntryAssembly().GetName().Name?.Split('.')[0];
            EnableSwagger = Configuration.GetSection("EnableSwagger").Get<bool>();
            EnableService = Configuration.GetSection("EnableService").Get<bool>();

            /* CRIAR NO Settings json prop com array de cultures ["pt","pt-br"] */
            //var cultures = Utilities.GetConfigurationRoot().GetSection("TranslateLanguages").Get<List<string>>();
            //SupportedCultures = cultures.Select(x => new CultureInfo(x)).ToList();

        }

        public IConfigurationRoot Configuration { get; }
        public static string ApplicationName { get; set; }
        public static bool EnableSwagger { get; set; }
        public static bool EnableService { get; set; }

        /* PARA TRANSLATE*/
        //public static List<CultureInfo> SupportedCultures { get; set; } = new();


        // This method gets Race by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            /*TRANSLATE I18N*/
            //services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc(opt =>
            {
                opt.Filters.Add(typeof(CheckJson));
                opt.EnableEndpointRouting = false;
            })
            //.AddDataAnnotationsLocalization(options =>
            //{
            //    options.DataAnnotationLocalizerProvider = (type, factory) =>
            //     factory.Create(typeof(SharedResource));
            //})
            .AddNewtonsoftJson();

            services.AddHealthChecks();

            //services.AddApplicationInsightsTelemetry(Configuration);

            //services.Configure<RequestLocalizationOptions>(options =>
            //{
            //    options.DefaultRequestCulture = new RequestCulture("pt");
            //    options.SupportedCultures = SupportedCultures;
            //    options.SupportedUICultures = SupportedCultures;
            //});


            /*ENABLE CORS*/
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigin",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .Build());
            });

            /*CROP IMAGE*/
            services.AddImageResizer();

            /*ADD SWAGGER*/
            services.AddJwtSwagger(ApplicationName, enableSwaggerAuth: true);

            services.AddHttpContextAccessor();

            services.AddSingleton<IConfiguration>(Configuration);

            services.AddOptions();

            services.AddSingleton<IStripeCustomerService, StripeCustomerService>();
            services.AddSingleton<IStripePaymentMethodService, StripePaymentMethodService>();
            services.AddSingleton<IStripePaymentIntentService, StripePaymentIntentService>();
            services.AddSingleton<IStripeMarketPlaceService, StripeMarketPlaceService>();
            services.AddSingleton<IStripeTransferService, StripeTransferService>();


        }

        // This method gets Race by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider)
        {
            var maxDaysLogs = 5;
            var retainedFileCountLimit = 24 * maxDaysLogs;

            loggerFactory.UseCustomLog(Configuration,
                                       "Log/BRPAYS-.txt",
                                       retainedFileCountLimit: retainedFileCountLimit,
                                       rollingInterval: RollingInterval.Hour);

            Utilities.SetHttpContext(serviceProvider.GetService<IHttpContextAccessor>());

            /* TRANSLATE API */
            //var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();

            //app.UseRequestLocalization(options.Value);


            /*CROP IMAGE*/
            app.UseImageResizer();

            app.UseStaticFiles();

            var path = Path.Combine(Directory.GetCurrentDirectory(), @"Content");

            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(path),
                RequestPath = new PathString("/content")
            });

            app.UseCors("AllowAllOrigin");

            /*LOG BASICO*/
            app.UseRequestResponseLoggingLite();
            /*RETORNO COM GZIP*/
            app.UseResponseCompression();
            /*JWT TOKEN*/
            app.UseJwtTokenApiAuth(Configuration);

            app.Use(async (context, next) =>
            {
                var remoteIp = context.Connection.RemoteIpAddress.ToString();
                var acceptedIps = new HashSet<string>() { "127.0.0.1", "::1" };

                if (context.Request.Path == "/" && acceptedIps.Contains(remoteIp))
                {
                    context.Response.Redirect("/health");
                }
                else
                {
                    await next();
                }
            });

            app.UseMvc();

            if (EnableSwagger)
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"../swagger/v1/swagger.json".Trim(), $"API {ApplicationName} {env.EnvironmentName}");
                    c.EnableFilter();
                    c.EnableDeepLinking();
                });
            }

            app.UseAuthorization();

            app.UseHealthChecks("/health");
        }
    }
}
