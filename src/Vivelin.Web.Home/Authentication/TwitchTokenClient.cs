using AspNet.Security.OAuth.Twitch;

using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Options;

using System.Text;
using System.Text.Json;

namespace Vivelin.Web.Home.Authentication;

/// <summary>
/// Represents a client used to refresh Twitch access tokens.
/// </summary>
/// <param name="clientId">The Twitch client ID.</param>
/// <param name="clientSecret">The Twitch client secret.</param>
public class TwitchTokenClient(IOptionsMonitor<TwitchAuthenticationOptions> options)
{
    private static readonly HttpClient s_httpClient = new();

    /// <summary>
    /// Gets or sets the Twitch OAuth token endpoint address.
    /// </summary>
    public Uri TokenEndpoint { get; set; }
        = new Uri(TwitchAuthenticationDefaults.TokenEndpoint);

    protected TwitchAuthenticationOptions Options
        => options.Get(TwitchAuthenticationDefaults.AuthenticationScheme);

    /// <summary>
    /// Requests a new access token based on a given OAuth refresh token.
    /// </summary>
    /// <param name="refreshToken">
    /// The refresh token issued to the client.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> that returns the OAuth token endpoint response.
    /// </returns>
    public async Task<OAuthTokenResponse> RefreshAccessTokenAsync(string refreshToken)
    {
        Dictionary<string, string> tokenRequestParameters = new()
        {
            {"grant_type", "refresh_token" },
            {"refresh_token", refreshToken },
            {"client_id", Options.ClientId },
            {"client_secret", Options.ClientSecret },
            { "scope", string.Join(" ", Options.Scope) }
        };

        FormUrlEncodedContent requestContent = new(tokenRequestParameters);
        HttpRequestMessage requestMessage = new(HttpMethod.Post, TokenEndpoint);
        requestMessage.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        requestMessage.Content = requestContent;

        var response = await s_httpClient.SendAsync(requestMessage);
        if (response.IsSuccessStatusCode)
        {
            JsonDocument payload = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            return OAuthTokenResponse.Success(payload);
        }
        else
        {
            var error = await GetExceptionAsync(response);
            return OAuthTokenResponse.Failed(error);
        }
    }

    private static async Task<Exception> GetExceptionAsync(HttpResponseMessage response)
    {
        StringBuilder output = new();
        output.Append("Status: ").Append(response.StatusCode).Append(";");
        output.Append("Headers: ").Append(response.Headers.ToString()).Append(";");
        output.Append("Body: ").Append(await response.Content.ReadAsStringAsync()).Append(";");
        return new Exception("OAuth token endpoint failure: " + output.ToString());
    }
}