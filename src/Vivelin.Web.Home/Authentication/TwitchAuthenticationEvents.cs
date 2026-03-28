using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

using System.Globalization;
using System.Security.Claims;

namespace Vivelin.Web.Home.Authentication;

/// <summary>
/// Refreshes expires Twitch access tokens.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="TwitchAuthenticationEvents"/>
/// class with the specified dependencies.
/// </remarks>
/// <param name="clock">
/// A system clock used to determine the absolute expiration date for a new
/// token.
/// </param>
/// <param name="tokenClient">A client used to refresh an access token.</param>
public class TwitchAuthenticationEvents(TimeProvider timeProvider, TwitchTokenClient tokenClient) : CookieAuthenticationEvents
{
    private const string RefreshTokenName = "refresh_token";
    private const string AccessTokenName = "access_token";
    private const string ExpirationDateTokenName = "expires_at";
    private const string TokenTypeTokenName = "token_type";

    /// <summary>
    /// Called each time a request principal has been validated by the
    /// middleware. By implementing this method the application may alter or
    /// reject the principal which has arrived with the request.
    /// </summary>
    /// <param name="context">
    /// Contains information about the login session as well as the user <see
    /// cref="ClaimsIdentity"/>.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> representing the completed operation.
    /// </returns>
    public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
    {
        if (context.Principal?.Identity is null || !context.Principal.Identity.IsAuthenticated)
        {
            return;
        }

        var refreshToken = context.Properties.GetTokenValue(RefreshTokenName);
        var accessToken = context.Properties.GetTokenValue(AccessTokenName);
        var expiresAt = context.Properties.GetTokenValue(ExpirationDateTokenName);
        if (DateTimeOffset.TryParse(expiresAt, out var expirationDate)
            && expirationDate <= DateTimeOffset.Now)
        {
            var tokenResponse = await tokenClient.RefreshAccessTokenAsync(refreshToken);
            if (tokenResponse.Error is not null)
            {
                throw tokenResponse.Error;
            }

            if (tokenResponse.AccessToken is null)
            {
                throw new InvalidOperationException($"Failed to refresh access token. Access token is null, but no error was thrown.");
            }

            var tokens = new List<AuthenticationToken>
            {
                new() { Name = AccessTokenName, Value = tokenResponse.AccessToken }
            };

            if (!string.IsNullOrEmpty(tokenResponse.RefreshToken))
            {
                tokens.Add(new AuthenticationToken { Name = RefreshTokenName, Value = tokenResponse.RefreshToken });
            }

            if (!string.IsNullOrEmpty(tokenResponse.TokenType))
            {
                tokens.Add(new AuthenticationToken { Name = TokenTypeTokenName, Value = tokenResponse.TokenType });
            }

            if (!string.IsNullOrEmpty(tokenResponse.ExpiresIn)
                && int.TryParse(tokenResponse.ExpiresIn, out int value))
            {
                var newExpirationDate = timeProvider.GetUtcNow() + TimeSpan.FromSeconds(value);
                tokens.Add(new AuthenticationToken
                {
                    Name = ExpirationDateTokenName,
                    Value = newExpirationDate.ToString("o", CultureInfo.InvariantCulture)
                });
            }

            context.Properties.StoreTokens(tokens);
            context.ShouldRenew = true;
        }
    }
}
