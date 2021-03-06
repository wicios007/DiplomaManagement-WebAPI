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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Entities;
using WebAPI.Interfaces;
using WebAPI.Middleware;
using WebAPI.Models;
using WebAPI.Models.Validators;
using WebAPI.Services;
using System.Security.Claims;
using System.Net.Mail;
using System.Net;

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
            services.Configure<JwtOptionsDto>(options => Configuration.GetSection("JwtOptions").Bind(options));
            services.AddIdentity<User, Role>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

            })
            .AddRoleManager<RoleManager<Role>>()
            .AddUserManager<UserManager<User>>()
            .AddEntityFrameworkStores<DiplomaManagementDbContext>()
            .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
                options.ClaimsIdentity.RoleClaimType = ClaimTypes.Role;
            });


            services.AddControllers().AddFluentValidation();

            services.AddScoped<DiplomaManagementSeeder>();
            services.AddAutoMapper(this.GetType().Assembly);

            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IThesisService, ThesisService>();

            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
            services.AddScoped<IUserContextService, UserContextService>();

            services.AddScoped<IProposedThesisService, ProposedThesisService>();
            services.AddScoped<IProposedThesisCommentService, ProposedThesisCommentService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddScoped<RequestTimeMiddleware>();
            services.AddScoped<IEmailSenderService, EmailSenderService>();
            services.AddScoped((serviceProvider) =>
            {
                var config = serviceProvider.GetRequiredService<IConfiguration>();
                return new SmtpClient()
                {
                    Host = config.GetValue<string>("Email:Smtp:Host"),
                    Port = config.GetValue<int>("Email:Smtp:Port"),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(config.GetValue<string>("Email:Smtp:Username"), config.GetValue<string>("Email:Smtp:Password"))
                };
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtOptions:SecretKey"])),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });

            services.AddHttpContextAccessor();
            services.AddSwaggerGen();

            

/*             services.AddCors(options =>
            {
                options.AddPolicy("DiplomaManagementClient", builder =>
                {
                    //var origins = Configuration["AllowedOrigins"].Split(';');
                    builder
                        .AllowAnyMethod()
                        //.AllowAnyOrigin()
                        //.WithOrigins("http://localhost:4200")
                        
                        .SetIsOriginAllowed(origin => true)
                        .AllowAnyHeader()
                        .AllowCredentials();
                    //.WithOrigins(Configuration["AllowedOrigins"]);
                });
            });*/
            


            services.AddDbContext<DiplomaManagementDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DiplomaManagementDbConnection")));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> userManager, RoleManager<Role> roleManager, DiplomaManagementSeeder seeder)
        {
            app.UseStaticFiles();
            app.UseCors("DiplomaManagementClient");
            seeder.Seed();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<RequestTimeMiddleware>();

            app.UseCors(options =>
            {
                options.SetIsOriginAllowed(origin => true)
                .AllowAnyHeader()
                .AllowAnyMethod();
            });

            app.UseAuthentication();

            DiplomaManagementUserSeeder.SeedData(userManager, roleManager);

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
