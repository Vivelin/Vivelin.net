using AspNet.Security.OAuth.Twitch;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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

        // This method gets called by the runtime. Use this method to add services to the container.
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
                    options.Cookie.SameSite = SameSiteMode.Strict;
                })
                .AddTwitch(options =>
                {
                    options.ClientId = Configuration["Twitch:ClientId"];
                    options.ClientSecret = Configuration["Twitch:ClientSecret"];
                    options.SaveTokens = true;
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireUserName("vivelin"));
            });

            services.AddOptions<CookieAuthenticationOptions>(
                CookieAuthenticationDefaults.AuthenticationScheme)
                .Configure<TwitchAuthenticationEvents>((options, events) =>
                {
                    options.LoginPath = "/login";
                    options.LogoutPath = "/logout";
                    options.Events = events;
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

            services.AddSingleton(new TwitchTokenClient(
                Configuration["Twitch:ClientId"], 
                Configuration["Twitch:ClientSecret"]));
            services.AddSingleton<TwitchAuthenticationEvents>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
