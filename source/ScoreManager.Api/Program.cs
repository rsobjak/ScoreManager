using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ScoreManager;
using ScoreManager.Data;
using ScoreManager.Extensions;
using Serilog;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json.Serialization;

try
{
    var builder = WebApplication.CreateBuilder(args);
    var oauthAuthority = builder.Configuration.GetValue<string>("OAuth2Authority");

    SerilogExtension.AddSerilogApi(builder.Configuration);
    builder.Host.UseSerilog(Log.Logger);

    // Add services to the container.
    builder.Services.AddDbContext<ApplicationDbContext>();
    builder.Services.AddHttpContextAccessor();
    builder.Services.ConfigureSelfBindableEntities();
    builder.Services.AddCors();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "Bearer";
        options.DefaultChallengeScheme = "Bearer";
    })
    //.AddCookie(setup => setup.ExpireTimeSpan = TimeSpan.FromSeconds(10))
    //.AddOpenIdConnect("Bearer", options =>
    //{
    //    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //    options.Authority = "http://localhost:8080/auth/realms/scoremanager";
    //    options.SignedOutRedirectUri = "/sign-out";
    //    options.ClientId = "scoremanager";
    //    options.ClientSecret = "ZL1L0FuAs2wTMUK3tse5kTe3xtBSYLdS";
    //    options.ResponseType = "code id_token";
    //    options.SaveTokens = false;
    //    options.GetClaimsFromUserInfoEndpoint = true;
    //    options.RequireHttpsMetadata = false;
    //    options.Scope.Add("openid");
    //    options.Scope.Add("profile");
    //});
    .AddJwtBearer("Bearer", o =>
     {
         o.BackchannelHttpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = delegate { return true; } };
         o.Authority = oauthAuthority;
         o.Audience = builder.Configuration.GetValue<string>("OAuth2ClientId");
         o.SaveToken = true;
         o.IncludeErrorDetails = true;
         o.RequireHttpsMetadata = false;
         o.TokenValidationParameters = new TokenValidationParameters
         {
             NameClaimType = ClaimTypes.Email,
             RoleClaimType = ClaimTypes.Role,
         };
     });

    builder.Services.AddControllers()
                    .AddJsonOptions(x =>
                    {
                        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                        //x.JsonSerializerOptions.IgnoreNullValues = true;
                    }
                    );

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "ScoreManager API",
            Description = "An ASP.NET Core Web API for managing ScoreManager solution data",
            Contact = new OpenApiContact
            {
                Name = "Developer",
                Email = "rodrigo.sobjak@gmail.com"
            },
        });
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows()
            {
                Implicit = new OpenApiOAuthFlow()
                {
                    AuthorizationUrl = new Uri($"{oauthAuthority}/protocol/openid-connect/auth"),
                    TokenUrl = new Uri($"{oauthAuthority}/protocol/openid-connect/token"),
                    Scopes = new Dictionary<string, string>
                    {
                        { "read", "Reads" }
                    }
                }
            }
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        },
                        Scheme = "oauth2",
                        Name = "oauth2",
                        In = ParameterLocation.Header,
                    },
                    new List<string>(){ "read", "admin"}
                }
            });

        // using System.Reflection;
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

    var app = builder.Build();

    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseMiddleware<RequestSerilLogMiddleware>();

    // Configure the HTTP request pipeline.
    //if (app.Environment.IsDevelopment())
    //{
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.DisplayOperationId();
            options.EnableTryItOutByDefault();
            options.EnablePersistAuthorization();
            options.EnableDeepLinking();
            options.EnableValidator();
            options.DisplayRequestDuration();
            options.ShowExtensions();
            options.ShowCommonExtensions();
            options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);

            options.OAuthClientId(app.Configuration.GetValue<string>("OAuth2ClientId"));
            options.OAuthClientSecret(app.Configuration.GetValue<string>("OAuth2ClientSecret"));
            options.OAuthAppName(app.Configuration.GetValue<string>("OAuth2ClientId"));
        });
    //}

    //app.UseHttpsRedirection();

    app.UseCors(x =>
    {
        x.AllowAnyMethod();
        x.AllowAnyOrigin();
        x.AllowAnyHeader();
    });

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.Information("Server Shutting down...");
    Log.CloseAndFlush();
}