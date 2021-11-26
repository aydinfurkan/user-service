using System;
using System.Text;
using CoreLib.Middlewares.Error;
using CoreLib.Middlewares.Logging;
using CoreLib.Mongo.Context;
using CoreLib.Swagger;
using CoreLib.Validations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using UserService.Configs;
using UserService.Helpers.Authorize;
using UserService.Repositories;
using UserService.Services;

namespace UserService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly UserServiceSettings _mongoSettings;
        private readonly JwtTokenSettings _jwtTokenSettings;
        
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                .AddUserSecrets<Program>()
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            _mongoSettings = Configuration.GetSection("MongoSettings").Get<UserServiceSettings>();
            _jwtTokenSettings = Configuration.GetSection("JwtToken").Get<JwtTokenSettings>();
            //_jwtSecretKey = Configuration["JwtToken:SecretKey"];
            //_googleSecretsSettings = Configuration.GetSection("Google:Secrets").Get<GoogleSecretSettings>();
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTValidation<Startup>();
            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
            services.AddTSwaggerGen("User Service", "User service for Piksel");
            
            services.AddSingleton<IToken, JwtToken>(_ => new JwtToken(_jwtTokenSettings));
            services.AddSingleton<IUserService, Services.UserService>();
            var mongoClient = new MongoClient(_mongoSettings.MongoConnStr);
            services.AddSingleton<IContext, Context>(_ =>  new Context(mongoClient, _mongoSettings.MongoDbName));
            services.AddSingleton<IUserRepository, UserRepository>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)    
                .AddJwtBearer(options =>    
                {    
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters    
                    {    
                        ValidateIssuer = false,    
                        ValidateAudience = false,    
                        ValidateLifetime = true,    
                        ValidateIssuerSigningKey = true,    
                        ValidIssuer = _jwtTokenSettings.Issuer,    
                        ValidAudience = _jwtTokenSettings.Audience,    
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenSettings.SecretKey))    
                    };    
                });
            
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "http://localhost");
                        //builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "http://www.pkhood.com");
                        //builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "https://www.pkhood.com");
                        builder.AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials();
                    });
            });
            
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<ErrorLoggingMiddleware>("UserService");

            app.UseTSwaggerUI("User Service");

            app.UseRouting();
            
            app.UseCors();
            
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}