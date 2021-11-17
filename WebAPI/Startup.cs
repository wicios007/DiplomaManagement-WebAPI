using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Entities;
using WebAPI.Interfaces;
using WebAPI.Middleware;
using WebAPI.Models;
using WebAPI.Models.Validators;
using WebAPI.Services;

namespace WebAPI
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
            var authenticationSettings = new AuthenticationSettings();
            Configuration.GetSection("Authentication").Bind(authenticationSettings);

            services.AddSingleton(authenticationSettings);

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer";
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authenticationSettings.JwtIssuer,
                    ValidAudience = authenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))

                };
            });
            //services.AddTransient<IWeatherForecastService, WeatherForecastService>(); //za kazdym razem zostanie utworzony nowy obiekt
            //services.AddScoped<IWeatherForecastService>();    //tworzy obiekt przy kazdym zapytaniu od klienta - jedno zapytanie = jedna instancja
            //services.AddSingleton<IWeatherForecastService>(); //tworzy tylko raz
            services.AddControllers().AddFluentValidation();

            services.AddScoped<DiplomaManagementSeeder>();
            services.AddAutoMapper(this.GetType().Assembly);
            
            services.AddScoped<ICollegeService, CollegeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IThesisService, ThesisService>();

 /*           services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);*/

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddScoped<RequestTimeMiddleware>();

            services.AddHttpContextAccessor(); // dzieki tej linijce mo¿na wstrzykn¹æ httpcontextaccesor do klasy
            services.AddSwaggerGen();

            /*
             services.AddCors(options =>
            {
                options.AddPolicy("DiplomaManagementClient", builder =>
                {
                    builder.AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins(Configuration["AllowedOrigins"]);


                });
            });
            */


            services.AddDbContext<DiplomaManagementDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DiplomaManagementDbConnection")));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DiplomaManagementSeeder seeder)
        {
            app.UseStaticFiles();
            //app.UseCors("DiplomaManagementClient");
            seeder.Seed();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<RequestTimeMiddleware>();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API");
            });

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
