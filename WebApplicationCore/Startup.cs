using System;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using WebApplicationCore.Domain.Repositories;
using WebApplicationCore.Domain.Security.Hashing;
using WebApplicationCore.Domain.Security.Tokens;
using WebApplicationCore.Domain.Services;
using WebApplicationCore.Extensions;
using WebApplicationCore.Persistence.Contexts;
using WebApplicationCore.Persistence.Repositories;
using WebApplicationCore.Security.Hashing;
using WebApplicationCore.Security.Tokens;
using WebApplicationCore.Services;


namespace WebApplicationCore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddCustomSwagger();

            //configuring context
            services.AddDbContext<AppDbContext>(options => {
                options.UseInMemoryDatabase("supermarket-api-in-memory");
            });

            //dependency injection configuration
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

            //authentication
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<ITokenHandler, Security.Tokens.TokenHandler>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();


            services.Configure<TokenOptions>(Configuration.GetSection("TokenOptions"));
            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        IssuerSigningKey = signingConfigurations.Key,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddAutoMapper(this.GetType().Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCustomSwagger();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
