using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using POS_Api.Helpers;
using POS_Api.ServiceInterfaces.BusinessCentral;
using POS_Api.ServiceInterfaces.Cache;
using POS_Api.ServiceInterfaces.Creditors;
using POS_Api.ServiceInterfaces.Debtors;
using POS_Api.ServiceInterfaces.EntityData;
using POS_Api.ServiceInterfaces.Inventory;
using POS_Api.ServiceInterfaces.Logging;
using POS_Api.ServiceInterfaces.Menu;
using POS_Api.ServiceInterfaces.Stock;
using POS_Api.ServiceInterfaces.Email;
using POS_Api.ServiceInterfaces.Sync;
using POS_Api.Services;
using POS_Api.Services.BusinessCentral;
using POS_Api.Services.Email;
using POS_Api.Services.Cache;
using POS_Api.Services.EntityData;
using POS_Api.Services.Inventory;
using POS_Api.Services.Logging;
using POS_Api.Services.Sync;
using POS_Webservice.Helpers;
using Serilog;
using System.Data;
using System.Net;
using System.Text;
using TMIS_Common.Interfaces;
using TMIS_Common.Models;

{
    var builder = WebApplication.CreateBuilder(args);

    #region SeriLog

    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();

    builder.Host.UseSerilog((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration));

    builder.Services.Configure<LoggingConfig>(builder.Configuration.GetSection("LoggingConfig"));
    builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<LoggingConfig>>().Value);

    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

    builder.Services.AddScoped<IDbConnection>(sp =>
        new SqlConnection(builder.Configuration.GetConnectionString("ApplicationLoggingDatabase")));

    builder.Services.AddScoped<ILogging_Service, Logging_Service>();

    #endregion

    #region Validation

    builder.Services.AddFluentValidationAutoValidation();

    #endregion

    #region Controllers

    builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
        });

    builder.Services.AddHttpContextAccessor();
    builder.Services.AddScoped<IUserContext, UserContext>();

    builder.Services.AddSingleton<ICache_Service, Cache_Service>();

    //builder.Services.AddHostedService<RequestFromSync_Service>();

    builder.Services.AddScoped<IInventory_Service, Inventory_Service>();
    builder.Services.AddScoped<IEntityData_Service, EntityData_Service>();
    builder.Services.AddScoped<IDebtors_Service, Debtors_Service>();
    builder.Services.AddScoped<ICreditors_Service, Creditors_Service>();
    builder.Services.AddScoped<IStock_Service, Stock_Service>();
    builder.Services.AddScoped<IMenu_Service, Menu_Service>();
    builder.Services.AddScoped<ISync_Service, Sync_Service>();
    builder.Services.AddScoped<IEmail_Service, Email_Service>();
    builder.Services.AddScoped<ImageHelper>();

    #endregion

    #region JWT

    async Task<SymmetricSecurityKey> GetPortalSigningKey(IServiceProvider services)
    {
        var config = services.GetRequiredService<IConfiguration>();
        var httpClient = new HttpClient();
        var portalUrl = config["PortalSettings:WellKnownUrl"]; // example: https://yourportal.com/.well-known/keys

        var keyResponse = await httpClient.GetFromJsonAsync<KeyResponse>(portalUrl);

        if (keyResponse == null || string.IsNullOrEmpty(keyResponse.k))
            throw new Exception("Failed to fetch signing key from Portal.");

        var keyBytes = Convert.FromBase64String(keyResponse.k);

        return new SymmetricSecurityKey(keyBytes);
    }

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKeyResolver = (token, securityToken, kid, validationParameters) =>
            {
                var key = GetPortalSigningKey(builder.Services.BuildServiceProvider()).GetAwaiter().GetResult();
                return new[] { key };
            }
        };
    });

    #endregion

    #region CORS

    var allowedOrigins = builder.Configuration
        .GetSection("Cors:AllowedOrigins")
        .Get<string[]>();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowFrontend", policy =>
        {
            policy
                .WithOrigins(allowedOrigins)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
    });

    #endregion

    #region Business Central Integration DI

    builder.Services.AddMemoryCache();

    // Token provider & handler
    builder.Services.AddSingleton<IBcTokenProvider, BcTokenProvider>();
    builder.Services.AddTransient<BcAuthHandler>();

    // HttpClient configured for BC API with automatic bearer injection
    builder.Services.AddHttpClient("bc")
        .AddHttpMessageHandler<BcAuthHandler>()
        .ConfigureHttpClient((sp, client) =>
        {
            var cfg = sp.GetRequiredService<IConfiguration>();
            var section = cfg.GetSection("BusinessCentral");
            var timeout = section.GetValue<int?>("RequestTimeoutSeconds") ?? 100;
            client.Timeout = TimeSpan.FromSeconds(timeout);
        });

    // High-level BC service
    builder.Services.AddSingleton<IBusinessCentral_Service, BusinessCentral_Service>();

    // Spec 3: BC push (paid POS invoices -> BC sales orders -> Microsoft.NAV.shipAndInvoice).
    // Scoped because it talks to per-request DB state.
    builder.Services.AddScoped<IBc_Push_Service, Bc_Push_Service>();

    // Timed hosted sync
    builder.Services.AddHostedService<BcSyncHostedService>();
    builder.Services.AddHostedService<BcPushHostedService>();
    builder.Services.AddHostedService<POS_Api.Services.SilentSite.SilentSiteDetector_Service>();

    #endregion

    #region Swagger

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "TMIS_Webservice",
            Version = "v1"
        });

        // JWT Bearer definition
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Enter 'Bearer' followed by your token. Example: Bearer abc123"
        });

        // Global security requirement
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });
    });

    #endregion

    var app = builder.Build();

    app.UseCors("AllowFrontend");

    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();

    if (builder.Configuration["LiveApp"] == "true")
    {
        // Ensure the assets folder exists before PhysicalFileProvider validates it.
        // CreateDirectory is idempotent: no-op if already present.
        var assetsPath = Path.Combine(builder.Environment.ContentRootPath, "assets");
        Directory.CreateDirectory(assetsPath);

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(assetsPath),
            RequestPath = "/assets"
        });
    }

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseMiddleware<POS_Api.Middleware.SiteHeartbeatMiddleware>();

    app.MapControllers();

    app.Run();
}
