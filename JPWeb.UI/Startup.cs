using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JPWeb.UI.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using JPWeb.UI.Data.Model;

namespace JPWeb.UI
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddIdentity<ApplicationUser, IdentityRole>(options => options.Stores.MaxLengthForKeys = 128)
            //   .AddEntityFrameworkStores<ApplicationDbContext>()
            //   .AddDefaultUI()
            //   .AddDefaultTokenProviders();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMvc();

            services.AddTransient<UserManager<ApplicationUser>>();
            services.AddTransient<ApplicationDbContext>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("UserAndHigherPolicy", p =>
                {
                    p.RequireAssertion(context =>
                    {
                        return context.User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value.Equals("User"))
                            || context.User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value.Equals("Admin"))
                            || context.User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value.Equals("SuperAdmin"));
                    });
                });
                options.AddPolicy("AdminAndHigherPolicy", p =>
                {
                    p.RequireAssertion(context =>
                    {
                        return context.User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value.Equals("Admin"))
                            || context.User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value.Equals("SuperAdmin"));
                    });
                });
                options.AddPolicy("SuperAdminPolicy", p =>
                {
                    p.RequireAssertion(context =>
                    {
                        return context.User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value.Equals("SuperAdmin"));
                    });
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
           // app.UseIdentity();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
