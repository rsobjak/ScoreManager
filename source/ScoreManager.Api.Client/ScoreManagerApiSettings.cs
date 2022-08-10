using IdentityModel.OidcClient;

namespace ScoreManager.Api
{
    public class ScoreManagerApiSettings
    {
        private readonly string _urlBase = "https://api-scoremanager.io/";
        private readonly OidcClient _oidcClient;

        public ScoreManagerApiSettings(OidcClient oidcClient)
        {
            _oidcClient = oidcClient;
        }

        public OidcClient GetOidcClient()
        {
            return _oidcClient;
        }

        public string GetUrlBase()
        {
            return _urlBase;
        }
    }
}