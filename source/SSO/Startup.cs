using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Services;
using SSO.Core;
using SSO.Core.Extensions;
using SSO.Extensions;
using Swashbuckle.AspNetCore.Swagger;

namespace SSO
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

            // Configure database
            services.AddSSOIdentity(options => options.UseSqlServer(Configuration.GetConnectionString("SSO")));
            services.AddOIDC(options => options.UseSqlServer(Configuration.GetConnectionString("SSO")));
            services.AddSSOServices();


            services.AddSwaggerGen(op =>
            {
                op.SwaggerDoc("v1", new Info
                {
                    Title = "Titan SSO",
                    Version = "v1",
                    Description = "Titan single sign-on service",
                    Contact = new Contact
                    {
                        Name = "Daryl Clarino",
                        Email = "clarinojd@gmail.com",
                        Url = "https://madtitan.com/clarinojd"
                    },
                    License = new License
                    {
                        Name = "MIT License",
                        Url = "https://opensource.org/licenses/MIT"
                    }
                });
                op.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            services.AddAuthentication("Bearer")
                    .AddCookie(Constants.ProviderCookies.Facebook)
                    .AddCookie(Constants.ProviderCookies.Google)
                    .AddCookie(Constants.ProviderCookies.Twitter)
                    .AddJwtBearer("Bearer", options =>
                    {
                        options.Authority = Configuration["Host"];
                        options.RequireHttpsMetadata = false;

                        options.Audience = "admin";
                    })
                    .AddGoogle("Google", op =>
                    {
                        op.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                        op.ClientId = Configuration["ExternalProviders:google:clientid"];
                        op.ClientSecret = Configuration["ExternalProviders:google:clientsecret"];
                        op.Scope.Add("email");
                        op.Scope.Add("profile");
                        op.Scope.Add("openid");
                    });

            services.AddSession();

            services
                .AddMvc()
                .AddSessionStateTempDataProvider()
                .AddJsonOptions(options => { options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
                    }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Secure Single Sign-on");
            });
            app.UseCookiePolicy();
            app.UseSession();
            app.UseAuthentication();
            app.UseIdentityServer();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
