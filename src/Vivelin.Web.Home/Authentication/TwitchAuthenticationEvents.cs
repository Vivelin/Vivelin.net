﻿using System.Globalization;
using System.Security.Claims;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Vivelin.Web.Home.Authentication;

/// <summary>
/// Refreshes expires Twitch access tokens.
/// </summary>
public class TwitchAuthenticationEvents : CookieAuthenticationEvents
{
    private const string RefreshTokenName = "refresh_token";
    private const string AccessTokenName = "access_token";
    private const string ExpirationDateTokenName = "expires_at";
    private const string TokenTypeTokenName = "token_type";

    /// <summary>
    /// Initializes a new instance of the <see
    /// cref="TwitchAuthenticationEvents"/> class with the specified
    /// dependencies.
    /// </summary>
    /// <param name="clock">
    /// A system clock used to determine the absolute expiration date for a
    /// new token.
    /// </param>
    /// <param name="tokenClient">
    /// A client used to refresh an access token.
    /// </param>
    public TwitchAuthenticationEvents(ISystemClock clock, TwitchTokenClient tokenClient)
    {
        Clock = clock ?? throw new ArgumentNullException(nameof(clock));
        TokenClient = tokenClient ?? throw new ArgumentNullException(nameof(tokenClient));
    }

    /// <summary>
    /// Gets the system clock used to determine the absolute expiration date
    /// for a new token.
    /// </summary>
    protected ISystemClock Clock { get; }

    /// <summary>
    /// Gets the client used to refresh an access token.
    /// </summary>
    protected TwitchTokenClient TokenClient { get; }

    /// <summary>
    /// Called each time a request principal has been validated by the
    /// middleware. By implementing this method the application may alter or
    /// reject the principal which has arrived with the request.
    /// </summary>
    /// <param name="context">
    /// Contains information about the login session as well as the user
    /// <see cref="ClaimsIdentity"/>.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> representing the completed operation.
    /// </returns>
    public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
    {
        if (context.Principal?.Identity?.IsAuthenticated != true)
            return;

        var refreshToken = context.Properties.GetTokenValue(RefreshTokenName);
        // var accessToken =
        // context.Properties.GetTokenValue(AccessTokenName);
        var expiresAt = context.Properties.GetTokenValue(ExpirationDateTokenName);
        if (refreshToken != null
            && DateTimeOffset.TryParse(expiresAt, out var expirationDate)
            && expirationDate <= DateTimeOffset.Now)
        {
            var tokenResponse = await TokenClient.RefreshAccessTokenAsync(refreshToken);
            if (tokenResponse.Error != null)
                throw tokenResponse.Error;

            if (tokenResponse.AccessToken == null)
                throw new InvalidOperationException("Unexpected null in AccessToken after refresh");

            var tokens = new List<AuthenticationToken>
            {
                new AuthenticationToken { Name = AccessTokenName, Value = tokenResponse.AccessToken }
            };

            if (!string.IsNullOrEmpty(tokenResponse.RefreshToken))
                tokens.Add(new AuthenticationToken { Name = RefreshTokenName, Value = tokenResponse.RefreshToken });

            if (!string.IsNullOrEmpty(tokenResponse.TokenType))
                tokens.Add(new AuthenticationToken { Name = TokenTypeTokenName, Value = tokenResponse.TokenType });

            if (!string.IsNullOrEmpty(tokenResponse.ExpiresIn)
                && int.TryParse(tokenResponse.ExpiresIn, out var value))
            {
                var newExpirationDate = Clock.UtcNow + TimeSpan.FromSeconds(value);
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
