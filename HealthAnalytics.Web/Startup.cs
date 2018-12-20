using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using HealthAnalytics.Data.UnitOfWork;
using HealthAnalytics.BusinessLogic.Services.Abstract;
using HealthAnalytics.BusinessLogic.Services.Implementation;
using MongoDB.Bson;
using HealthAnalytics.Web.Middlewares;
using HealthAnalytics.BusinessLogic;

namespace HealthAnalytics.Web
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.ISSUER,
                        ValidateAudience = false,
                        ValidAudience = AuthOptions.AUDIENCE,
                        ValidateLifetime = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true
                    };
                });
            services.AddTransient<IUnitOfWork<ObjectId>, MongoUnitOfWork>();
            services.AddTransient<IHashingService, Md5HashingService>();
            services.AddTransient<ISMSService, SMSService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IUserService, UserService>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => 
            {
                builder.AllowAnyHeader();
                builder.AllowAnyOrigin();
                builder.AllowCredentials();
                builder.AllowAnyMethod();
            });
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMiddleware(typeof(ExceptionHandler));
            app.UseMvcWithDefaultRoute();
        }
    }
}
