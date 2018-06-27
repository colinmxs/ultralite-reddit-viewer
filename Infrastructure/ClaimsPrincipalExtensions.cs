using System;
using System.Linq;
using System.Security.Claims;

namespace UltraliteRedditViewer.Infrastructure
{
    public static class ClaimsPrincipalExtensions
    {
        public static RedditAuthTokens GetRedditAuthTokens(this ClaimsPrincipal principal)
        {
            var claims = principal.Claims;

            var accessToken = claims.Single(c => c.Type == nameof(RedditAuthTokens.AccessToken)).Value;
            var refreshToken = claims.Single(c => c.Type == nameof(RedditAuthTokens.RefreshToken)).Value;
            var expiresAt = claims.Single(c => c.Type == nameof(RedditAuthTokens.ExpiresAt)).Value;
            var tokenType = claims.Single(c => c.Type == nameof(RedditAuthTokens.TokenType)).Value;

            return new RedditAuthTokens(accessToken, refreshToken, tokenType, DateTime.Parse(expiresAt));
        }
    }
}
