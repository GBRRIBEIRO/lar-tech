
using lar_tech.Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using lar_tech.Domain.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using lar_tech.API.Extensions;
using lar_tech.Services.Identity;
using Microsoft.OpenApi.Models;
using lar_tech.Domain.Interfaces;
using lar_tech.Data.Repositories;

namespace lar_tech.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //Modify swagger generation to accept a token
            builder.Services.AddSwaggerGen(options =>
            {
                //Add the security token configuration
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });


                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }

    });
            });

            //Add dbContext
            builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer"))
                );

            //Add identity dbContext
            builder.Services.AddDbContext<IdentityAppDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerIdentity"))
                );

            //Add default identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityAppDbContext>()
                .AddDefaultTokenProviders();

            //Extension
            //Uses authentication
            builder.Services.AddAuthenticationCustom(builder.Configuration);

            //Add identity service
            builder.Services.AddScoped<IIdentityService, IdentityService>();

            //Add Unit of Work
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //Auto migrate
            app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
            app.Services.CreateScope().ServiceProvider.GetRequiredService<IdentityAppDbContext>().Database.Migrate();

            app.MapControllers();

            app.Run();
        }
    }
}