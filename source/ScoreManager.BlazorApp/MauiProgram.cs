using IdentityModel.Client;
using IdentityModel.OidcClient;
using ScoreManager.Api;
using ScoreManager.BlazorApp.Data;
using ScoreManagerApp;
using System.Net;

namespace ScoreManager.BlazorApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            ServicePointManager
                    .ServerCertificateValidationCallback +=
                    (sender, cert, chain, sslPolicyErrors) => true;
#if ANDROID
            Platforms.Android.DangerousAndroidMessageHandlerEmitter.Register();
            Platforms.Android.DangerousTrustProvider.Register();
#endif

            builder.Services.AddTransient<WebAuthenticatorBrowser>();
            builder.Services.AddSingleton<ScoreManagerApiSettings>();
            builder.Services.AddSingleton(typeof(IScoreManagerApiClient), typeof(ScoreManagerApiClient));
            //builder.Services.AddSingleton(typeof(IScoreManagerApiClient), new ScoreManagerApiClient("http://192.168.100.42:8081/"));
            builder.Services.AddTransient<OidcClient>(sp =>
               new OidcClient(new OidcClientOptions
               {
                   Authority = "https://sso-scoremanager.io/auth/realms/scoremanager",
                   ClientId = "scoremanager",
                   RedirectUri = "scoremanager://foo",
                   Scope = "openid email profile",
                   ClientSecret = "ZL1L0FuAs2wTMUK3tse5kTe3xtBSYLdS",
                   Browser = sp.GetRequiredService<WebAuthenticatorBrowser>(),
                   Policy = new Policy { Discovery = new DiscoveryPolicy { RequireHttps = false } },
               })
           );


            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
#endif

            builder.Services.AddSingleton<WeatherForecastService>();

            return builder.Build();
        }
    }
}