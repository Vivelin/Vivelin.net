using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vivelin.Web.Home.Models;
using Vivelin.Web.Home.Twitch;

namespace Vivelin.Web.Home.Pages
{
    public class DashboardModel : PageModel
    {
        private static readonly JsonSerializerOptions s_jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = SnakeCaseNamingPolicy.SnakeCase
        };

        private readonly HttpClient _httpClient;
        private readonly IAuthenticationService _authService;

        public DashboardModel(IHttpClientFactory httpClientFactory,
            IAuthenticationService authService)
        {
            _httpClient = httpClientFactory.CreateClient("api.twitch.tv");
            _authService = authService;
        }

        public List<StreamInfo> Streams { get; set; }

        public async Task<IActionResult> OnGetAsync(CancellationToken cancellationToken)
        {
            if (!User.Identity.IsAuthenticated)
                return Challenge();

            var accessToken = await _authService.GetTokenAsync(HttpContext, "access_token");
            if (accessToken == null)
                return Forbid();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var streams = await GetFromTwitchAsync<StreamsResponse>(
                $"streams/followed?user_id={Uri.EscapeDataString(userId)}",
                accessToken, cancellationToken);

            var usersQuery = string.Join("&id=", streams.Data.Select(x => x.UserId).Distinct());
            var users = await GetFromTwitchAsync<UsersResponse>(
                $"users?id={usersQuery}", accessToken, cancellationToken);

            Streams = StreamInfo.BuildList(streams.Data, users.Data);
            return Page();
        }

        private async Task<T> GetFromTwitchAsync<T>(string requestUri, string accessToken, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            request.Headers.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var response = await _httpClient.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new HttpRequestException($"The request to {request.RequestUri} " +
                    $"returned a {(int)response.StatusCode} ({response.ReasonPhrase}). " +
                    $"Response content:\n{content}", null, response.StatusCode);
            }

            return await response.Content.ReadFromJsonAsync<T>(s_jsonSerializerOptions, cancellationToken);
        }
    }
}