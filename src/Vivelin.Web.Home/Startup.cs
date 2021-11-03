using System;
using System.Threading;
using System.Threading.Tasks;

using AspNet.Security.OAuth.Twitch;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Vivelin.AspNetCore.Headers;
using Vivelin.Web.Data;
using Vivelin.Web.Home.Authentication;

namespace Vivelin.Web.Home
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add
        // services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = TwitchAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                {
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SameSite = SameSiteMode.Lax;
                    options.LoginPath = "/login";
                    options.LogoutPath = "/logout";
                    options.EventsType = typeof(TwitchAuthenticationEvents);
                    options.ExpireTimeSpan = TimeSpan.FromDays(14);
                    options.SlidingExpiration = true;
                })
                .AddTwitch(options =>
                {
                    options.ClientId = Configuration["Twitch:ClientId"];
                    options.ClientSecret = Configuration["Twitch:ClientSecret"];
                    options.SaveTokens = true;
                    options.Scope.Add("user:read:follows");
                    options.Scope.Add("user:read:subscriptions");
                    options.Events = new OAuthEvents
                    {
                        OnTicketReceived = context =>
                        {
                            context.Properties.IsPersistent = true;
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireUserName("vivelin"));
                options.InvokeHandlersAfterFailure = false;
            });

            services.AddRazorPages(o =>
            {
                o.Conventions.Add(new PageRouteTransformerConvention(
                    new SlugParameterTransformer()));
                o.Conventions.AuthorizeFolder("/Admin", "Admin");
                o.Conventions.AuthorizePage("/Dashboard");
            });

            services.AddDbContext<DataContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("DataContext");
                options.UseSqlite(connectionString);
            });

            services.AddHttpClient("api.twitch.tv", client =>
            {
                client.BaseAddress = new Uri("https://api.twitch.tv/helix/");
                client.DefaultRequestHeaders.Add("client-id", Configuration["Twitch:ClientId"]);
            });

            services.AddSingleton<TwitchTokenClient>();
            services.AddSingleton<TwitchAuthenticationEvents>();
        }

        // This method gets called by the runtime. Use this method to configure
        // the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseResponseHeaders(options =>
            {
                options.PreventFraming();
                options.PreventContentTypeSniffing();
                options.AddContentSecurityPolicy(csp =>
                {
                    csp.Default.AllowFromSelf();
                    csp.Images.AllowFromSelf()
                        .AllowFromScheme("data:")
                        .AllowFromOrigin("https://static-cdn.jtvnw.net/");
                    csp.Styles.AllowFromSelf()
                        .AllowFromOrigin("https://fonts.googleapis.com/");
                    csp.Fonts.AllowFromSelf()
                        .AllowFromOrigin("https://fonts.gstatic.com/");

                });
                options.AddReferrerPolicy(ReferrerPolicy.OriginWhenCrossOrigin);
                options.Add("Permissions-Policy", "");
            });

            app.UseExceptionHandler("/Error");
            app.UseStatusCodePagesWithReExecute("/Error");

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}