using IdentityModel.OidcClient;

namespace ScoreManager.Api
{
    public abstract class ScoreManagerApiBase : IApiBase
    {
        protected string BaseUrl;
        private readonly OidcClient _oidcClient;
        private string _token;
        private string _username;
        private string _email;
        private string _expirationDate;

        public string Username => _username;

        public ScoreManagerApiBase(ScoreManagerApiSettings settings)
        {
            this._oidcClient = settings.GetOidcClient();
            this.BaseUrl = settings.GetUrlBase();

        }
        public string GetEmail()
        {
            return _email;
        }

        public string GetUsername()
        {
            return _username;
        }
        public async Task LoginAsync()
        {
            var loginResult = await _oidcClient.LoginAsync(new LoginRequest());
            _token = loginResult.AccessToken;
            var mail = loginResult.User.Claims.FirstOrDefault(s => s.Type == "email");
            _email = mail != null ? mail.Value : string.Empty;
            _username = loginResult.User.Identity.Name;
        }

        public async Task LogoutAsync()
        {
            await Task.Delay(5);
            this._token = null;
            this._username = null;
            this._email = null;
        }

        protected Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken)
        {
            var msg = new HttpRequestMessage();
            // SET THE BEARER AUTH TOKEN
            msg.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
            return Task.FromResult(msg);
        }

        public bool IsAuthenticated()
        {
            return _token != null;
        }

        public bool IsNotAuthenticated()
        {
            return !IsAuthenticated();
        }
    }
}