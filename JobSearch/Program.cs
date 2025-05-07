using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

using JobSearch.Domains.Services.Contracts;
using JobSearch.Domains.Services.UseCases;
using JobSearch.Storage;

namespace JobSearch
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "JobSearch API",
                    Version = "v1",
                    Description = "API для поиска работы.",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Vadim",
                        Email = "mailto:pachavimka@mail.ru",
                    }
                });
            });

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection")));

            builder.Services.AddTransient<IRepository, Repository>();
            builder.Services.AddTransient<IAuthService, AuthService>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IEmployerService, EmployerService>();
            builder.Services.AddTransient<IResumeService, ResumeService>();
            builder.Services.AddTransient<IVacancyService, VacancyService>();
            builder.Services.AddTransient<IResponceService, ResponceService>();
            builder.Services.AddScoped<IEmailService, EmailService>();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/api/auth/login";
                    options.AccessDeniedPath = "/api/auth/forbidden";
                });

            builder.Services.AddAuthorization();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "JobSearch API v1");

                    c.DocumentTitle = "JobSearch API Documentation";
                });
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication(); 
            app.UseAuthorization(); 
            app.MapControllers();
            app.UseCors();
            app.Run();
        }
    }
}
