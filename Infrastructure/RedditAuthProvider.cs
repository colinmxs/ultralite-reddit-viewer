using RestSharp;
using System;
using System.Text;
using System.Threading.Tasks;

namespace UltraliteRedditViewer.Infrastructure
{
    public class RedditAuthProvider
    {
        private const string AuthorizeUrl = "https://ssl.reddit.com/api/v1/authorize";

        //fucking RestClient MUST have a BaseUrl even if the RestRequest has a fully qualified url. Stupid.
        private const string SSLBaseUrl = "https://ssl.reddit.com";
        private const string WWWBaseUrl = "https://www.reddit.com";

        private const string RevokeTokenPath = "/api/v1/revoke_token";
        private const string AccessTokenPath = "/api/v1/access_token";

        private RestClient _webClient;
        private readonly string _redirectUri;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public RedditAuthProvider(string clientId, string clientSecret, string redirectUri)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;
            _redirectUri = redirectUri;
            _webClient = new RestClient();
        }        

        public string GetAuthUrl(string state, string scope, bool permanent = false)
        {
            return $"{AuthorizeUrl}?" +
                $"client_id={_clientId}&" +
                $"response_type=code&" +
                $"state={state}&" +
                $"redirect_uri={_redirectUri}&" +
                $"duration={(permanent ? "permanent" : "temporary")}&" +
                $"scope={(scope.ToString().Replace(" ", ""))}";
        }

        public async Task<RedditAuthTokens> GetTokenAsync(string code, bool isRefresh = false)
        {
            var request = new RestRequest(AccessTokenPath, Method.POST);
            request.AddHeader("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes(_clientId + ":" + _clientSecret))}");
            if (isRefresh)
            {
                request.AddObject(new
                {
                    grant_type = "refresh_token",
                    refresh_token = code
                });
            }
            else
            {
                request.AddObject(new
                {
                    grant_type = "authorization_code",
                    code,
                    redirect_uri = _redirectUri
                });
            }
            _webClient.BaseUrl = new Uri(SSLBaseUrl);
            var response = await _webClient.ExecuteTaskAsync(request);
            return new RedditAuthTokens(response.Content);
        }        

        public async Task RevokeTokenAsync(string token, bool isRefresh)
        {
            string tokenType = isRefresh ? "refresh_token" : "access_token";
            var request = new RestRequest(RevokeTokenPath, Method.POST);
            request.AddHeader("Authorization", $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes(_clientId + ":" + _clientSecret))}");

            request.AddObject(new
            {
                token = token,
                token_type = tokenType
            });
            _webClient.BaseUrl = new Uri(WWWBaseUrl);
            await _webClient.ExecutePostTaskAsync(request);
        }
    }
}
