using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace UltraliteRedditViewer.Infrastructure
{
    public class RedditAuthTokens
    {
        private const string AccessTokenKey = "access_token";
        private const string RefreshTokenKey = "refresh_token";
        private const string ExpiresKey = "expires_in";
        private const string TokenTypeKey = "token_type";

        /// <summary>
        /// Access token returned by provider. Can be used for further calls of provider API.
        /// </summary>
        public string AccessToken { get; private set; }

        /// <summary>
        /// Refresh token returned by provider. Can be used for further calls of provider API.
        /// </summary>
        public string RefreshToken { get; private set; }

        /// <summary>
        /// Token type returned by provider. Can be used for further calls of provider API.
        /// </summary>
        public string TokenType { get; private set; }

        /// <summary>
        /// Seconds till the token expires returned by provider. Can be used for further calls of provider API.
        /// </summary>
        public DateTime ExpiresAt { get; private set; }

        internal RedditAuthTokens(string content)
        {
            AccessToken = ParseTokenResponse(content, AccessTokenKey);
            RefreshToken = ParseTokenResponse(content, RefreshTokenKey);
            TokenType = ParseTokenResponse(content, TokenTypeKey);

            if (Int32.TryParse(ParseTokenResponse(content, ExpiresKey), out int expiresIn))
                ExpiresAt = DateTime.Now.AddSeconds(expiresIn);
        }

        internal RedditAuthTokens(string accessToken, string refreshToken, string tokenType, DateTime expiresAt)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            TokenType = tokenType;
            ExpiresAt = expiresAt;
        }

        private string ParseTokenResponse(string content, string key)
        {
            if (String.IsNullOrEmpty(content) || String.IsNullOrEmpty(key))
                return null;

            try
            {
                // response can be sent in JSON format
                var token = JObject.Parse(content).SelectToken(key);
                return token?.ToString();
            }
            catch (JsonReaderException)
            {
                // or it can be in "query string" format (param1=val1&param2=val2)
                var collection = HttpUtility.ParseQueryString(content);
                return collection[key];
            }
        }
    }
}
