Adicionar no appsettings.json

  "googleMapsKey":"",
  "Language":"pt-BR",
  "Jwt": {
	  "ProjectFirebaseId":"", //caso use auth firebase
    "Issuer": "",
    "Audience": "",
    "SecretKey": "",
    "TokenFrom": "fromminutes",  //fromdays ,fromhours,frommilliseconds,fromseconds,fromminutes
    "TokenValue": 5.0 // double
  },
   "Config": {
    "BaseUrl": "",
    "DefaultUrl": "",
	  "CustomUrls":[""] // opcional array
  },

Adicionar no StartUp.cs

  public IConfigurationRoot Configuration { get; }

  // This method gets called by the runtime. Use this method to add services to the container.
  public void ConfigureServices(IServiceCollection services)
  {
      // Add framework services.
      services.AddMvc();

       /*ENABLE CORS*/
      services.AddCors(options =>
      {
          options.AddPolicy("AllowAllOrigin",
              builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials().Build());
      });
       /*CROP IMAGE*/
       services.AddImageResizer();

      services.AddJwtSwagger("Api Teste");
  }

  // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
  public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
  {
      loggerFactory.UseCustomLog(Configuration);
      
      /*CROP IMAGE*/
      app.UseImageResizer();
      
      app.UseStaticFiles();
      if (!env.IsDevelopment())
      {
          var path = Path.Combine(Directory.GetCurrentDirectory(), @"Content");

          if (!Directory.Exists(path))
              Directory.CreateDirectory(path);

          app.UseStaticFiles(new StaticFileOptions()
          {
              FileProvider = new PhysicalFileProvider(path),
              RequestPath = new PathString("/content")
          });
      }

      app.UseCors("AllowAllOrigin");
      app.UseRequestResponseLoggingLite();
      app.UseResponseCompression();
      app.UseJwtTokenApiAuth(Configuration);

      app.UseMvc();
      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
          c.SwaggerEndpoint("/swagger/v1/swagger.json", "Teste");
      });
  }
