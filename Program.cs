﻿using System.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Shlyapnikova_lr.Data;
using Shlyapnikova_lr.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using OfficeOpenXml;
using Shlyapnikova_lr.Hubs;


namespace Shlyapnikova_lr
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            //CreateHostBuilder(args).Build().Run();
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<Shlyapnikova_lrContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("Shlyapnikova_lrContext")));


            //builder.Services.AddDbContext<Shlyapnikova_lrContext>(options =>
                //options.UseSqlServer(builder.Configuration.GetConnectionString("Shlyapnikova_lrContext") ?? throw new InvalidOperationException("Connection string 'Shlyapnikova_lrContext' not found.")));
            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", policy =>
                {
                    policy.WithOrigins("http://localhost:4200") // URL вашего Angular приложения
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSignalR();
       
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {

                            ValidateIssuer = true,
                            ValidIssuer = AuthOptions.Issuer,
                            ValidateAudience = true,
                            ValidAudience = AuthOptions.Audience,
                            ValidateLifetime = true,
                            IssuerSigningKey = AuthOptions.SigningKey,
                            ValidateIssuerSigningKey = true,
                        };
                    });
            

            var app = builder.Build();



            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowSpecificOrigin");
            app.UseHttpsRedirection();


            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.MapHub<StudentHub>("/studentHub");

            app.Run();



        }
    }
}
