using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

using Vivelin.Web.Home.Models;
using Vivelin.Web.Home.Twitch;

namespace Vivelin.Web.Home.Pages;

public class DashboardModel(IHttpClientFactory httpClientFactory,
    IAuthenticationService authService) : PageModel
{
    private static readonly JsonSerializerOptions s_jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = SnakeCaseNamingPolicy.SnakeCase
    };

    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("api.twitch.tv");

    public List<StreamInfo> Streams { get; set; } = [];

    public string? ErrorMessage { get; set; }

    public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken)
    {
        if (User.Identity is null || !User.Identity.IsAuthenticated)
        {
            return Challenge();
        }

        var accessToken = await authService.GetTokenAsync(HttpContext, "access_token");
        if (accessToken is null)
        {
            return Forbid();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
        {
            return Forbid();
        }

        var streams = await GetFromTwitchAsync<StreamsResponse>(
            $"streams/followed?user_id={Uri.EscapeDataString(userId)}",
            accessToken, cancellationToken);
        if (streams is not null && streams.Data.Count != 0)
        {
            var userIds = streams.Data.Select(x => x.UserId).Distinct().ToList();
            if (userIds.Count > 100)
            {
                ErrorMessage = "There are currently more than 100 users live.";
                return Page();
            }

            string usersQuery = string.Join("&id=", userIds);
            var users = await GetFromTwitchAsync<UsersResponse>(
                $"users?id={usersQuery}", accessToken, cancellationToken);

            Streams = StreamInfo.BuildList(streams.Data, users?.Data ?? []);
        }

        return Page();
    }

    private async Task<T?> GetFromTwitchAsync<T>(string requestUri, string accessToken, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        request.Headers.Authorization = new AuthenticationHeaderValue(
            "Bearer", accessToken);

        var response = await _httpClient.SendAsync(request, cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new HttpRequestException($"The request to {request.RequestUri} " +
                $"returned a {(int)response.StatusCode} ({response.ReasonPhrase}). " +
                $"Response content:\n{content}", null, response.StatusCode);
        }

        return await response.Content.ReadFromJsonAsync<T>(s_jsonSerializerOptions, cancellationToken);
    }
}