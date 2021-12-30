using HangilFA.Areas.Identity.Data;
using HangilFA.Data;
using HangilFA.Handlers;
using HangilFA.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(HangilFA.Areas.Identity.IdentityHostingStartup))]
namespace HangilFA.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {

                //services.Configure<CookiePolicyOptions>(options =>
                //{
                //    options.CheckConsentNeeded = context => true;
                //    options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
                //});

                services.AddDbContext<HangilFADBContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("HangilFADBContextConnection")));

                services.AddDefaultIdentity<HangilFAUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    //options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<HangilFADBContext>();

                services.AddControllersWithViews().AddRazorRuntimeCompilation();
                services.AddRazorPages();
                services.AddMemoryCache();

                services.AddScoped<IDataAccessService, DataAccessService>();
                services.AddScoped<IAuthorizationHandler, PermissionHandler>();
                services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
                //services.Configure<IdentityOptions>(options =>
                //{
                //    // Password settings.
                //    options.Password.RequireDigit = true;
                //    options.Password.RequireLowercase = true;
                //    options.Password.RequireNonAlphanumeric = true;
                //    options.Password.RequireUppercase = true;
                //    options.Password.RequiredLength = 6;
                //    options.Password.RequiredUniqueChars = 1;

                //    // Lockout settings.
                //    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                //    options.Lockout.MaxFailedAccessAttempts = 5;
                //    options.Lockout.AllowedForNewUsers = true;

                //    // User settings.
                //    options.User.AllowedUserNameCharacters =
                //    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                //    options.User.RequireUniqueEmail = false;
                //});

                //services.ConfigureApplicationCookie(options =>
                //{
                //    // Cookie settings
                //    options.Cookie.HttpOnly = true;
                //    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                //    options.LoginPath = "/Identity/Account/Login";
                //    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                //    options.SlidingExpiration = true;
                //});
            });
        }
    }
}